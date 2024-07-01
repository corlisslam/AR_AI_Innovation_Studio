using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARImageTracker : MonoBehaviour
{
    private ARTrackedImageManager _trackedImageManager;
    public GameObject prefabToInstantiate;

    private void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
    }

    private void OnDisable()
    {
        _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
    }

    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added)
        {
            UpdateARContent(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            UpdateARContent(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // Handle removed images if needed
        }
    }

    private void UpdateARContent(ARTrackedImage trackedImage)
    {
        // Instantiate the prefab at the tracked image's position and rotation
        GameObject instantiatedPrefab = Instantiate(prefabToInstantiate, trackedImage.transform.position, trackedImage.transform.rotation);
        // Make the prefab a child of the tracked image to follow its position and rotation
        instantiatedPrefab.transform.parent = trackedImage.transform;
    }
}