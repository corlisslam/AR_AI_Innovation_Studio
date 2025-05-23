//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.InputSystem.EnhancedTouch;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;

//public class CharacterSpawner1 : MonoBehaviour
//{
//    // Prefab for the Convai character
//    public GameObject _characterPrefab;

//    private ARTrackedImageManager _arTrackedImageManager;

//    // Event triggered when a character is spawned
//    public Action OnCharacterSpawned;

//    /// <summary>
//    /// Initializes necessary components and finds AR-related managers.
//    /// </summary>
//    ///


//    private void Awake()
//    {
//        // Assuming this script is attached to the XR Origin GameObject
//        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
//    }

//    private void OnEnable()
//    {
//        _arTrackedImageManager.trackedImagesChanged += ARTrackedImageManager_TrackedImagesChanged;
//    }

//    private void OnDisable()
//    {
//        _arTrackedImageManager.trackedImagesChanged -= ARTrackedImageManager_TrackedImagesChanged;
//    }

//    /// <summary>
//    /// Handles tracked image changes, spawning characters on added images.
//    /// </summary>
//    /// <param name="eventArgs">Event arguments containing information about tracked image changes.</param>
//    private void ARTrackedImageManager_TrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
//    {
//        foreach (var trackedImage in eventArgs.added)
//        {
//            // Handle Added Event
//            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
//            GameObject character = SpawnCharacter();
//            character.transform.parent = trackedImage.transform; ;
//        }
//    }

//    private GameObject SpawnCharacter()
//    {
//        GameObject character = Instantiate(_characterPrefab);

//        OnCharacterSpawned?.Invoke();

//        return character;
//    }

//}
//using System;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.EventSystems;
//using UnityEngine.InputSystem.EnhancedTouch;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;

//public class CharacterSpawner1 : MonoBehaviour
//{
//    // Prefab for the Convai character
//    public GameObject _characterPrefab;

//    private ARTrackedImageManager _arTrackedImageManager;

//    // Event triggered when a character is spawned
//    public Action OnCharacterSpawned;

//    /// <summary>
//    /// Initializes necessary components and finds AR-related managers.
//    /// </summary>
//    private void Awake()
//    {
//        // Assuming this script is attached to the XR Origin GameObject
//        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
//    }

//    private void OnEnable()
//    {
//        _arTrackedImageManager.trackedImagesChanged += ARTrackedImageManager_TrackedImagesChanged;
//    }

//    private void OnDisable()
//    {
//        _arTrackedImageManager.trackedImagesChanged -= ARTrackedImageManager_TrackedImagesChanged;
//    }

//    /// <summary>
//    /// Handles tracked image changes, spawning characters on added images.
//    /// </summary>
//    /// <param name="eventArgs">Event arguments containing information about tracked image changes.</param>
//    private void ARTrackedImageManager_TrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
//    {
//        foreach (var trackedImage in eventArgs.added)
//        {
//            // Handle Added Event
//            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
//            Debug.Log("Calling SpawnCharacter()");
//            GameObject character = SpawnCharacter();
//            //SpawnCharacter();
//            //Debug.Log("Character spawned: " + character.name);
//            //character.transform.SetParent(null, true);
//            //Debug.Log("Character parent set as null");

//        }
//    }

//    private GameObject SpawnCharacter()
//    {
//        Debug.Log("Spawning character");
//        GameObject character = Instantiate(_characterPrefab);
//        Debug.Log("Character spawned");

//        OnCharacterSpawned?.Invoke();
//        Debug.Log("Invoke subscription to OnCharacterSpwaned");

//        //Debug.Log("Returning character")
//        return character;
//    }

//}


// working on this version rn
using System;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class CharacterSpawner1 : MonoBehaviour
{
    // Prefab for the Convai character
    public GameObject _characterPrefab;

    private ARRaycastManager _arRaycastManager;
    private ARPlaneManager _arPlaneManager;
    private ARTrackedImageManager _arTrackedImageManager;

    // Flag to determine if character spawn mode is active
    private bool _isSpawnModeActive = true;

    // Flag indicating whether the maximum character count has been reached
    private bool _isMaxCharacterCountReached;

    // Counter for the number of characters spawned
    private int _spawnedCharacterCount;

    // Maximum number of characters allowed to be spawned
    private const int MAX_CHARACTER_COUNT = 5;

    //private ARTrackedImageManager _arTrackedImageManager;

    // Event triggered when a character is spawned
    public Action OnCharacterSpawned;

    //public GameObject canvasUIPrefab;
    //private GameObject instantiatedCanvasUI;

    //private Button exitButton;

    /// <summary>
    /// Initializes necessary components and finds AR-related managers.
    /// </summary>
    private void Awake()
    {
        _arRaycastManager = FindObjectOfType<ARRaycastManager>();
        _arPlaneManager = FindObjectOfType<ARPlaneManager>();
        //instantiatedCanvasUI = Instantiate(canvasUIPrefab);
        //Debug.Log("Canvas UI instantiated, but all buttons disabled except for exit.");
        //Debug.Log("UI that is assigned to instantiatedCanvasUI is " + instantiatedCanvasUI.name);

        //exitButton = instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();

        //if (UIController.Instance.exitButton != null)
        //{
            //exitButton.onClick.AddListener(ExitGame);
            //exitButton.gameObject.SetActive(true);
            //Debug.Log("Listener added to Exit button and button is initialized");

        //    UIController.Instance.exitButton.gameObject.SetActive(true);
        //    Debug.Log("Singleton exit button enabled in Character Tour scene.");
        //}

        //GameObject character = SpawnCharacter();
        //character.transform.SetParent(null, true);

        // Assuming this script is attached to the XR Origin GameObject
        //_arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void Start()
    {
        GameObject character = SpawnCharacter();
        NotificationSystemHandler.Instance.NotificationRequest(NotificationType.TourHasStarted);
        //SceneManagerSingleton.Instance.SetNPC(character);
    }



    //private void OnEnable()
    //{
    //    _arTrackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    //}

    //private void OnDisable()
    //{
    //    _arTrackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    //}

    /// <summary>
    /// Handles tracked image changes, spawning characters on added images.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing information about tracked image changes.</param>
    //private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    //{
    //    foreach (var trackedImage in eventArgs.added)
    //    {
    //        // Handle Added Event
    //        Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
    //        if (trackedImage.referenceImage.name == "MarkerBall")
    //        {
    //            Debug.Log("Tracked image's name is MarkerBall, calling SpawnCharacter method");
    //            GameObject character = SpawnCharacter();
    //            //character.transform.parent = trackedImage.transform;

    //            character.transform.SetParent(null, true);
    //        }
    //    }
    //}

    private GameObject SpawnCharacter()
    {
        GameObject character = Instantiate(_characterPrefab);
        Debug.Log("Instantiated character: " + character.name);

        OnCharacterSpawned?.Invoke();

        return character;
    }

    //void ExitGame()
    //{
    //    SceneManager.LoadScene("HomeScene");
    //    LoaderUtility.Deinitialize();

    //}
}



