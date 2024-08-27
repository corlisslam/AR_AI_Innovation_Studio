//WITH PART1 and PART 2
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;



public class Scanner : MonoBehaviour
{
    private ARTrackedImageManager _trackedImageManager;
    private Camera arCamera;
    private ARSession _arSession;
    private Keyboard keyboard;


    //private ARTrackedImage currentTrackedImage; // To store the currently tracked image

    public string lastAdditiveScene = null;
    public string lastTrackedReferenceImageName = null;

    //public UIController uiController;

    public static Scanner Instance { get; set; }

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
        arCamera = Camera.main;
        keyboard = Keyboard.current; // Access the current keyboard device

        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found. Make sure the Main Camera is tagged as MainCamera.");
        }
        
    }

    private void Update()
    {
        if (keyboard.bKey.wasPressedThisFrame) 
        {
            Debug.Log("B key was pressed.");
            Debug.Log("CurrentAdditiveScene: " + lastAdditiveScene);
            Debug.Log("Unloading additive scene: " + lastAdditiveScene);
            //SceneManager.UnloadSceneAsync(lastAdditiveScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
            Debug.Log("Additive scene unloaded");

        }

        if (keyboard.lKey.wasPressedThisFrame) //unload character tour scene with kekey
        {
            Debug.Log("L key was pressed.");
            StartCoroutine(CleanupAndUnloadScene(3));
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
            Debug.Log("Last Additive Scene: " + lastAdditiveScene);

            //Unload the last additive scene if it exists when a new tracked image is added
            if (!string.IsNullOrEmpty(lastAdditiveScene)
                && SceneManager.GetSceneByName(lastAdditiveScene).isLoaded
                && lastTrackedReferenceImageName != trackedImage.referenceImage.name)
            {
               if (lastTrackedReferenceImageName == "Part1")
                {
                    Debug.Log("Starting CleanupAdditiveScene coroutine");
                    StartCoroutine(CleanupAndUnloadScene(3));
                }

                else if (lastTrackedReferenceImageName == "Part2")
                {
                    Debug.Log("Starting CleanupAdditiveScene coroutine");
                    StartCoroutine(CleanupAndUnloadScene(3));
                }

                else if (lastTrackedReferenceImageName == "MarkerModels")
                {
                    Debug.Log("Starting CleanupAdditiveScene coroutine");
                    StartCoroutine(CleanupAndUnloadScene(2));
                }

            }

            if (lastTrackedReferenceImageName != trackedImage.referenceImage.name)
            {
                if (trackedImage.referenceImage.name == "Part1")
                {
                    Debug.Log($"lastTrackedReferenceImageName is: {lastTrackedReferenceImageName}");
                    Debug.Log($"New tracked image's name is {trackedImage.referenceImage.name}, loading CharacterTour scene by calling LoadScene Coroutine");

                    StartCoroutine(LoadScene(3, "CharacterTour", trackedImage.referenceImage.name));

                    SelectedTriggerIndexSetter.Instance.selectedTriggerIndex = 0;
                    Debug.Log($"SelectedTriggerIndexSetter.Instance.selectedTriggerIndex set to: {SelectedTriggerIndexSetter.Instance.selectedTriggerIndex}");

                }

                else if (trackedImage.referenceImage.name == "Part2")
                {
                    Debug.Log($"lastTrackedReferenceImageName is: {lastTrackedReferenceImageName}");
                    Debug.Log($"New tracked image's name is {trackedImage.referenceImage.name}, loading CharacterTour scene by calling LoadScene Coroutine");

                    StartCoroutine(LoadScene(3, "CharacterTour", trackedImage.referenceImage.name));

                    SelectedTriggerIndexSetter.Instance.selectedTriggerIndex = 1;
                    Debug.Log($"SelectedTriggerIndexSetter.Instance.selectedTriggerIndex set to: {SelectedTriggerIndexSetter.Instance.selectedTriggerIndex}");

                }

                else if (trackedImage.referenceImage.name == "MarkerModels")
                {
                    Debug.Log($"lastTrackedReferenceImageName is: {lastTrackedReferenceImageName}");
                    Debug.Log($"New tracked image's name is {trackedImage.referenceImage.name}, loading MainScene scene by calling LoadScene Coroutine");

                    StartCoroutine(LoadScene(2, "MainScene", trackedImage.referenceImage.name));
                
                }

            }

        }
    }

    private IEnumerator LoadScene(int buildIndex, string sceneName, string referenceImageName)
    {
        Debug.Log("Loading Scene: " + buildIndex);
        yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
        Debug.Log("Loaded Scene: " + buildIndex);

        lastAdditiveScene = sceneName;
        Debug.Log($"Updated lastAdditiveScene to: {lastAdditiveScene}");

        lastTrackedReferenceImageName = referenceImageName;
        Debug.Log($"lastTrackedReferenceImageName updated to: {lastTrackedReferenceImageName}");

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

        SetRestartAndExitButtonActive();
    }

    private void SetRestartAndExitButtonActive()
    {
        UIController.Instance.restartButton.gameObject.SetActive(true);
        UIController.Instance.exitButton.gameObject.SetActive(true);
    }

    private void SetRestartAndExitButtonInactive()
    {
        UIController.Instance.restartButton.gameObject.SetActive(false);
        UIController.Instance.exitButton.gameObject.SetActive(false);
    }


    public IEnumerator CleanupAndUnloadScene(int buildIndex)
    {
        SetRestartAndExitButtonInactive();

        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("Character");
        foreach (GameObject obj in objectsToDestroy)
        {
            Debug.Log("Destroying object tagged Character.");
            Destroy(obj);
            Debug.Log("Destroyed an object tagged Character.");

            // Wait until the object is null (i.e., fully destroyed)
            yield return new WaitUntil(() => obj == null);
        }

        Debug.Log("Unloading Scene: " + buildIndex);
        yield return SceneManager.UnloadSceneAsync(buildIndex);
        Debug.Log("Last additive scene unloaded");
        //currentAdditiveScene = null;
        //Debug.Log("Set CurrentAdditiveScene as null.");

    }
}

