using System.IO.Pipes;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using Aryzon;

public class VideoStarte : AryzonRaycastInteractable
{
    private VideoPlayer vidPlayer;

    protected override void Awake()
    {
        base.Awake();
        vidPlayer = gameObject.GetComponent<VideoPlayer>();
        vidPlayer.Pause();
    }

    protected override void Down()
    {
        if (vidPlayer.isPlaying)
        {
            vidPlayer.Pause();
        }
        else
        {
            vidPlayer.Play();
        }
    }
}