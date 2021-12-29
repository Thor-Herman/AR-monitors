using UnityEngine;
using UnityEngine.Events;
using Aryzon;


public class CloseTabScript : AryzonRaycastInteractable
{
    public bool isNewTab;
    private GameObject menuCanvas;
    public GameObject displayContent;

    protected override void Awake()
    {
        base.Awake();
        menuCanvas = GameObject.Find("CanvasMenu");
        Debug.Log(gameObject.transform.parent.parent.gameObject);
    }

    protected override void Down()
    {
        if (!isNewTab)
        {
            gameObject.transform.parent.parent.gameObject.SetActive(false);
            menuCanvas.SetActive(true);
        }
        else
        {
            gameObject.transform.parent.gameObject.SetActive(false);
            displayContent.transform.gameObject.SetActive(true);
        }
    }
}