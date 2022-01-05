using System.IO.Pipes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.UI;
using Aryzon;

public class VideoStarte : AryzonRaycastInteractable
{
    private VideoPlayer vidPlayer;
    public Sprite playBtn;
    public Sprite stopBtn;

    protected override void OnEnable()
    {
        vidPlayer = gameObject.transform.parent.gameObject.GetComponent<VideoPlayer>();
        vidPlayer.Pause();
        base.OnEnable();
    }

    protected override void OnDisable()
    {
        gameObject.GetComponent<Image>().sprite = playBtn;
        base.OnDisable();
    }

    protected override void Down()
    {
        if (vidPlayer.isPlaying)
        {
            vidPlayer.Pause();
            gameObject.GetComponent<Image>().sprite = playBtn;
        }
        else
        {
            vidPlayer.Play();
            gameObject.GetComponent<Image>().sprite = stopBtn;
        }
    }

    private void FixedUpdate()
    {
        if (!vidPlayer.isPlaying)
        {
            gameObject.GetComponent<Image>().sprite = playBtn;
        }
    }
}