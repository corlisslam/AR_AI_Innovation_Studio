using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioTwo : MonoBehaviour
{

    public GameObject canvasUIPrefab;
    private GameObject instantiatedCanvasUI;

    private AudioSource audioClip2;
    private Button playButton;
    private Button pauseButton;
    private Button replayButton;
    private Button exitButton;
    private Button nextButton;

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

        if (playButton != null && pauseButton != null && replayButton != null && exitButton != null)
        {
            playButton.onClick.AddListener(PlayAudio);
            pauseButton.onClick.AddListener(PauseAudio);
            replayButton.onClick.AddListener(StartAudioTwo);
            // Add ExitGame later
            //exitButton.onClick.AddListener(ExitGame);
            //StartAudioTwo();
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
        Debug.Log("Buttons initialized, pause and exit button is enabled.");

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

}
