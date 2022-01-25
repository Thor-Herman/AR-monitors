using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Aryzon;

public class HideBtnScript : AryzonRaycastInteractable
{
    private GameObject currentCanvas;
    public GameObject hiddenCanvas;
    int counter = 0;
    bool clicked = false;
    public HideBtnScript hideBtnScript;
    public Collider hiddenCanvasCollider;
    public Image hiddenCanvasImage;

    void Start()
    {
        hiddenCanvasCollider.enabled = false;
        hiddenCanvasImage.enabled = false;
    }


    public void UpdateCurrentCanvas(GameObject canvas)
    {
        currentCanvas = canvas;
    }

    protected override void Down()
    {
        if (!clicked)
        {
            if (gameObject.name.Equals("HideBtn") && gameObject.active)
            {
                currentCanvas = gameObject.transform.parent.gameObject;
                hideBtnScript.UpdateCurrentCanvas(currentCanvas);
                StartCoroutine(WaitASecond());
            }
            else
            {
                hiddenCanvasCollider.enabled = false;
                hiddenCanvasImage.enabled = false;
                currentCanvas.SetActive(true);
                // clicked = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (clicked)
        {
            counter = (counter + 1) % 20;
            if (counter == 0)
            {
                clicked = false;
                hiddenCanvasCollider.enabled = true;
                Debug.Log("ENABLED COLLIDER");
            }
        }
    }

    IEnumerator WaitASecond()
    {
        Debug.Log("This will show up");
        yield return new WaitForSeconds(.11f);
        hiddenCanvasCollider.enabled = true;
        hiddenCanvasImage.enabled = true;
        Debug.Log("This isn't gonna show up");
        Debug.Log("ola");
        clicked = true;
        currentCanvas.SetActive(false);
    }
}
