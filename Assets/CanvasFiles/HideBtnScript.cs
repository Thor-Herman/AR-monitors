using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Aryzon;

public class HideBtnScript : AryzonRaycastInteractable
{
    private GameObject currentCanvas;
    public GameObject hiddenCanvas;
    int counter = 0;
    bool clicked = false;
    public HideBtnScript hideBtnScript;

    void Start()
    {
        hiddenCanvas.SetActive(false);
    }
     

    public void UpdateCurrentCanvas(GameObject canvas)
    {
        currentCanvas = canvas;
    }

    protected override void Down()
    {
        if (!clicked)
        {
            if (gameObject.name.Equals("HideBtn") && (!gameObject.name.Equals("UnhideBtn") || !gameObject.name.Equals("UnhideBtn1")))
            {
                currentCanvas = gameObject.transform.parent.gameObject;
                currentCanvas.SetActive(false);
                hiddenCanvas.SetActive(true);
                hideBtnScript.UpdateCurrentCanvas(currentCanvas);
                Debug.Log("ola");
                clicked = true;
            }
            else
            {
                hiddenCanvas.SetActive(false);
                currentCanvas.SetActive(true);
                clicked = true;
            }
        }
    }

    void FixedUpdate()
    {
        if(clicked)
        {
            counter = (counter + 1) % 2;
            if(counter == 0)
            {
                clicked = false;
            }
        }
    }
}
