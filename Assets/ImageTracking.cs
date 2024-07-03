using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARImageTracker : MonoBehaviour
{
    private ARTrackedImageManager _trackedImageManager;

    [SerializeField]
    private List<PrefabWithTransform> prefabsToInstantiate; // List of prefabs to instantiate

    private void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        if (_trackedImageManager != null)
        {
            _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged;
        }
    }

    private void OnDisable()
    {
        if (_trackedImageManager != null)
        {
            _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
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
        foreach (PrefabWithTransform item in prefabsToInstantiate)
        {
            GameObject instantiatedPrefab = Instantiate(item.prefab, trackedImage.transform);
            instantiatedPrefab.transform.localPosition = item.positionOffset;
            instantiatedPrefab.transform.localRotation = Quaternion.Euler(item.rotationOffset);
            instantiatedPrefab.transform.localScale = item.scale;
        }
    }
}
