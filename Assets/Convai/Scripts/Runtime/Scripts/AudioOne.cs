using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using UnityEngine.XR.ARFoundation;

public class AudioOne : MonoBehaviour
{

    public GameObject canvasUIPrefab;
    private GameObject instantiatedCanvasUI;

    public GameObject audioTwoGameObjectPrefab;
    private GameObject instantiatedAudioTwoGameObject;

    private AudioSource audioClip1;
    private Button playButton;
    private Button pauseButton;
    private Button replayButton;
    private Button exitButton;
    private Button nextButton;

    private bool wasPlaying = false;
    private bool isPaused = false;


    private void Awake() //Called right after a game object is instantiated
    {
        audioClip1 = GetComponent<AudioSource>();

        if (audioClip1 == null)
        {
            Debug.LogError("AudioSource is not in the same Game Object.");
            //Debug.LogError("AudioSource is not assigned in the Inspector.");
        }

        else
        {
            Debug.Log("AudioSource is assigned in Inspector.");
        }

        instantiatedCanvasUI = Instantiate(canvasUIPrefab);
        Debug.Log("Canvas UI instantiated, but all buttons disabled except for exit.");
        Debug.Log("UI that is assigned to instantiatedCanvasUI is " + instantiatedCanvasUI.name);

        playButton = instantiatedCanvasUI.transform.Find("PlayButton").GetComponent<Button>();
        pauseButton = instantiatedCanvasUI.transform.Find("PauseButton").GetComponent<Button>();
        replayButton = instantiatedCanvasUI.transform.Find("ReplayButton").GetComponent<Button>();
        exitButton = instantiatedCanvasUI.transform.Find("ExitButton").GetComponent<Button>();
        nextButton = instantiatedCanvasUI.transform.Find("NextButton").GetComponent<Button>();

        if (playButton != null && pauseButton != null && replayButton != null && exitButton != null && nextButton != null)
        {
            playButton.onClick.AddListener(PlayAudio);
            pauseButton.onClick.AddListener(PauseAudio);
            replayButton.onClick.AddListener(StartAudioOne);
            nextButton.onClick.AddListener(CallAudioTwo);    
            exitButton.onClick.AddListener(ExitGame);
        }

    }

    // Start is called before the first frame update, after all the Awake() are called
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (wasPlaying == true && !isPaused && !audioClip1.isPlaying)
        {
            wasPlaying = false;
            OnAudioFinished();
        }
    }

    //public void SetUIButtons()
    //{
    //    //playButton.gameObject.SetActive(false);  // Hide play button initially
    //    //pauseButton.gameObject.SetActive(true);  // Show pause button initially
    //    //replayButton.gameObject.SetActive(false); // Hide replay button initially
    //    //exitButton.gameObject.SetActive(true);
    //    //Debug.Log("Buttons set up");

    //    if (playButton != null && pauseButton != null && replayButton != null && exitButton != null)
    //    {
    //        playButton.onClick.AddListener(PlayAudio);
    //        pauseButton.onClick.AddListener(PauseAudio);
    //        replayButton.onClick.AddListener(StartAudioOne);
    //        // Add ExitGame later
    //        //exitButton.onClick.AddListener(ExitGame);
    //        StartAudioOne();
    //    }
    //}

    //void Initialize()
    //{
    //    playButton.onClick.AddListener(PlayAudio);
    //    pauseButton.onClick.AddListener(PauseAudio);
    //    //replayButton.onClick.AddListener(ReplayAudio);
    //    // Add ExitGame later
    //    //exitButton.onClick.AddListener(ExitGame);

    //    playButton.gameObject.SetActive(false);  // Hide play button initially
    //    pauseButton.gameObject.SetActive(true);  // Show pause button initially
    //    replayButton.gameObject.SetActive(false); // Hide replay button initially
    //    exitButton.gameObject.SetActive(true);

    //    StartAudioOne();
    //}

    public void StartAudioOne()
    {
        playButton.gameObject.SetActive(false);  // Hide play button initially
        pauseButton.gameObject.SetActive(true);  // Show pause button initially
        replayButton.gameObject.SetActive(false); // Hide replay button initially
        exitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        Debug.Log("Buttons initialized, pause, next, and exit button is enabled.");

        if (audioClip1 != null)
        {
            wasPlaying = true;
            audioClip1.Play(0);
            Debug.Log("Audio playing? " + audioClip1.isPlaying);
        }
        else
        {
            Debug.LogError("AudioSource is not assigned or is missing.");
        }
    }

    void PauseAudio()
    {
        isPaused = true;
        audioClip1.Pause();
        playButton.gameObject.SetActive(true);  // Hide play button initially
        pauseButton.gameObject.SetActive(false);  // Show pause button initially
    }

    void PlayAudio()
    {
        isPaused = false;
        audioClip1.UnPause();
        playButton.gameObject.SetActive(false);  // Hide play button initially
        pauseButton.gameObject.SetActive(true);  // Show pause button initially
    }

    void OnAudioFinished()
    {
        Debug.Log("Audio clip 1 finished playing.");
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);
    }

    void CallAudioTwo()
    {
        audioClip1.Stop();
        Debug.Log("Audio clip 1 stopped.");

        if (audioTwoGameObjectPrefab != null)
        {
            if (instantiatedCanvasUI != null)
            {
                Destroy(instantiatedCanvasUI);
            }

            instantiatedAudioTwoGameObject = Instantiate(audioTwoGameObjectPrefab);
        }
        else
        {
            Debug.LogError("audioTwoGameObjectPrefab is not assigned.");
        }

        AudioTwo audioTwoScript = instantiatedAudioTwoGameObject.GetComponent<AudioTwo>();

        if (audioTwoScript != null)
        {
            audioTwoScript.StartAudioTwo();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("AudioTwo script not found on instantiated AudioTwo GameObject.");
        }
    }

    void ExitGame()
    {

        SceneManager.LoadScene("HomeScene");
        LoaderUtility.Deinitialize();
        //    StartCoroutine(ExitGameCoroutine());
        //}

        //private IEnumerator ExitGameCoroutine()
        //{
        //    var xrManagerSettings = XRGeneralSettings.Instance.Manager;

        //    // Stop the XR subsystems
        //    xrManagerSettings.StopSubsystems();

        //    // Deinitialize the XR loader and wait until it is complete
        //    xrManagerSettings.DeinitializeLoader();
        //    yield return new WaitUntil(() => xrManagerSettings.activeLoader == null);

        //    Debug.Log("XR loader deinitialized.");

        //    // Load the "HomeScene"
        //    SceneManager.LoadScene("HomeScene");
    }

}
