// This code is attached to the XR Orirgin game object because it is added as a component to it
using System.Collections; 
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;


public class ARImageTracker : MonoBehaviour // Defining new class inheriting from MonoBehav, which is smthing for Unity
{
    private ARTrackedImageManager _trackedImageManager; // Define private variable ofthe type ARTrackedImageManager

    [SerializeField] // Allows you to modify the values directly in the editor, even while the game is running
    private List<PrefabWithTransform> prefabsToInstantiate; // List of prefabs to instantiate

    [SerializeField] // A field that references the AR camera in editor
    private Camera arCamera; // Reference to the AR camera

    private void Awake() // This method is called when the script is loaded, private void just means private for methods
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>(); // Initialize _trackedImageManager to the component attached to game obejct, is it now an instance of the ARTrackedImageManager class
        if (arCamera == null) // If arCamera is not set, set it to the main camera in the scene
        {
            arCamera = Camera.main; // Optionally find the main camera if not set
        }
    }

    private void OnEnable() // This method is called when the script is enabled
    {
        if (_trackedImageManager != null)
        {
            _trackedImageManager.trackedImagesChanged += OnTrackedImagesChanged; // The += operator is used to subscribe to OnTrackedImagesChanged method to the trackedImagesChanged event, so that the method is called when the event is raised.
        } // TrackedImagesChanged is an event of the ARTrackedImageManager class
    }

    private void OnDisable() // This method called when the script is disabled
    {
        if (_trackedImageManager != null)
        {
            _trackedImageManager.trackedImagesChanged -= OnTrackedImagesChanged;
        }
    }

    // OnTrackedImagesChanged is an event handler method
    // ARTrackedImagesChangedEventArgs is a class that contains information about the tracked images that have changed
    private void OnTrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs) // Event handler called when tracked images change (added, updated, removed)
    // eventArgs is an instance of ARTrackedImagesChangedEventArgs
    {
        foreach (ARTrackedImage trackedImage in eventArgs.added) //Iterates through each newly added tracked image and updates the AR content
        // ARTrackedImage is a list of objects that were added
        {
            UpdateARContent(trackedImage);
        }

        foreach (ARTrackedImage trackedImage in eventArgs.updated)
        {
            // No updates yet
        }

        foreach (ARTrackedImage trackedImage in eventArgs.removed)
        {
            // Handle removed images if needed
        }
    }

    private void UpdateARContent(ARTrackedImage trackedImage) // This method updates the AR content for a given tracked image.
    {
        foreach (PrefabWithTransform item in prefabsToInstantiate)
        {
            // GameObject instantiatedPrefab = Instantiate(item.prefab, trackedImage.transform);
            GameObject instantiatedPrefab = Instantiate(item.prefab, arCamera.transform); // Instantiates a prefab and sets its parent to the AR camera's transform.
        
            // Apply position offset relative to the camera's position
            instantiatedPrefab.transform.localPosition = item.positionOffset;

            // Apply rotation offset
            instantiatedPrefab.transform.localRotation = Quaternion.Euler(item.rotationOffset);

            // Apply scale
            instantiatedPrefab.transform.localScale = item.scale;

            // Unparent, so that the instantiated objects will stay fixed to their location
            instantiatedPrefab.transform.SetParent(null, true);

            // Debugging logs
            Debug.Log($"Instantiated Prefab: {item.prefab.name}");
            Debug.Log($"Position Offset: {item.positionOffset}");
            Debug.Log($"Rotation Offset: {item.rotationOffset}");
            Debug.Log($"Scale: {item.scale}");
            Debug.Log($"Final Position: {instantiatedPrefab.transform.localPosition}");
            Debug.Log($"Final Rotation: {instantiatedPrefab.transform.localRotation.eulerAngles}");
            Debug.Log($"Final Scale: {instantiatedPrefab.transform.localScale}");

        }
    }
}
