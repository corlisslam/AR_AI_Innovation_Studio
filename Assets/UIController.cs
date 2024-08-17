using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;
using UnityEngine.SceneManagement;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using UnityEngine.UI;


public class UIController : MonoBehaviour
{

    public GameObject canvasUIPrefab;
    private GameObject instantiatedCanvasUI;

    public Button exitButton;

    void Awake()
    {
        instantiatedCanvasUI = Instantiate(canvasUIPrefab);
        Debug.Log("Canvas UI instantiated, but all buttons disabled.");
        Debug.Log("UI that is assigned to instantiatedCanvasUI is " + instantiatedCanvasUI.name);

        exitButton = instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();

        if (exitButton != null)
        {
            exitButton.onClick.AddListener(ExitGame);
            exitButton.gameObject.SetActive(true);
            Debug.Log("Listener added to Exit button and button is initialized");
        }

        else
        {
            Debug.Log("exitButton cannot be found.");
        }
    }

    void ExitGame()
    {
        if (exitButton != null)
        {
            exitButton.gameObject.SetActive(false);
            Debug.Log("Exit button is deactivated");

            Debug.Log("Loading Home Scene");
            SceneManager.LoadScene("HomeScene");
            LoaderUtility.Deinitialize();
        }
        else
        {
            Debug.LogError("Unable to go home with Home button");
        }
    }
    //public GameObject canvasUIPrefab;
    //private GameObject instantiatedCanvasUI;

    //private Button exitButton;
    //private Button cameraButton;

    //void OnEnable()
    //{
    //    Debug.Log("UIController is enabled.");

    //    instantiatedCanvasUI = Instantiate(canvasUIPrefab);
    //    Debug.Log("Canvas UI instantiated, but all buttons disabled.");
    //    Debug.Log("UI that is assigned to instantiatedCanvasUI is " + instantiatedCanvasUI.name);

    //    exitButton = instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();
    //    cameraButton = instantiatedCanvasUI.transform.Find("CameraButton").GetComponent<Button>();

    //    if (exitButton != null && cameraButton != null)
    //    {
    //        exitButton.onClick.AddListener(ExitGame);
    //        exitButton.gameObject.SetActive(true);
    //        Debug.Log("Listener added to Exit button and button is initialized");
    //        cameraButton.onClick.AddListener(CameraScreen);
    //        cameraButton.gameObject.SetActive(true);
    //        Debug.Log("Listener added to Camera button and button is initialized");
    //    }

    //    else
    //    {
    //        Debug.Log("exitButton and cameraButton cannot be found.");
    //    }

    //}

    //void ExitGame()
    //{
    //    string currentAdditiveScene = FindCurrentAdditiveScene();
    //    // Unload the current additive scene if it exists
    //    if (!string.IsNullOrEmpty(currentAdditiveScene) && SceneManager.GetSceneByName(currentAdditiveScene).isLoaded)
    //    {
    //        exitButton.gameObject.SetActive(false);
    //        Debug.Log("Exit button is deactivated");
    //        cameraButton.gameObject.SetActive(false);
    //        Debug.Log("Camera button is deactivated");

    //        this.gameObject.SetActive(false);
    //        Debug.Log("UIController GameObject is deactivated");

    //        Debug.Log("Unloading additive scene: " + currentAdditiveScene);
    //        SceneManager.UnloadSceneAsync(currentAdditiveScene);
    //        SceneManager.LoadScene("HomeScene");
    //        LoaderUtility.Deinitialize();
    //    }
    //    else
    //    {
    //        Debug.LogError("Unable to go home with Home button");
    //    }
    //}

    //void CameraScreen()
    //{
    //    string currentAdditiveScene = FindCurrentAdditiveScene();
    //    // Unload the current additive scene if it exists
    //    if (!string.IsNullOrEmpty(currentAdditiveScene) && SceneManager.GetSceneByName(currentAdditiveScene).isLoaded)
    //    {
    //        exitButton.gameObject.SetActive(false);
    //        Debug.Log("Exit button is deactivated");
    //        cameraButton.gameObject.SetActive(false);
    //        Debug.Log("Camera button is deactivated");

    //        this.gameObject.SetActive(false);
    //        Debug.Log("UIController GameObject is deactivated");

    //        Debug.Log("Unloading additive scene: " + currentAdditiveScene);
    //        SceneManager.UnloadSceneAsync(currentAdditiveScene);

    //    }
    //    else
    //    {
    //        Debug.LogError("Unable to go home with Home button");
    //    }
    //}


    //private string FindCurrentAdditiveScene()
    //{
    //    GameObject xrOrigin = GameObject.Find("XR Origin (AR Rig)");

    //    if (xrOrigin != null)
    //    {
    //        Scanner scanner = xrOrigin.GetComponent<Scanner>();

    //        if (scanner != null)
    //        {
    //            string currentAdditiveScene = scanner.currentAdditiveScene;
    //            Debug.Log("currentAdditiveScene from Scanner.cs: " + currentAdditiveScene);
    //            return currentAdditiveScene;
    //        }
    //        else
    //        {
    //            Debug.LogError("Scanner.cs not found in XR Origin (Ar Rig).");
    //        }
    //    }
    //    else
    //    {
    //        Debug.LogError("XR Origin (AR Rig) not found in the scene.");
    //    }

    //    return string.Empty;

    //}
}
