using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Aryzon;
public class ChangeAudioTimeScript : AryzonRaycastInteractable
{
    private AudioSource audioPlayer;
    private RectTransform progressBar; //world coordinates from 0.0 to 0.7
    private Text currentSongTime;  
    int maxSeconds = 256;
    float maxWidth = 100.0f;
    float currentScale;
    Vector3 hitPos;

    public void ReceiveHitPosition(Vector3 hitPos)
    {
        this.hitPos = hitPos;
    }
    protected override void Awake()
    {
        base.Awake();
        audioPlayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
        progressBar = (RectTransform)GameObject.Find("ProgressBar").GetComponent<Transform>().transform;
        currentSongTime = GameObject.Find("CurrentSongTime").GetComponent<Text>();
    }
    protected override void Down()
    {
        float relativePosition = hitPos.x / 0.7f;
        int seconds = (int)(relativePosition * maxSeconds);
        audioPlayer.time = seconds;
        currentScale = seconds * maxWidth / maxSeconds;
        progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentScale);
    }
}