//WITH PART 1 AND PART 2

//// WITH JUST PART 1
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.UI;
//using UnityEngine.EventSystems;
//using UnityEngine.InputSystem;



//public class Scanner : MonoBehaviour
//{
//    private ARTrackedImageManager _trackedImageManager;
//    private Camera arCamera;
//    private ARSession _arSession;
//    private Keyboard keyboard;


//    //private ARTrackedImage currentTrackedImage; // To store the currently tracked image

//    private string currentAdditiveScene = null;

//    //public UIController uiController;

//    void Awake()
//    {
//        _trackedImageManager = GetComponent<ARTrackedImageManager>();
//        _arSession = FindObjectOfType<ARSession>();
//        arCamera = Camera.main;
//        keyboard = Keyboard.current; // Access the current keyboard device

//        if (arCamera == null)
//        {
//            Debug.LogError("AR Camera not found. Make sure the Main Camera is tagged as MainCamera.");
//        }


//    }

//    private void Update()
//    {
//        if (keyboard.bKey.wasPressedThisFrame)
//        {
//            Debug.Log("B key was pressed.");
//            Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);
//            Debug.Log("Unloading additive scene: " + currentAdditiveScene);
//            SceneManager.UnloadSceneAsync(currentAdditiveScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
//            Debug.Log("Additive scene unloaded");

//        }

//        if (keyboard.lKey.wasPressedThisFrame) //unload character tour scene with kekey
//        {
//            Debug.Log("L key was pressed.");
//            StartCoroutine(CleanupAndUnloadScene(3));
//        }
//    }

//    /*
//    private void UnloadScene()
//    {
//        Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);
//        Debug.Log("Unloading additive scene: " + currentAdditiveScene);
//        SceneManager.UnloadSceneAsync(currentAdditiveScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
//        Debug.Log("Additive scene unloaded");
//    }
//    */

//    private void OnEnable()
//    {
//        _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
//    }

//    private void OnDisable()
//    {
//        _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
//    }

//    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
//    {
//        foreach (var trackedImage in eventArgs.added)
//        {
//            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
//            Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);

//            /* //MAKE SURE IT DOESN'T RESCAN BY ITSELF
//            if (string.IsNullOrEmpty(currentAdditiveScene))
//            {
//                if (trackedImage.referenceImage.name == "MarkerBall")
//                {
//                    Debug.Log("Tracked image's name is MarkerBall, loading CharacterTour scene");
//                    SceneManager.LoadSceneAsync("CharacterTour", LoadSceneMode.Additive);
//                    currentAdditiveScene = "CharacterTour";
//                    Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);

//                }

//                if (trackedImage.referenceImage.name == "MarkerModels")
//                {
//                    Debug.Log("Tracked image's name is MarkerModels, loading MainScene scene");
//                    SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
//                    currentAdditiveScene = "MainScene";
//                    Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);

//                }
//            }

//            if (!string.IsNullOrEmpty(currentAdditiveScene))
//            {
//                Debug.Log("Another image in background detected but already have scanned an image");
//            }
//            */ //MAKE SURE IT DOESN'T RESCAN BY ITSELF

