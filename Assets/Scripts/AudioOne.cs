using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioOne : MonoBehaviour
{

    public GameObject canvasUIPrefab;
    private GameObject instantiatedCanvasUI;

    private AudioSource audioClip1;
    private Button playButton;
    private Button pauseButton;
    private Button replayButton;
    private Button exitButton;


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

    }

    // Start is called before the first frame update, after all the Awake() are called
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetUIButtons()
    {
        playButton.gameObject.SetActive(false);  // Hide play button initially
        pauseButton.gameObject.SetActive(true);  // Show pause button initially
        replayButton.gameObject.SetActive(false); // Hide replay button initially
        exitButton.gameObject.SetActive(true);
        Debug.Log("Buttons set up");

        if (playButton != null && pauseButton != null && replayButton != null && exitButton != null)
        {
            Initialize();
        }
    }

    void Initialize()
    {
        playButton.onClick.AddListener(PlayAudio);
        pauseButton.onClick.AddListener(PauseAudio);
        //replayButton.onClick.AddListener(ReplayAudio);
        // Add ExitGame later
        //exitButton.onClick.AddListener(ExitGame);

        playButton.gameObject.SetActive(false);  // Hide play button initially
        pauseButton.gameObject.SetActive(true);  // Show pause button initially
        replayButton.gameObject.SetActive(false); // Hide replay button initially
        exitButton.gameObject.SetActive(true);

        StartAudioOne();
    }

    void StartAudioOne()
    {
        if (audioClip1 != null)
        {
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
        audioClip1.Pause();
        playButton.gameObject.SetActive(true);  // Hide play button initially
        pauseButton.gameObject.SetActive(false);  // Show pause button initially
    }

    void PlayAudio()
    {
        audioClip1.UnPause();
        playButton.gameObject.SetActive(false);  // Hide play button initially
        pauseButton.gameObject.SetActive(true);  // Show pause button initially
    }

}
