using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Aryzon;
public class ChangeAudioTimeScript : AryzonRaycastInteractable
{
    public AudioSource audioSource;
    public RectTransform progressBarRectTransform; //world coordinates from 0.0 to 0.7
    public Text currentSongTimeText;
    private Text currentSongTime;
    int maxSeconds = 256;
    float maxWidth = 100.0f;
    float currentScale;
    float hitPos;
    public int screenIndex;

    public void ReceiveHitPosition(Vector3 hitPos)
    {
        this.hitPos = hitPos.x;
    }

    protected override void Down()
    {
        Debug.Log($"Current position {hitPos}");
        float relativePosition = 0.0f;
        if (screenIndex == 1)
        {
            relativePosition = 1f + ((hitPos + 0.2f) / 0.8f);
            Debug.Log(relativePosition);
        }else if(screenIndex == 2)
        {
            relativePosition = (hitPos - 2.5f) / 0.8f;
        }
        int seconds = (int)(relativePosition * maxSeconds);
        audioSource.time = seconds;
        currentScale = seconds * maxWidth / maxSeconds;
        progressBarRectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentScale);
    }

    protected override void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Mouse"))
        {
            this.hitPos = .9f + other.gameObject.transform.position.x; // For some reason it's inverted?
            Down();
        }
    }
}