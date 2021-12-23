using UnityEngine;
using UnityEngine.Events;

namespace Aryzon
{
    // This class shows how to receive and handle reticle  events in your code.
    // Place this component on an object with a renderer, like a cube and its
    // material will change color when the reticle hovers over it.
    // It is recommended to duplicate and edit this code in your own class.

    //[RequireComponent(typeof(AryzonRaycastObject))]
    public class MenuButtonsScript : MonoBehaviour
    {

        private AryzonRaycastObject raycastObject;
        // private VideoPlayer vidPlayer;
        //private Renderer objectRenderer;
        private GameObject menuCanvas;
        private GameObject targetCanvas;

        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();
            //vidPlayer = gameObject.GetComponent<VideoPlayer>();
            menuCanvas = GameObject.Find("CanvasMenu");
            string name = "Canvas" + gameObject.name;
            targetCanvas = GameObject.Find(name);
            targetCanvas.SetActive(false);

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
            menuCanvas.SetActive(false);
            targetCanvas.SetActive(true);
        }

        private void Clicked()
        {

        }
    }
}