////Code for TAP TO PLACE (WITH SCREENTOWORLDPOINT)
////using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;
//using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

////[RequireComponent(typeof(AudioSource))]
//public class PlaceObject : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject prefab;


//    private AudioSource audioClip1;

//    public Camera arCamera;

//    private float depth = 1.0f;

//    private GameObject spawnedObject;


//    private void Awake()
//    {
//        audioClip1 = GetComponent<AudioSource>();
//        //aRRaycastManager = GetComponent<ARRaycastManager>();
//        //aRPlaneManager = GetComponent<ARPlaneManager>();
//        if (audioClip1 == null)
//        {
//            Debug.LogError("AudioSource is not in the same Game Object.");
//            //Debug.LogError("AudioSource is not assigned in the Inspector.");
//        }

//        else
//        {
//            Debug.Log("AudioSource is assigned in Inspector.");
//        }
//    }

//    private void OnEnable()
//    {
//        // Enables simulation on editor
//        EnhancedTouch.TouchSimulation.Enable();
//        // Enables enhanced touch
//        EnhancedTouch.EnhancedTouchSupport.Enable();
//        // Touch on screen, cast raycast to that touch location
//        EnhancedTouch.Touch.onFingerDown += FingerDown;
//    }

//    private void OnDisable()
//    {
//        // Enables simulation on editor
//        EnhancedTouch.TouchSimulation.Disable();
//        // Enables enhanced touch
//        EnhancedTouch.EnhancedTouchSupport.Disable();
//        // Unsubscribing from event
//        EnhancedTouch.Touch.onFingerDown -= FingerDown;
//    }

//    private void FingerDown(EnhancedTouch.Finger finger)
//    {
//        if (finger.index != 0) return;

//        if (spawnedObject != null)
//        {
//            audioClip1.Stop();
//            Destroy(spawnedObject);
//            Debug.Log("Previously placed object removed from screen");

//        }
//        Vector3 screenPosition = finger.currentTouch.screenPosition;
//        Debug.Log("Screen touch position is at: " + screenPosition.x + ", " + screenPosition.y);
//        Vector3 worldPosition = arCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, depth));
//        spawnedObject = Instantiate(prefab, worldPosition, Quaternion.identity);
//        Debug.Log("Instantiated object position: " + spawnedObject.transform.position);

//        if (audioClip1 != null)
//        {
//            audioClip1.Play(0);
//            Debug.Log("Audio playing? "+ audioClip1.isPlaying);
//        }
//        else
//        {
//            Debug.LogError("AudioSource is not assigned or is missing.");
//        }

//    }
//}

////Code for TAP TO PLACE (WITHOUT PLANE DETECTION):
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.XR.ARFoundation;
//using UnityEngine.XR.ARSubsystems;
//using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;

//[RequireComponent(requiredComponent: typeof(ARRaycastManager))] // Will not let you delete those components if this script is attached to game object
//public class PlaceObject : MonoBehaviour
//{
//    [SerializeField]
//    private GameObject prefab;

//    private ARRaycastManager aRRaycastManager;
//    //private ARPlaneManager aRPlaneManager;
//    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
//    private GameObject spawnedObject;

//    private void Awake()
//    {
//        aRRaycastManager = GetComponent<ARRaycastManager>();
//        //aRPlaneManager = GetComponent<ARPlaneManager>();
//    }

//    private void OnEnable()
//    {
//        // Enables simulation on editor
//        //EnhancedTouch.TouchSimulation.Enable();
//        // Enables enhanced touch
//        EnhancedTouch.EnhancedTouchSupport.Enable();
//        // Touch on screen, cast raycast to that touch location
//        EnhancedTouch.Touch.onFingerDown += FingerDown;
//    }

//    private void OnDisable()
//    {
//        // Enables simulation on editor
//        //EnhancedTouch.TouchSimulation.Disable();
//        // Enables enhanced touch
//        EnhancedTouch.EnhancedTouchSupport.Disable();
//        // Unsubscribing from event
//        EnhancedTouch.Touch.onFingerDown -= FingerDown;
//    }

//    private void FingerDown(EnhancedTouch.Finger finger)
//    {
//        if (finger.index != 0) return;

//        if (aRRaycastManager.Raycast(finger.currentTouch.screenPosition, hits, TrackableType.FeaturePoint))
//        {
//            Pose hitPose = hits[0].pose;
//            if (spawnedObject != null)
//            {
//                Destroy(spawnedObject);
//                Debug.Log("Previously placed object removed from screen");
//            }
//            spawnedObject = Instantiate(prefab, hitPose.position, hitPose.rotation);
//            spawnedObject.transform.localPosition += prefab.transform.localPosition;
//            Debug.Log("hitPose is at: " + hitPose.position);
//            Debug.Log("Instantiated object scale: " + spawnedObject.transform.position);
//        }
//    }
//}


