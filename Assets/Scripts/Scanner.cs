/// <summary>
///     Scanner.cs is part of XR origin game object because allows for direct reference to XR origin game object components without
///     needing to search for components across the scene. Plus it is inherently tied to the AR tracking functionality so keeping it
///     with XR origin game object keeps related functionality grouped together. If Scanner.cs is in its own game object, referencing
///     the ARTrackedImageManager component in XR origin is more costly performance wise because it requires Unity to search the entire scene.
/// </summary>

//WITH PART1 and PART 2
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
    //private Keyboard keyboard; // CAN DELETE AFTER TESTING

    //private ARTrackedImage currentTrackedImage; // To store the currently tracked image

    public string LastAdditiveScene { get; set; } = null; 
    public string LastTrackedReferenceImageName { get; set; } = null;

    //public UIController uiController;

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

        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        _arSession = FindObjectOfType<ARSession>();
        _arCamera = Camera.main;
        //keyboard = Keyboard.current; // Access the current keyboard device

        if (_arCamera == null)
        {
            Debug.LogError("AR Camera not found. Make sure the Main Camera is tagged as MainCamera.");
        }
        
    }

    private void Update()
    {
        //if (keyboard.bKey.wasPressedThisFrame) 
        //{
        //    Debug.Log("B key was pressed.");
        //    Debug.Log("CurrentAdditiveScene: " + lastAdditiveScene);
        //    Debug.Log("Unloading additive scene: " + lastAdditiveScene);
        //    //SceneManager.UnloadSceneAsync(lastAdditiveScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
        //    Debug.Log("Additive scene unloaded");

        //}

        //if (keyboard.lKey.wasPressedThisFrame) //unload character tour scene with kekey
        //{
        //    Debug.Log("L key was pressed.");
        //    StartCoroutine(CleanupAndUnloadScene(3));
        //}
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

            //Unload the last additive scene if it exists when a new tracked image is added
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

        int sceneNumber = GetSceneIndex(imageName);
        string sceneName = GetSceneName(sceneNumber);

        StartCoroutine(LoadScene(sceneNumber, sceneName, imageName));
        SetSelectedTriggerIndex(imageName);
    }

    private void SetSelectedTriggerIndex(string trackedReferenceImageName)
    {
        int index = trackedReferenceImageName == "Part 1" ? 0 :
                trackedReferenceImageName == "Part 2" ? 1 : -1;

        if (index != -1)
        {
            SelectedTriggerIndexSetter.Instance.selectedTriggerIndex = index;
            Debug.Log($"SelectedTriggerIndexSetter.Instance.selectedTriggerIndex set to: {index}");
        }
    }

    private string GetSceneName(int sceneNumber)
    {
        switch(sceneNumber)
        {
            case 2:
                return "MainScene";
            case 3:
                return "CharacterTour";
            default:
                return null; // handle as needed later
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
        UIController.Instance.exitButton.gameObject.SetActive(true);
    }

    private void SetRestartButtonActive()
    {
        UIController.Instance.restartButton.gameObject.SetActive(true);
    }

    private void SetRestartAndExitButtonInactive()
    {
        UIController.Instance.restartButton.gameObject.SetActive(false);
        UIController.Instance.exitButton.gameObject.SetActive(false);
    }


    public IEnumerator CleanupAndUnloadScene(int buildIndex)
    {
        SetRestartAndExitButtonInactive();

        StartCoroutine(CleanupCharacterTourScene());

        Debug.Log("Unloading Scene: " + buildIndex);
        yield return SceneManager.UnloadSceneAsync(buildIndex);
        Debug.Log("Last additive scene unloaded");
    }

    // Need to do this to clear npc character
    private IEnumerator CleanupCharacterTourScene()
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

