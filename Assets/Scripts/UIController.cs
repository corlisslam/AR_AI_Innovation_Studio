/// < summary >
///     Manages all aspects of the user interface, including showing and hiding buttons, updating UI elements, what pressing a button does.
///     It is part of its own game object.
/// </summary>
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{
    public static UIController Instance { get; private set; }

    public GameObject CanvasUIPrefab;
    private GameObject _instantiatedCanvasUI;

    public Button ExitButton;
    public Button RestartButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _instantiatedCanvasUI = Instantiate(CanvasUIPrefab);
        Debug.Log("CanvasUIPrefab instantiated, but all buttons disabled.");
        Debug.Log("UI that is assigned to instantiatedCanvasUI is " + _instantiatedCanvasUI.name);

        ExitButton = _instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();
        RestartButton = _instantiatedCanvasUI.transform.Find("RestartButton").GetComponent<Button>();

        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(ExitGame);
            ExitButton.gameObject.SetActive(true);
            Debug.Log("Listener added to ExitButton and button is initialized.");
        }

        else
        {
            Debug.Log("ExitButton cannot be found.");
        }

        if (RestartButton != null)
        {
            RestartButton.onClick.AddListener(RestartScan);
            Debug.Log("Listener added to RestartButton.");
        }

        else
        {
            Debug.Log("RestartButton cannot be found.");
        }
    }


    private IEnumerator CleanupCharacterTourScene()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject obj in objectsToDestroy)
        {
            Debug.Log("Destroying object.");
            Destroy(obj);

            // Wait until the object is null (i.e., fully destroyed)
            yield return new WaitUntil(() => obj == null);
        }
        
    }

    private void RestartScan()
    {
        Debug.Log($"LastAdditiveScene is: {Scanner.Instance.LastAdditiveScene}");
        Debug.Log($"LastTrackedReferenceImageName is: {Scanner.Instance.LastTrackedReferenceImageName}");

        if (Scanner.Instance.LastAdditiveScene == "CharacterTour")
        {
            StartCoroutine(Scanner.Instance.CleanupAndUnloadScene(3));
        }

        else if (Scanner.Instance.LastAdditiveScene == "MainScene")
        {
            StartCoroutine(Scanner.Instance.CleanupAndUnloadScene(2));
        }

        Scanner.Instance.LastAdditiveScene = null; // "rescanning" is restarting the AR session from fresh
        Debug.Log("Set LastAdditiveScene to null.");

        Scanner.Instance.LastTrackedReferenceImageName = null;
        Debug.Log("Set LastTrackedReferenceImageName to null.");

        StartCoroutine(Scanner.Instance.RestartARSession());
    }


    private IEnumerator ExitGameCoroutine(float timeout)
    {
        // Deactivate the exit button
        if (ExitButton != null && RestartButton != null)
        {
            ExitButton.gameObject.SetActive(false);
            Debug.Log("ExitButton is deactivated.");

            RestartButton.gameObject.SetActive(false);
            Debug.Log("RestartButton is deactivated.");

            // Start cleaning up the CharacterTour scene
            Debug.Log("Starting CleanupCharacterTourScene Coroutine.");
            yield return StartCoroutine(CleanupCharacterTourScene());

            //should you add a coroutine to make sure all additive scenes are cleared?

            // Destroy the SelectedTriggerIndex Singleton
            Debug.Log("Destroying SelectedTriggerIndex Singleton.");
            if (SelectedTriggerIndexSetter.Instance != null)
            {
                SelectedTriggerIndexSetter.Instance.DestroySelectedTriggerIndex();
                yield return null;
            }
            else
            {
                Debug.Log("Unable to destroy SelectedTriggerIndex Singleton.");
            }

            // Load the home scene
            Debug.Log("Loading Home Scene...");
            AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HomeScene");

            float elapsedTime = 0f;

            // Wait for the scene to load or for the timeout
            while (!asyncLoad.isDone)
            {
                // If the loading takes longer than the timeout, exit the loop
                if (elapsedTime >= timeout)
                {
                    Debug.LogError("Scene loading timed out.");
                    HandleSceneLoadFailure();
                    yield break;
                }

                // Increment the elapsed time
                elapsedTime += Time.deltaTime;

                // Yield until the next frame
                yield return null;
            }
            //yield return asyncLoad;  // Wait for the scene to fully load

            // If the scene loads successfully
            Debug.Log("Scene loaded successfully.");

            // Deinitialize the loader utility after the scene loads
            LoaderUtility.Deinitialize();
            Debug.Log("Called LoaderUtility.Deinitialize().");

            // Destroy the Scanner/XR origin singleton after everything has completed
            Destroy(Scanner.Instance.gameObject);
            Debug.Log("Scanner's game object XR Origin is destroyed.");

            // Destroy the UIController after everything has completed
            Destroy(gameObject);
            Debug.Log("UIController destroyed.");

        }
        else
        {
            Debug.LogError("ExitButton or RestartButton is null. Unable to exit.");
        }
    }

    private void HandleSceneLoadFailure()
    {
        // Handle what should happen if the scene fails to load
        Debug.LogError("Failed to load Home Scene.");
        // Add error handling, fallback logic, or retry logic here
    }

    private void ExitGame()
    {
        PlayerPrefs.SetString("PlayerName", "Visitor");
        
        StartCoroutine(ExitGameCoroutine(10f));
    }
}