// currently this script detects in a loop if there is an object placed in scene and if there is not, it would place the object at the first touch's position 
//[RequireComponent(typeof(ARRaycastManager))]
//public class PlaceObject : MonoBehaviour
//{
//    public GameObject objectToPlace;
//    //public AudioClip placementAudio;

//    private GameObject spawnedObject;
//    //private ARTrackedImageManager arTrackedImageManager;
//    private ARRaycastManager arRaycastManager;
//    //private ARPlaneManager arPlaneManager;
//    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
//    //private AudioSource audioSource;
//    //private bool objectPlaced = false;

//    void Awake()
//    {
//        //arTrackedImageManager = GetComponent<ARTrackedImageManager>();
//        arRaycastManager = GetComponent<ARRaycastManager>();
//        //arPlaneManager = GetComponent<ARPlaneManager>();
//        //audioSource = gameObject.AddComponent<AudioSource>();
//        //audioSource.clip = placementAudio;
//    }

//    void Update()
//    {
//        // Does not go further down this method if there is already an object placed
//        // if (objectPlaced) return;

//        if (Input.touchCount > 0) //user is currently touching screen if touchCount is more than 0
//        {
//            //Get the first touch
//            Touch touch = Input.GetTouch(0);

//            //Check if touch has just begun
//            if (touch.phase == TouchPhase.Began)
//            {
//                // Perform the raycast to direct where the touch hit in the AR space, hits is a list
//                // When a raycast is performed, the ray may intersect multiple surfaces
//                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
//                {
//                    // Get the hit result
//                    Pose hitPose = hits[0].pose;

//                    if (spawnedObject == null)
//                    {
//                        // Instantiate the object at the hit position
//                        spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
//                        Debug.Log("Object instantiated at " + hitPose.position);
//                        //objectPlaced = true;
//                    }

//                    else
//                    {
//                        spawnedObject.transform.position = hitPose.position;
//                        spawnedObject.transform.rotation = hitPose.rotation;
//                        Debug.Log("Object moved to  " + hitPose.position);
//                    }

//                    // Play the placement audio
//                    //audioSource.Play();

//                    // Set the flag to true to prevent further placement

//                }
//            }
//        }
//    }
//}

//CODE FOR INSTANTIATING WHEN TRACKED IMAGE DETECTED
//using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;

public class PlaceObject : MonoBehaviour
{
    [SerializeField]
    private GameObject[] prefab; //Allow for possibility of instantiating more than one prefab in the future

    public GameObject audioOneGameObjectPrefab;
    private GameObject instantiatedAudioOneGameObject;

    private ARTrackedImageManager trackedImageManager;
    private Camera arCamera;

    private Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {

        // should i instantiate it at awake?
        if (audioOneGameObjectPrefab != null)
        {
            instantiatedAudioOneGameObject = Instantiate(audioOneGameObjectPrefab);
        }
        else
        {
            Debug.LogError("audioOneGameObjectPrefab is not assigned.");
        }

        trackedImageManager = GetComponent<ARTrackedImageManager>();
        arCamera = Camera.main;

        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found. Make sure the Main Camera is tagged as MainCamera.");
        }

        //if (audioClip1 == null)
        //{
        //    Debug.LogError("AudioSource is not in the same Game Object.");
        //    //Debug.LogError("AudioSource is not assigned in the Inspector.");
        //}

        //else
        //{
        //    Debug.Log("AudioSource is assigned in Inspector.");
        //}

    }

    private void OnEnable()
    {
        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
            SpawnPrefab(trackedImage);
        }
    }

    private void SpawnPrefab(ARTrackedImage trackedImage)
    {
        if (instantiatedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            return; // Prefab already spawned for this image
        }

        foreach (GameObject prefab in prefab)
        {
            //Vector3 position = trackedImage.transform.position;
            //Quaternion rotation = trackedImage.transform.rotation;
            Debug.Log("Spawning prefab for tracked image: " + trackedImage.referenceImage.name);

            GameObject instantiatedPrefab = Instantiate(prefab);
            if (instantiatedPrefab != null)
            {
                Debug.Log("Avatar " + instantiatedPrefab.name + " instantiated");
            }
            else
            {
                Debug.Log("Prefab not instantiated");
            }

            // Ensures it is in world space
            instantiatedPrefab.transform.SetParent(null, true);

            // Debugging parent information
            //if (instantiatedPrefab.transform.parent != null)
            //{
            //    Debug.Log("Parent of instantiated prefab: " + instantiatedPrefab.transform.parent.name);
            //}
            //else
            //{
            //    Debug.Log("Instantiated prefab has no parent");
            //}

            instantiatedPrefabs[trackedImage.referenceImage.name] = instantiatedPrefab;

            CallAudioOne();
        }
    }

    private void CallAudioOne()
    {
        AudioOne audioOneScript = instantiatedAudioOneGameObject.GetComponent<AudioOne>();
       
        if (audioOneScript != null)
        {
                audioOneScript.StartAudioOne();
         }
        else
        {
            Debug.LogError("AudioOne script not found on instantiated AudioOne GameObject.");
        }

    }
}