//            //Unload the current additive scene if it exists when a new tracked image is added
//            if (!string.IsNullOrEmpty(currentAdditiveScene) && SceneManager.GetSceneByName(currentAdditiveScene).isLoaded)
//            {

//                Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);
//                //Debug.Log("Unloading additive scene: " + currentAdditiveScene);
//                //SceneManager.UnloadSceneAsync(currentAdditiveScene, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects);
//                //Debug.Log("Additive scene unloaded");

//                if (currentAdditiveScene == "CharacterTour" && trackedImage.referenceImage.name != "Part2")
//                {
//                    Debug.Log("Starting CleanupAdditiveScene coroutine");
//                    StartCoroutine(CleanupAndUnloadScene(3));
//                    //Debug.Log("Starting UnloadScene coroutine for CharacterTour");
//                    //StartCoroutine(UnloadScene(3));
//                }

//                else if (currentAdditiveScene == "MainScene" && trackedImage.referenceImage.name != "MarkerModels")
//                {
//                    Debug.Log("Starting UnloadScene coroutine for MainScene");
//                    StartCoroutine(CleanupAndUnloadScene(2));
//                }


//            }


//            if (trackedImage.referenceImage.name == "Part2" && currentAdditiveScene != "CharacterTour")
//            {

//                Debug.Log("Tracked image's name is Part2, loading CharacterTour scene by calling LoadScene Coroutine");
//                StartCoroutine(LoadScene(3));
//                //SceneManager.LoadSceneAsync("CharacterTour", LoadSceneMode.Additive).completed += OnSceneLoaded;

//                SelectedTriggerIndexSetter.Instance.selectedTriggerIndex = 1;
//                Debug.Log("SelectedTriggerIndexSetter.Instance.selectedTriggerIndex set: " + SelectedTriggerIndexSetter.Instance.selectedTriggerIndex);

//                currentAdditiveScene = "CharacterTour";
//                Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);


//            }

//            if (trackedImage.referenceImage.name == "MarkerModels" && currentAdditiveScene != "MainScene")
//            {

//                Debug.Log("Tracked image's name is MarkerModels, loading MainScene scene by calling LoadScene Coroutine.");
//                StartCoroutine(LoadScene(2));
//                //SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive).completed += OnSceneLoaded ;
//                currentAdditiveScene = "MainScene";
//                Debug.Log("CurrentAdditiveScene: " + currentAdditiveScene);
//            }
//            //Unload the current additive scene if it exists when a new tracked image is added

//        }
//    }

//    private IEnumerator LoadScene(int buildIndex)
//    {
//        Debug.Log("Loading Scene: " + buildIndex);
//        yield return SceneManager.LoadSceneAsync(buildIndex, LoadSceneMode.Additive);
//        // Scene has loaded, now disable and re-enable the ARTrackedImageManager
//        Debug.Log("Calling RestartARSession Coroutine.");
//        StartCoroutine(RestartARSession());
//    }

//    private IEnumerator RestartARSession()
//    {
//        // Disable the AR session
//        if (_arSession != null)
//        {
//            Debug.Log("Disabling AR session...");
//            _arSession.Reset();  // Alternatively, you can disable it and then re-enable if Reset isn't sufficient
//            yield return new WaitForSeconds(1f); // Wait a second for the session to reset
//            Debug.Log("AR session restarted.");
//        }

//        // Re-enable the AR session
//        //if (_arSession != null)
//        //{
//        //    Debug.Log("Enabling AR session...");
//        //    _arSession.enabled = true;
//        //}destroy
//    }

//    private IEnumerator CleanupAndUnloadScene(int buildIndex)
//    {
//        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("CharacterTour");
//        foreach (GameObject obj in objectsToDestroy)
//        {
//            Debug.Log("Destroying object tagged CharacterTour");
//            Destroy(obj);
//            Debug.Log("Destroyed an object tagged CharacterTour.");

//            // Wait until the object is null (i.e., fully destroyed)
//            yield return new WaitUntil(() => obj == null);
//        }

//        Debug.Log("Unloading Scene: " + buildIndex);
//        yield return SceneManager.UnloadSceneAsync(buildIndex);
//        Debug.Log("Additive scene unloaded");
//        //currentAdditiveScene = null;
//        //Debug.Log("Set CurrentAdditiveScene as null.");

//    }

//    //private IEnumerator UnloadScene(int buildIndex)
//    //{
//    //    //Debug.Log("Current additive scene is" + currentAdditiveScene);
//    //    Debug.Log("Unloading Scene: " + buildIndex);
//    //    yield return SceneManager.UnloadSceneAsync(buildIndex);
//    //}

//}

