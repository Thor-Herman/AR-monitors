using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Aryzon
{
    public class ChangeAudioTimeScript : MonoBehaviour
    {
        private AryzonRaycastObject raycastObject;
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
        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();
            audioPlayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
            progressBar = (RectTransform)GameObject.Find("ProgressBar").GetComponent<Transform>().transform;
            currentSongTime = GameObject.Find("CurrentSongTime").GetComponent<Text>();
        }

        private void OnEnable()
        {
            if (raycastObject)
            {
                raycastObject.OnPointerOff.AddListener(Off);
                raycastObject.OnPointerOver.AddListener(Over);
                raycastObject.OnPointerUp.AddListener(Clicked);
                raycastObject.OnPointerDown.AddListener(Down);
            }
        }

        private void OnDisable()
        {
            if (raycastObject)
            {
                raycastObject.OnPointerOff.RemoveListener(Off);
                raycastObject.OnPointerOver.RemoveListener(Over);
                raycastObject.OnPointerUp.RemoveListener(Clicked);
                raycastObject.OnPointerDown.RemoveListener(Down);
            }
        }

        private void Off()
        {

        }

        private void Over()
        {

        }

        private void Down()
        {
            float relativePosition = hitPos.x / 0.7f;
            int seconds = (int) (relativePosition * maxSeconds);
            audioPlayer.time = seconds;
            currentScale = seconds * maxWidth / maxSeconds;
            progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentScale);
        }

        private void FixedUpdate()
        {

        }
        private void Clicked()
        {

        }
    }
}