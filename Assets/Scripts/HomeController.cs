using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;

public class HomeController : MonoBehaviour
{
    public GameObject homeUIPrefab;
    private GameObject instantiatedHomeUI;
    private Button cameraButton;

    private void Awake()
    {
        instantiatedHomeUI = Instantiate(homeUIPrefab);
        Debug.Log("Home UI instantiated, start button showing.");
        Debug.Log("UI that is assigned to instantiatedHomeUI is " + instantiatedHomeUI.name);

        cameraButton = instantiatedHomeUI.transform.Find("CameraButton").GetComponent<Button>();

        cameraButton.onClick.AddListener(OnCameraButtonPressed);
    }

    public void OnCameraButtonPressed()
    {
        Debug.Log("Loading MainScanner scene");
        LoaderUtility.Initialize();
        SceneManager.LoadScene("MainScanner");
        //SceneManager.LoadScene("MainScene");

        //    StartCoroutine(InitializeLoaderAndLoadScene());
        //    //SceneManager.LoadScene("MainScene");
        //}

        //private IEnumerator InitializeLoaderAndLoadScene()
        //{
        //    var xrManagerSettings = XRGeneralSettings.Instance.Manager;

        //    // Initialize the XR loader and wait until it is complete
        //    xrManagerSettings.InitializeLoader();
        //    yield return new WaitUntil(() => xrManagerSettings.isInitializationComplete);

        //    // Check if the XR loader was successfully initialized
        //    if (xrManagerSettings.activeLoader == null)
        //    {
        //        Debug.LogError("Failed to initialize XR loader.");
        //        yield break; // Exit the coroutine if initialization failed
        //    }

        //    // Start the XR subsystems
        //    xrManagerSettings.StartSubsystems();

        //    // Load the AR scene
        //    SceneManager.LoadScene("MainScene");
    }
}
 

