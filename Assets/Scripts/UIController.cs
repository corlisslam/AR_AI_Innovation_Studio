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

    public GameObject canvasUIPrefab;
    private GameObject instantiatedCanvasUI;

    public Button exitButton;
    public Button restartButton;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy any duplicate instances
            return;
        }

        instantiatedCanvasUI = Instantiate(canvasUIPrefab);
        Debug.Log("Canvas UI instantiated, but all buttons disabled.");
        Debug.Log("UI that is assigned to instantiatedCanvasUI is " + instantiatedCanvasUI.name);

        exitButton = instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();
        restartButton = instantiatedCanvasUI.transform.Find("RestartButton").GetComponent<Button>();

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
            exitButton.gameObject.SetActive(true);
            Debug.Log("Listener added to exitbutton and button is initialized.");
        }

        else
        {
            Debug.Log("exitButton cannot be found.");
        }

        if (restartButton != null)
        {
            restartButton.onClick.AddListener(RestartScan);
            Debug.Log("Listener added to restartButton.");
        }

        else
        {
            Debug.Log("restartButton cannot be found.");
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
        if (exitButton != null && restartButton != null)
        {
            exitButton.gameObject.SetActive(false);
            Debug.Log("Exit button is deactivated.");

            restartButton.gameObject.SetActive(false);
            Debug.Log("Restart button is deactivated.");

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
            Debug.LogError("Exit button or restart button is null. Unable to exit.");
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


    //private void ExitGame()
    //{
    //    if (exitButton != null)
    //    {
    //        exitButton.gameObject.SetActive(false);
    //        Debug.Log("Exit button is deactivated (singleton)");

    //        Debug.Log("Calling CleanupAdditiveScene CoRoutine");
    //        StartCoroutine(CleanupCharacterTourScene());

    //        Debug.Log("Destroying SelectedTriggerIndex Singleton.");
    //        if (SelectedTriggerIndex.Instance != null)
    //        {
    //            SelectedTriggerIndex.Instance.DestroySelectedTriggerIndex();
    //            // do i need coroutine here to wait till end of frame to make sure the object is destroyed

    //        }
    //        else
    //        {
    //            Debug.Log("Unable to destroy SelectedTriggerIndex Singleton.");
    //        }

    //        Debug.Log("Loading Home Scene");
    //        SceneManager.LoadSceneAsync("HomeScene");
    //        LoaderUtility.Deinitialize();
    //        Debug.Log("Called LoaderUtility.Deinitialize()");
    //        Destroy(gameObject);

    //    }
    //    else
    //    {
    //        Debug.LogError("Unable to go home with Home button");
    //    }
    //}

    
    //public GameObject canvasUIPrefab;
    //private GameObject instantiatedCanvasUI;

    //private Button exitButton;
    //private Button cameraButton;

    //void OnEnable()
    //{
    //    Debug.Log("UIController is enabled.");

    //    instantiatedCanvasUI = Instantiate(canvasUIPrefab);
    //    Debug.Log("Canvas UI instantiated, but all buttons disabled.");
    //    Debug.Log("UI that is assigned to instantiatedCanvasUI is " + instantiatedCanvasUI.name);

    //    exitButton = instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();
    //    cameraButton = instantiatedCanvasUI.transform.Find("CameraButton").GetComponent<Button>();

    //    if (exitButton != null && cameraButton != null)
    //    {
    //        exitButton.onClick.AddListener(ExitGame);
    //        exitButton.gameObject.SetActive(true);
    //        Debug.Log("Listener added to Exit button and button is initialized");
    //        cameraButton.onClick.AddListener(CameraScreen);
    //        cameraButton.gameObject.SetActive(true);
    //        Debug.Log("Listener added to Camera button and button is initialized");
    //    }

    //    else
    //    {
    //        Debug.Log("exitButton and cameraButton cannot be found.");
    //    }

    //}

    //void ExitGame()
    //{
    //    string currentAdditiveScene = FindCurrentAdditiveScene();
    //    // Unload the current additive scene if it exists
    //    if (!string.IsNullOrEmpty(currentAdditiveScene) && SceneManager.GetSceneByName(currentAdditiveScene).isLoaded)
    //    {
    //        exitButton.gameObject.SetActive(false);
    //        Debug.Log("Exit button is deactivated");
    //        cameraButton.gameObject.SetActive(false);
    //        Debug.Log("Camera button is deactivated");

    //        this.gameObject.SetActive(false);
    //        Debug.Log("UIController GameObject is deactivated");

    //        Debug.Log("Unloading additive scene: " + currentAdditiveScene);
    //        SceneManager.UnloadSceneAsync(currentAdditiveScene);
    //        SceneManager.LoadScene("HomeScene");
    //        LoaderUtility.Deinitialize();
    //    }
    //    else
    //    {
    //        Debug.LogError("Unable to go home with Home button");
    //    }
    //}

    //void CameraScreen()
    //{
    //    string currentAdditiveScene = FindCurrentAdditiveScene();
    //    // Unload the current additive scene if it exists
    //    if (!string.IsNullOrEmpty(currentAdditiveScene) && SceneManager.GetSceneByName(currentAdditiveScene).isLoaded)
    //    {
    //        exitButton.gameObject.SetActive(false);
    //        Debug.Log("Exit button is deactivated");
    //        cameraButton.gameObject.SetActive(false);
    //        Debug.Log("Camera button is deactivated");

    //        this.gameObject.SetActive(false);
    //        Debug.Log("UIController GameObject is deactivated");

    //        Debug.Log("Unloading additive scene: " + currentAdditiveScene);
    //        SceneManager.UnloadSceneAsync(currentAdditiveScene);

    //    }
    //    else
    //    {
    //        Debug.LogError("Unable to go home with Home button");
    //    }
    //}


    //private string FindCurrentAdditiveScene()
    //{
    //    GameObject xrOrigin = GameObject.Find("XR Origin (AR Rig)");

    //    if (xrOrigin != null)
    //    {
    //        Scanner scanner = xrOrigin.GetComponent<Scanner>();

    //        if (scanner != null)
    //        {
    //            string currentAdditiveScene = scanner.currentAdditiveScene;
    //            Debug.Log("currentAdditiveScene from Scanner.cs: " + currentAdditiveScene);
    //            return currentAdditiveScene;
    //        }
    //        else
    //        {
    //            Debug.LogError("Scanner.cs not found in XR Origin (Ar Rig).");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("XR Origin (AR Rig) not found in the scene.");
    //    }

    //    return string.Empty;

    //}
}
