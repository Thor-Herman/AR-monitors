using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Aryzon;

public class ScrollPdfScript : AryzonRaycastInteractable
{

    public GameObject pdfImage;
    private GameObject currentScrollBar;
    private bool pressedUp;
    private bool isPressing;
    private float y = 5.0f / (2898.0f + 2902.0f) * 284.1f;


    protected override void Awake()
    {
        base.Awake();
        //pdfImage = GameObject.Find("PdfImage");
        currentScrollBar = GameObject.Find("CurrentPositionScrollBar");
    }

    protected override void OnEnable()
    {
        pressedUp = gameObject.name.Equals("UpBtn");
        //progressBar.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 0.0f);
        base.OnEnable();
    }

    //protected override void Off()
    //{
    //    isPressing = false;
    //}

    //protected override void Over()
    //{
    //    isPressing = true;
    //}

    protected override void Down()
    {
        isPressing = true;
    }

    protected override void Up()
    {
        isPressing = false;
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
}