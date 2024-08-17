using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class Scanner : MonoBehaviour
{
    private ARTrackedImageManager _trackedImageManager;
    private Camera arCamera;


    //private ARTrackedImage currentTrackedImage; // To store the currently tracked image

    private string currentAdditiveScene = null;

    public UIController uiController;

    void Awake()
    {
        _trackedImageManager = GetComponent<ARTrackedImageManager>();
        arCamera = Camera.main;

        if (arCamera == null)
        {
            Debug.LogError("AR Camera not found. Make sure the Main Camera is tagged as MainCamera.");
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
            TrackedImageTransform.Instance.position = trackedImage.transform.position;
            TrackedImageTransform.Instance.rotation = trackedImage.transform.rotation;
            TrackedImageTransform.Instance.imageName = trackedImage.referenceImage.name;

            //TrackedImageTransform.Instance.transform.position = TrackedImageTransform.Instance.position;
            //TrackedImageTransform.Instance.transform.rotation = TrackedImageTransform.Instance.rotation;

            //currentTrackedImage = trackedImage; // Store the tracked image reference


            // Unload the current additive scene if it exists when a new tracked image is added
            if (!string.IsNullOrEmpty(currentAdditiveScene) && SceneManager.GetSceneByName(currentAdditiveScene).isLoaded)
            {
                if (currentAdditiveScene == "CharacterTour")
                {
                    Debug.Log("Unloading additive scene: " + currentAdditiveScene);
                    SceneManager.UnloadSceneAsync(currentAdditiveScene);
                    currentAdditiveScene = null;

                    GameObject baseEventSystem = GameObject.Find("EventSystem");
                    if (baseEventSystem != null)
                    {
                        baseEventSystem.SetActive(true);
                        Debug.Log("Base EventSystem re-enabled.");
                    }
                    else
                    {
                        Debug.LogError("Base EventSystem not found.");
                    }

                    Button exitButton = uiController.exitButton;
                    if (exitButton != null)
                    {
                        exitButton.gameObject.SetActive(true);
                        Debug.Log("Base exit button enabled.");
                    }
                    else
                    {
                        Debug.LogError("Base exit button not found.");
                    }
                }

                else
                {
                    Debug.Log("Unloading additive scene: " + currentAdditiveScene);
                    SceneManager.UnloadSceneAsync(currentAdditiveScene);
                    currentAdditiveScene = null;
                }

            }

            
            if (trackedImage.referenceImage.name == "MarkerBall")
            {
                //GameObject uiController = GameObject.Find("UI Controller");
                //if (uiController != null)
                //{
                //    uiController.SetActive(true);
                //    Debug.Log("UI Controller activated after scene load.");
                //}
                //else
                //{
                //    Debug.LogError("UI Controller not found!");
                //}

                GameObject baseEventSystem = GameObject.Find("EventSystem");

                
                Button exitButton = uiController.exitButton;
                if (exitButton != null)
                {
                    exitButton.gameObject.SetActive(false);
                    Debug.Log("Base exit button disabled.");
                }
                else
                {
                    Debug.LogError("Base exit button not found.");
                }

                // Disable the base scene's EventSystem
                if (baseEventSystem != null)
                {
                    baseEventSystem.SetActive(false);
                    Debug.Log("Base EventSystem disabled.");
                }
                else
                {
                    Debug.LogError("Base EventSystem not found.");
                }


                Debug.Log("Tracked image's name is MarkerBall, loading CharacterTour scene");
                SceneManager.LoadSceneAsync("CharacterTour", LoadSceneMode.Additive);
                currentAdditiveScene = "CharacterTour";

            }

            if (trackedImage.referenceImage.name == "MarkerModels")
            {
                //GameObject uiController = GameObject.Find("UI Controller");
                //if (uiController != null)
                //{
                //    uiController.SetActive(true);
                //    Debug.Log("UI Controller activated after scene load.");
                //}
                //else
                //{
                //    Debug.LogError("UI Controller not found!");
                //}

                Debug.Log("Tracked image's name is MarkerModels, loading MainScene scene");
                SceneManager.LoadSceneAsync("MainScene", LoadSceneMode.Additive);
                currentAdditiveScene = "MainScene";

            }
        }
    }

    //void Update()
    //{
    //    if (TrackedImageTransform.Instance != null)
    //    {
    //        Debug.Log("Tracked Image Position: " + TrackedImageTransform.Instance.position);
    //    }
    //}
}
