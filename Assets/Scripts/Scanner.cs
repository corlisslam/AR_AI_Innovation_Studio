/// <summary>
///     Scanner.cs is part of XR origin game object, allowing direct reference to XR origin game object components without the overhead of searching across the scene.
///     Plus it is inherently tied to the AR tracking functionality so keeping it with XR origin game object keeps related functionality grouped together.
/// </summary>

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.InputSystem;

public class Scanner : MonoBehaviour
{
    public static Scanner Instance { get; private set; }

    private ARTrackedImageManager _trackedImageManager;
    private Camera _arCamera;
    private ARSession _arSession;

    public string LastAdditiveScene = null; 
    public string LastTrackedReferenceImageName = null;


    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();

        if (_trackedImageManager == null)
        {
            Debug.LogError("ARTrackedImageManager not found. Please attach it to the XR Origin game object.");
        }

        _arSession = FindObjectOfType<ARSession>();
        if (_arSession == null)
        {
            Debug.LogError("ARSession not found in the scene. Ensure an ARSession component is available.");
        }

        _arCamera = Camera.main;

        if (_arCamera == null)
        {
            Debug.LogError("AR Camera not found. Ensure the Main Camera is tagged as 'MainCamera'.");
        }
    }


    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {        _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
            Debug.Log("Last Additive Scene: " + LastAdditiveScene);

            if (ShouldUnloadLastScene(trackedImage))
            {
                StartCoroutine(CleanupAndUnloadScene(GetSceneIndex(LastTrackedReferenceImageName)));
            }

            if (LastTrackedReferenceImageName != trackedImage.referenceImage.name)
            {
                LoadSceneBasedOnTrackedImage(trackedImage);
                SetRestartButtonActive();
            }
        }
    }

    private bool ShouldUnloadLastScene(ARTrackedImage trackedImage)
    {
        return !string.IsNullOrEmpty(LastAdditiveScene) &&
               SceneManager.GetSceneByName(LastAdditiveScene).isLoaded &&
               LastTrackedReferenceImageName != trackedImage.referenceImage.name;
    }

    private void LoadSceneBasedOnTrackedImage(ARTrackedImage trackedImage)
    {
        string imageName = trackedImage.referenceImage.name;
        Debug.Log($"lastTrackedReferenceImageName is: {LastTrackedReferenceImageName}");
        Debug.Log($"New tracked image's name is {imageName}, loading corresponding scene by calling LoadScene Coroutine");

        int sceneIndex = GetSceneIndex(imageName);
        Debug.Log($"sceneIndex is: {sceneIndex}");
        if (sceneIndex < 0)
        {
            Debug.LogWarning($"No scene associated with the tracked image: {imageName}");
            return;
        }

        string sceneName = GetSceneName(sceneIndex);
        Debug.Log($"sceneName is: {sceneName}");
        if (string.IsNullOrEmpty(sceneName))
        {
            Debug.LogError($"Scene index {sceneIndex} does not correspond to a valid scene name.");
            return;
        }

        StartCoroutine(LoadScene(sceneIndex, sceneName, imageName));
        SetSelectedTriggerIndex(imageName);
    }

    private void SetSelectedTriggerIndex(string trackedReferenceImageName)
    {
        int index = trackedReferenceImageName == "Part1" ? 0 :
                trackedReferenceImageName == "Part2" ? 1 : -1;

        if (index != -1)
        {
            SelectedTriggerIndexSetter.Instance.selectedTriggerIndex = index;
            Debug.Log($"SelectedTriggerIndexSetter.Instance.selectedTriggerIndex set to: {index}");
        }
        else
        {
            Debug.Log($"index is: {index}");
        }
    }

    private string GetSceneName(int sceneIndex)
    {
        switch(sceneIndex)
        {
            case 2:
                return "MainScene";
            case 3:
                return "CharacterTour";
            default:
                return null;
        }
    }

    private int GetSceneIndex(string trackedReferenceImageName)
    {
        switch (trackedReferenceImageName)
        {
            case "Part1":
            case "Part2":
                return 3;
            case "MarkerModels":
                return 2;
            default:
                return -1; // handle as needed later
        }
    }

    private IEnumerator LoadScene(int buildIndex, string sceneName, string referenceImageName)
    {
        Debug.Log("Loading Scene: " + buildIndex);
        yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        Debug.Log("Loaded Scene: " + buildIndex);

        LastAdditiveScene = sceneName;
        Debug.Log($"Updated LastAdditiveScene to: {LastAdditiveScene}");

        LastTrackedReferenceImageName = referenceImageName;
        Debug.Log($"LastTrackedReferenceImageName updated to: {LastTrackedReferenceImageName}");

        Debug.Log("Calling RestartARSession Coroutine.");
        StartCoroutine(RestartARSession());

    }

    public IEnumerator RestartARSession()
    {
        // Reset the AR session
        if (_arSession != null)
        {
            Debug.Log("Disabling AR session...");
            _arSession.Reset();
            yield return new WaitForSeconds(1f); // Wait a second for the session to reset
            Debug.Log("AR session restarted.");
        }

        SetExitButtonActive();
    }

    private void SetExitButtonActive()
    {
        UIController.Instance.ExitButton.gameObject.SetActive(true);
    }

    private void SetRestartButtonActive()
    {
        UIController.Instance.RestartButton.gameObject.SetActive(true);
    }

    private void SetRestartAndExitButtonInactive()
    {
        UIController.Instance.RestartButton.gameObject.SetActive(false);
        UIController.Instance.ExitButton.gameObject.SetActive(false);
    }


    public IEnumerator CleanupAndUnloadScene(int buildIndex)
    {
        SetRestartAndExitButtonInactive();

        //if (buildIndex == -1)
        //{

        //}

   
        Debug.Log("Unloading Scene: " + buildIndex);
        yield return SceneManager.UnloadSceneAsync(buildIndex);
        Debug.Log("Last additive scene unloaded");

        StartCoroutine(CleanupCharacterTourScene());
    }

    // Need to do call this coroutine to clear npc character
    public IEnumerator CleanupCharacterTourScene()
    {
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Character");

        if (objectsToDestroy.Length == 0)
        {
            Debug.Log("No objects tagged as 'Character' found. Skipping cleanup.");
            yield break;
        }

        foreach (GameObject obj in objectsToDestroy)
        {
            Debug.Log("Destroying object.");
            Destroy(obj);

            yield return new WaitUntil(() => obj == null);
        }
    }
}

