using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Aryzon;

public class PlaySongScript : AryzonRaycastInteractable
{
    public Sprite playBtn;
    public Sprite stopBtn;
    public GameObject otherBtn;

    private AudioSource audioPlayer;
    private Text currentSongName;
    private Text currentSongTime;
    private bool displayTime = false;
    private RectTransform progressBar;
    int maxSeconds = 256;
    float maxWidth = 100.0f;
    float currentScale;
    bool ended = false;


    protected override void Awake()
    {
        base.Awake();
        audioPlayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        currentSongName = GameObject.Find("CurrentSongName").GetComponent<Text>();
        currentSongTime = GameObject.Find("CurrentSongTime").GetComponent<Text>();
        progressBar = (RectTransform)GameObject.Find("ProgressBar").GetComponent<Transform>().transform;

    }

    protected override void OnEnable()
    {
        audioPlayer.Pause();
        Debug.Log("1");
        progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.0f);
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        audioPlayer.Pause();
        gameObject.GetComponent<Image>().sprite = playBtn;
        otherBtn.GetComponent<Image>().sprite = playBtn;
        currentSongName.GetComponent<Text>().text = "0:00";
        base.OnDisable();
    }

    protected override void Down()
    {
        if (audioPlayer.isPlaying)
        {
            audioPlayer.Pause();
            gameObject.GetComponent<Image>().sprite = playBtn;
            otherBtn.GetComponent<Image>().sprite = playBtn;
        }
        else
        {
            currentSongName.GetComponent<Text>().text = "Tokyo Drift - Teriyaki Boyz";
            displayTime = true;
            audioPlayer.Play();
            gameObject.GetComponent<Image>().sprite = stopBtn;
            otherBtn.GetComponent<Image>().sprite = stopBtn;
        }
    }

    private void FixedUpdate()
    {
        if (displayTime)
        {
            int t = (int)audioPlayer.time;
            if (t == 256 && !ended)
            {
                ended = true;
            }
            else if (t == 256 && ended)
            {
                audioPlayer.Pause();
                currentSongTime.GetComponent<Text>().text = "0:00";
                audioPlayer.time = 0;
                gameObject.GetComponent<Image>().sprite = playBtn;
                otherBtn.GetComponent<Image>().sprite = playBtn;
                ended = false;
            }
            else
            {
                int s = t % 60;
                if (s < 10)
                {
                    currentSongTime.GetComponent<Text>().text = t / 60 + ":0" + s;
                }
                else 
                { 
                    currentSongTime.GetComponent<Text>().text = t / 60 + ":" + s; 
                }
            }

            currentScale = t * maxWidth / maxSeconds;
            progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentScale);
        }
    }
}
