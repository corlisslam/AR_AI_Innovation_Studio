using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using UnityEngine.UI;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class HomeController : MonoBehaviour
{
    [SerializeField] private GameObject homePageButtonGameObject;
    private Button homePageButton;

    [SerializeField] private TMP_InputField homePageInputField;
    private string playerName;

    private void Awake()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        if (homePageButtonGameObject == null)
        {
            Debug.LogError("homePageButtonGameObject is not assigned!");
        }

        homePageButton = homePageButtonGameObject.GetComponent<Button>();
        homePageButton.onClick.AddListener(OnScanImageButtonPressed);
    }

    public void OnScanImageButtonPressed()
    {
        Debug.Log("SCAN IMAGE pressed");

        playerName = homePageInputField.text;

        if (!string.IsNullOrEmpty(playerName))
        {
            Debug.Log("Player Name Entered: " + playerName);
            PlayerPrefs.SetString("PlayerName", playerName);
        }
        else
        {
            Debug.Log("Player name is empty!");
            PlayerPrefs.SetString("PlayerName", "Visitor");
            Debug.Log("Player name set to Visitor.");
        }

        StartCoroutine(LoadScene());
    }

    public IEnumerator LoadScene()
    {
        Debug.Log("Loading MainScanner scene");
        LoaderUtility.Initialize();

        AsyncOperation loadOperation = SceneManager.LoadSceneAsync("MainScanner");

        float elapsedTime = 0f;
        float timeout = 10f;

        while (!loadOperation.isDone)
        {
            if (elapsedTime >= timeout)
            {
                Debug.LogError("Scene loading timed out.");
                LoaderUtility.Deinitialize();
                PlayerPrefs.SetString("PlayerName", "Visitor");
                yield break;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Debug.Log("Loaded MainScanner scene");
    }
}

//using System.Collections;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.XR.Management;
//using UnityEngine.UI;
//using UnityEngine.XR.ARFoundation;
//using TMPro;

//public class HomeController : MonoBehaviour
//{
//    [SerializeField] private GameObject homePageButtonGameObject;
//    private Button homePageButton;

//    [SerializeField] private TMP_InputField homePageInputField;
//    private string playerName;

//    private void Awake()
//    {
//        if (homePageButtonGameObject != null)
//        {
//            homePageButton = homePageButtonGameObject.GetComponent<Button>();
//            homePageButton.onClick.AddListener(OnScanImageButtonPressed);
//        }
//        else
//        {
//            Debug.LogError("homePageButtonGameObject is not assigned!");
//        }
//    }

//    public void OnScanImageButtonPressed()
//    {
//        Debug.Log("SCAN IMAGE pressed");

//        playerName = homePageInputField.text;

//        if (!string.IsNullOrEmpty(playerName))
//        {
//            Debug.Log("Player Name Entered: " + playerName);
//            PlayerPrefs.SetString("PlayerName", playerName);
//        }
//        else
//        {
//            Debug.LogError("Player name is empty!");
//            PlayerPrefs.SetString("PlayerName", "Visitor");
//            Debug.Log("Player name set to Visitor.");
//        }

//        LoadScene();
//        //SceneManager.LoadScene("MainScene");

//        //    StartCoroutine(InitializeLoaderAndLoadScene());
//        //    //SceneManager.LoadScene("MainScene");
//        //}

//        //private IEnumerator InitializeLoaderAndLoadScene()
//        //{
//        //    var xrManagerSettings = XRGeneralSettings.Instance.Manager;

//        //    // Initialize the XR loader and wait until it is complete
//        //    xrManagerSettings.InitializeLoader();
//        //    yield return new WaitUntil(() => xrManagerSettings.isInitializationComplete);

//        //    // Check if the XR loader was successfully initialized
//        //    if (xrManagerSettings.activeLoader == null)
//        //    {
//        //        Debug.LogError("Failed to initialize XR loader.");
//        //        yield break; // Exit the coroutine if initialization failed
//        //    }

//        //    // Start the XR subsystems
//        //    xrManagerSettings.StartSubsystems();

//        //    // Load the AR scene
//        //    SceneManager.LoadScene("MainScene");
//    }

//    public void LoadScene()
//    {
//        Debug.Log("Loading MainScanner scene");
//        LoaderUtility.Initialize();
//        SceneManager.LoadScene("MainScanner");
//    }
//}




