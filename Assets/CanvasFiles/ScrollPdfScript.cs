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
    public class ScrollPdfScript : MonoBehaviour { 

        private AryzonRaycastObject raycastObject;
        private GameObject pdfImage;
        private GameObject currentScrollBar;
        private bool pressedUp;
        private bool isPressing;
        private float y = 5.0f / (2898.0f + 2902.0f) * 284.1f;


        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();
            pdfImage = GameObject.Find("PdfImage");
            currentScrollBar = GameObject.Find("CurrentPositionScrollBar");
        }

        private void OnEnable()
        {
            pressedUp = gameObject.name.Equals("UpBtn");
            //progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.0f);
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
            isPressing = false;
        }

        private void Over()
        {
            isPressing = true;
           
        }
        private void FixedUpdate()
        {
            if (isPressing)
            {
                if (pressedUp)
                {
                    if (pdfImage.GetComponent<RectTransform>().localPosition.y > -2898)
                    {
                        //pdfImage.GetComponent<RectTransform>().localPosition.y += 175;
                        pdfImage.GetComponent<RectTransform>().localPosition -= new Vector3(0, 5, 0);
                        currentScrollBar.GetComponent<RectTransform>().localPosition -= new Vector3(0, y, 0);
                    }
                }
                else
                {
                    if (pdfImage.GetComponent<RectTransform>().localPosition.y < 2902)
                    {
                        //pdfImage.GetComponent<RectTransform>().localPosition.y -= 175;
                        pdfImage.GetComponent<RectTransform>().localPosition += new Vector3(0, 5, 0);
                        currentScrollBar.GetComponent<RectTransform>().localPosition -= new Vector3(0, y, 0);
                    }
                }
            }
        }
        private void Down()
        {
        //    if (pressedUp)
        //    {
        //        if (pdfImage.GetComponent<RectTransform>().localPosition.y > -2898)
        //        {
        //            //pdfImage.GetComponent<RectTransform>().localPosition.y += 175;
        //            pdfImage.GetComponent<RectTransform>().localPosition -= new Vector3(0, 175, 0);
        //        }
        //    }
        //    else
        //    {
        //        if (pdfImage.GetComponent<RectTransform>().localPosition.y < 2902)
        //        {
        //            //pdfImage.GetComponent<RectTransform>().localPosition.y -= 175;
        //            pdfImage.GetComponent<RectTransform>().localPosition += new Vector3(0, 175, 0);
        //        }
        //    }
        }

        private void Clicked()
        {
            //gameObject.GetComponent<VideoPlayer>().Stop();
        }
    }
}