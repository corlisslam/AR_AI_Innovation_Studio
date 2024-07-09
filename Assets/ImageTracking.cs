using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// currently this script detects in a loop if there is an object placed in scene and if there is not, it would place the object at the first touch's position 
public class PlaceObject : MonoBehaviour
{
    public GameObject objectToPlace;
    //public AudioClip placementAudio;

    private GameObject spawnedObject;
    //private ARTrackedImageManager arTrackedImageManager;
    private ARRaycastManager arRaycastManager;
    private List<ARRaycastHit> hits = new List<ARRaycastHit>();
    //private AudioSource audioSource;
    private bool objectPlaced = false;

    void Awake()
    {
        //arTrackedImageManager = GetComponent<ARTrackedImageManager>();
        arRaycastManager = GetComponent<ARRaycastManager>();
        //audioSource = gameObject.AddComponent<AudioSource>();
        //audioSource.clip = placementAudio;
    }

    void Update()
    {
        // Does not go further down this method if there is already an object placed
        // if (objectPlaced) return;

        if (Input.touchCount > 0) //user is currently touching screen if touchCount is more than 0
        {
            //Get the first touch
            Touch touch = Input.GetTouch(0);

            //Check if touch has just begun
            if (touch.phase == TouchPhase.Began)
            {
                // Perform the raycast to direct where the touch hit in the AR space, hits is a list
                // When a raycast is performed, the ray may intersect multiple surfaces
                if (arRaycastManager.Raycast(touch.position, hits, TrackableType.PlaneWithinPolygon))
                {
                    // Get the hit result
                    Pose hitPose = hits[0].pose;

                    if (spawnedObject == null)
                    {
                        // Instantiate the object at the hit position
                        spawnedObject = Instantiate(objectToPlace, hitPose.position, hitPose.rotation);
                        Debug.Log("Object instantiated at " + hitPose.position);
                        objectPlaced = true;
                    }
                    
                    else
                    {
                        spawnedObject.transform.position = hitPose.position;
                        spawnedObject.transform.rotation = hitPose.rotation;
                        Debug.Log("Object moved to " + hitPose.position);
                    }

                    // Play the placement audio
                    //audioSource.Play();

                    // Set the flag to true to prevent further placement
                    
                }
            }
        }
    }
}

//public class SetUpModels : MonoBehaviour
//{
//    [SerializeField]
//    public GameObject[] prefabsToSpawn;

//    private ARTrackedImageManager trackedImageManager;
//    private Camera arCamera;

//    private Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();

//    private void Awake()
//    {
//        trackedImageManager = GetComponent<ARTrackedImageManager>();
//        arCamera = Camera.main;
//        if (arCamera == null)
//        {
//            Debug.LogError("AR Camera not found. Make sure the Main Camera is tagged as MainCamera.");
//        }
//    }

//    private void OnEnable()
//    {
//        trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
//    }

//    private void OnDisable()
//    {
//        trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
//    }

//    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
//    {
//        foreach (ARTrackedImage trackedImage in eventArgs.added)
//        {
//            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
//            SpawnPrefabs(trackedImage);
//        }
//    }

//    private void SpawnPrefabs(ARTrackedImage trackedImage)
//    {
//        if (instantiatedPrefabs.ContainsKey(trackedImage.referenceImage.name))
//        {
//            return; // Prefabs already spawned for this image
//        }

//        foreach (GameObject prefab in prefabsToSpawn)
//        {
//            //Vector3 position = trackedImage.transform.position;
//            //Quaternion rotation = trackedImage.transform.rotation;
//            Debug.Log("Spawning prefab for tracked image: " + trackedImage.referenceImage.name);

//            GameObject instantiatedPrefab = Instantiate(prefab);
//            //instantiatedPrefab.transform.SetParent(null, true);

//            instantiatedPrefabs[trackedImage.referenceImage.name] = instantiatedPrefab;
//        }
//    }
//}
