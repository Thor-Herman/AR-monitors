using UnityEngine;
using UnityEngine.Events;
using Aryzon;


public class CloseTabScript : AryzonRaycastInteractable
{
    public bool isNewTab;
    private GameObject menuCanvas;
    private GameObject parentCanvas;
    public GameObject displayContent;

    protected override void Awake()
    {
        base.Awake();
        parentCanvas = gameObject.transform.parent.parent.gameObject;
        menuCanvas = parentCanvas.transform.parent.Find("CanvasMenu").gameObject;
    }

    protected override void Down()
    {
        if (!isNewTab)
        {
            parentCanvas.SetActive(false);
            if (menuCanvas == null) menuCanvas = parentCanvas.transform.parent.Find("CanvasMenu").gameObject;
            menuCanvas.SetActive(true);
        }
        else
        {
            transform.parent.gameObject.SetActive(false);
            displayContent.transform.gameObject.SetActive(true);
        }
    }
}