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
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        if (CanvasUIPrefab != null)
        {
            _instantiatedCanvasUI = Instantiate(CanvasUIPrefab);
            Debug.Log("CanvasUIPrefab instantiated successfully.");
            Debug.Log("UI that is assigned to instantiatedCanvasUI is " + _instantiatedCanvasUI.name);
        }
        else
        {
            Debug.LogError("CanvasUIPrefab is not assigned. Please assign it in the inspector.");
        }

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        ExitButton = _instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();
        if (ExitButton == null)
        {
            Debug.LogError("ExitButton cannot be found.");
            return;
        }
        ExitButton.onClick.AddListener(ExitGame);
        SetExitButtonActive();

        RestartButton = _instantiatedCanvasUI.transform.Find("RestartButton").GetComponent<Button>();
        if (RestartButton == null)
        {
            Debug.LogError("RestartButton cannot be found.");
            return;
        }
        RestartButton.onClick.AddListener(RestartScan);
        Debug.Log("Listener added to RestartButton.");

    }

    private void RestartScan()
    {
        Debug.Log($"LastAdditiveScene is: {Scanner.Instance.LastAdditiveScene}");
        Debug.Log($"LastTrackedReferenceImageName is: {Scanner.Instance.LastTrackedReferenceImageName}");

        StartCoroutine(CallCleanupAndUnloadScene());
    }

    private IEnumerator CallCleanupAndUnloadScene()
    {
        int lastAdditiveSceneIndex = GetLastAdditiveSceneIndex(Scanner.Instance.LastAdditiveScene);

        bool isUnloaded = false;
        yield return StartCoroutine(Scanner.Instance.CleanupAndUnloadScene(lastAdditiveSceneIndex, success => isUnloaded = success));

        if (isUnloaded == false)
        {
            SetRestartButtonActive();
            SetExitButtonActive();
            Debug.LogError("Unable to unload Last Additive Scene, cannot restart scan.");
            yield break;
        }

        Scanner.Instance.LastAdditiveScene = null; // "rescanning" is restarting the AR session from fresh
        Debug.Log("Set LastAdditiveScene to null.");

        Scanner.Instance.LastTrackedReferenceImageName = null;
        Debug.Log("Set LastTrackedReferenceImageName to null.");

        yield return StartCoroutine(Scanner.Instance.RestartARSession());
    }

    private int GetLastAdditiveSceneIndex(string lastAdditiveScene)
    {
        switch (lastAdditiveScene)
        {
            case "CharacterTour":
                return 3;
            case "MainScene":
                return 2;
            default:
                return -1; // handle as needed later
        }
    }

    private void SetExitButtonActive()
    {
        ExitButton.gameObject.SetActive(true);
        Debug.Log("ExitButton activated");
    }

    private void SetRestartButtonActive()
    {
        RestartButton.gameObject.SetActive(true);
        Debug.Log("RestartButton activated");
    }

    private void SetRestartAndExitButtonInactive()
    {
        RestartButton.gameObject.SetActive(false);
        ExitButton.gameObject.SetActive(false);
        Debug.Log("ExitButton and RestartButton inactivated");
    }

    private IEnumerator ExitGameCoroutine()
    {
        bool loadSuccess = false;
        yield return StartCoroutine(LoadHomeScene(success => loadSuccess = success));

        if (loadSuccess)
        {
            Debug.Log("Scene loaded successfully.");
            PlayerPrefs.SetString("PlayerName", "Visitor");
        }
        else if (!loadSuccess)
        {
            Debug.LogError("Failed to load Home Scene. Exiting coroutine.");
            SetExitButtonActive();
            SetRestartButtonActive();
            yield break;
        }

        yield return StartCoroutine(Scanner.Instance.CleanupCharacterTourScene());

        Debug.Log("Destroying SelectedTriggerIndex Singleton.");
        if (SelectedTriggerIndexSetter.Instance == null)
        {
            Debug.LogWarning("Unable to destroy SelectedTriggerIndex Singleton as SelectedTriggerIndexSetter.Instance is null.");
        }

        else
        {
            SelectedTriggerIndexSetter.Instance.DestroySelectedTriggerIndex();
            Debug.Log("Destroyed SelectedTriggerIndexSetter instance.");
        }

        LoaderUtility.Deinitialize();
        Debug.Log("Called LoaderUtility.Deinitialize().");

        Destroy(Scanner.Instance.gameObject);
        Debug.Log("Scanner's game object XR Origin is destroyed.");

        Destroy(gameObject);
        Debug.Log("UIController destroyed.");
    }

    private IEnumerator LoadHomeScene(Action<bool> onComplete)
    {
        Debug.Log("Loading Home Scene...");
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("HomeScene");

        float elapsedTime = 0f;
        float timeout = 10f;

        while (!asyncLoad.isDone)
        {
            if (elapsedTime >= timeout)
            {
                Debug.LogError("Scene loading timed out.");
                onComplete?.Invoke(false);
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }
        
        onComplete?.Invoke(true);
    }

    //private void HandleSceneLoadFailure()
    //{
    //    SetExitButtonActive();
    //    SetRestartButtonActive();
    //}

    private void ExitGame()
    {
        SetRestartAndExitButtonInactive();
        StartCoroutine(ExitGameCoroutine());
    }
}
