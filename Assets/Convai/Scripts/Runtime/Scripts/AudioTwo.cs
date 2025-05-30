using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.XR.Management;
using UnityEngine.XR.ARFoundation;

public class AudioTwo : MonoBehaviour
{

    public GameObject canvasUIPrefab;
    private GameObject instantiatedCanvasUI;

    public GameObject audioOneGameObjectPrefab;
    private GameObject instantiatedAudioOneGameObject;

    private AudioSource audioClip2;
    private Button playButton;
    private Button pauseButton;
    private Button replayButton;
    private Button exitButton;
    private Button nextButton;
    private Button backButton;

    private bool wasPlaying = false;
    private bool isPaused = false;


    private void Awake() //Called right after a game object is instantiated
    {
        audioClip2 = GetComponent<AudioSource>();

        if (audioClip2 == null)
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
        backButton = instantiatedCanvasUI.transform.Find("BackButton").GetComponent<Button>();

        if (playButton != null && pauseButton != null && replayButton != null && exitButton != null && backButton != null)
        {
            playButton.onClick.AddListener(PlayAudio);
            pauseButton.onClick.AddListener(PauseAudio);
            replayButton.onClick.AddListener(StartAudioTwo);
            exitButton.onClick.AddListener(ExitGame);
            backButton.onClick.AddListener(CallAudioOne);
        }

    }

    // Start is called before the first frame update, after all the Awake() are called
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (wasPlaying == true && !isPaused && !audioClip2.isPlaying)
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

    public void StartAudioTwo()
    {
        playButton.gameObject.SetActive(false);  // Hide play button initially
        pauseButton.gameObject.SetActive(true);  // Show pause button initially
        replayButton.gameObject.SetActive(false); // Hide replay button initially
        exitButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(false);
        backButton.gameObject.SetActive(true);
        Debug.Log("Buttons initialized, pause, back, and exit button is enabled.");

        if (audioClip2 != null)
        {
            wasPlaying = true;
            audioClip2.Play(0);
            Debug.Log("Audio playing? " + audioClip2.isPlaying);
        }
        else
        {
            Debug.LogError("AudioSource is not assigned or is missing.");
        }
    }

    void PauseAudio()
    {
        isPaused = true;
        audioClip2.Pause();
        playButton.gameObject.SetActive(true);  // Hide play button initially
        pauseButton.gameObject.SetActive(false);  // Show pause button initially
    }

    void PlayAudio()
    {
        isPaused = false;
        audioClip2.UnPause();
        playButton.gameObject.SetActive(false);  // Hide play button initially
        pauseButton.gameObject.SetActive(true);  // Show pause button initially
    }

    void OnAudioFinished()
    {
        Debug.Log("Audio clip finished playing.");
        playButton.gameObject.SetActive(false);
        pauseButton.gameObject.SetActive(false);
        replayButton.gameObject.SetActive(true);
    }

    void CallAudioOne()
    {
        audioClip2.Stop();
        Debug.Log("Audio clip 2 stopped.");

        if (audioOneGameObjectPrefab != null)
        {

            if (instantiatedCanvasUI != null)
            {
                Destroy(instantiatedCanvasUI);
            }

            instantiatedAudioOneGameObject = Instantiate(audioOneGameObjectPrefab);
        }
        else
        {
            Debug.LogError("audioOneGameObjectPrefab is not assigned.");
        }

        AudioOne audioOneScript = instantiatedAudioOneGameObject.GetComponent<AudioOne>();

        if (audioOneScript != null)
        {
            audioOneScript.StartAudioOne();
            Destroy(gameObject);
        }
        else
        {
            Debug.LogError("AudioOne script not found on instantiated AudioOne GameObject.");
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
