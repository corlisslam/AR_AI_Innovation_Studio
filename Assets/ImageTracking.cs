using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class SetUpModels : MonoBehaviour
{
    [SerializeField]
    public GameObject[] prefabsToSpawn;

    private ARTrackedImageManager trackedImageManager;
    private Camera arCamera;

    private Dictionary<string, GameObject> instantiatedPrefabs = new Dictionary<string, GameObject>();

    private void Awake()
    {
        trackedImageManager = GetComponent<ARTrackedImageManager>();
        arCamera = Camera.main;
        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found. Make sure the Main Camera is tagged as MainCamera.");
        }
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
            SpawnPrefabs(trackedImage);
        }
    }

    private void SpawnPrefabs(ARTrackedImage trackedImage)
    {
        if (instantiatedPrefabs.ContainsKey(trackedImage.referenceImage.name))
        {
            return; // Prefabs already spawned for this image
        }

        foreach (GameObject prefab in prefabsToSpawn)
        {
            //Vector3 position = trackedImage.transform.position;
            //Quaternion rotation = trackedImage.transform.rotation;
            Debug.Log("Spawning prefab for tracked image: " + trackedImage.referenceImage.name);

            GameObject instantiatedPrefab = Instantiate(prefab);
            //instantiatedPrefab.transform.SetParent(null, true);

            instantiatedPrefabs[trackedImage.referenceImage.name] = instantiatedPrefab;
        }
    }
}
