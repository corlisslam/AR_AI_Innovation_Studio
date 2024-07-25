using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class CharacterSpawner1 : MonoBehaviour
{
    // Prefab for the Convai character
    public GameObject _characterPrefab;

    private ARTrackedImageManager _arTrackedImageManager;

    // Event triggered when a character is spawned
    public Action OnCharacterSpawned;

    /// <summary>
    /// Initializes necessary components and finds AR-related managers.
    /// </summary>
    private void Awake()
    {
        // Assuming this script is attached to the XR Origin GameObject
        _arTrackedImageManager = GetComponent<ARTrackedImageManager>();
    }

    private void OnEnable()
    {
        _arTrackedImageManager.trackedImagesChanged += ARTrackedImageManager_TrackedImagesChanged;
    }

    private void OnDisable()
    {
        _arTrackedImageManager.trackedImagesChanged -= ARTrackedImageManager_TrackedImagesChanged;
    }

    /// <summary>
    /// Handles tracked image changes, spawning characters on added images.
    /// </summary>
    /// <param name="eventArgs">Event arguments containing information about tracked image changes.</param>
    private void ARTrackedImageManager_TrackedImagesChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var trackedImage in eventArgs.added)
        {
            // Handle Added Event
            Debug.Log("Tracked image added: " + trackedImage.referenceImage.name);
            GameObject character = SpawnCharacter();
            character.transform.SetParent(null, true);
        }
    }

    private GameObject SpawnCharacter()
    {
        GameObject character = Instantiate(_characterPrefab);

        OnCharacterSpawned?.Invoke();

        return character;
    }

}
