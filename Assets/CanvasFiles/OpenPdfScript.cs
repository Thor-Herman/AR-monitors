using UnityEngine;
using UnityEngine.Events;

namespace Aryzon
{
    // This class shows how to receive and handle reticle  events in your code.
    // Place this component on an object with a renderer, like a cube and its
    // material will change color when the reticle hovers over it.
    // It is recommended to duplicate and edit this code in your own class.

    //[RequireComponent(typeof(AryzonRaycastObject))]
    public class OpenPdfScript : MonoBehaviour
    {
        private AryzonRaycastObject raycastObject;
        // private VideoPlayer vidPlayer;
        //private Renderer objectRenderer;
        public GameObject newTab;

        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();
            //vidPlayer = gameObject.GetComponent<VideoPlayer>();


        }

        private void OnEnable()
        {
            newTab.SetActive(false);
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
            newTab.SetActive(true);
            gameObject.transform.parent.gameObject.SetActive(false);

        }

        private void Clicked()
        {

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