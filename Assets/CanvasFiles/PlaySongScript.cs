using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Aryzon
{
    // This class shows how to receive and handle reticle  events in your code.
    // Place this component on an object with a renderer, like a cube and its
    // material will change color when the reticle hovers over it.
    // It is recommended to duplicate and edit this code in your own class.

    //[RequireComponent(typeof(AryzonRaycastObject))]
    public class PlaySongScript : MonoBehaviour
    {
        public Sprite playBtn;
        public Sprite stopBtn;
        public GameObject otherBtn;

        private AryzonRaycastObject raycastObject;
        private AudioSource audioPlayer;
        private Text currentSongName;
        private Text currentSongTime;
        private bool displayTime = false;
        private RectTransform progressBar;
        int maxSeconds = 256;
        float maxWidth = 100.0f;
        float currentScale;


        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();
            audioPlayer = GameObject.Find("AudioSource").GetComponent<AudioSource>();
            currentSongName = GameObject.Find("CurrentSongName").GetComponent<Text>();
            currentSongTime = GameObject.Find("CurrentSongTime").GetComponent<Text>();
            progressBar = (RectTransform)GameObject.Find("ProgressBar").GetComponent<Transform>().transform;

        }

        private void OnEnable()
        {
            audioPlayer.Pause();
            Debug.Log("1");
            progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.0f);
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
            audioPlayer.Pause();
            gameObject.GetComponent<Image>().sprite = playBtn;
            otherBtn.GetComponent<Image>().sprite = playBtn;
            currentSongName.GetComponent<Text>().text = "0:00";
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
                int s = t % 60;
                if (s < 10){
                    currentSongTime.GetComponent<Text>().text = t / 60 + ":0" + s;
                }
                else { currentSongTime.GetComponent<Text>().text = t/60 + ":" + s; }

                currentScale = t * maxWidth / maxSeconds;
                progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, currentScale);
            }
        }
        private void Clicked()
        {
            //gameObject.GetComponent<VideoPlayer>().Stop();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Mouse")) // Called when user clicks on this part
            {
                Down();
            }
        }
    }
}