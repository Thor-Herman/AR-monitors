using UnityEngine;
using UnityEngine.Events;
using Aryzon;

public class MenuButtonsScript : AryzonRaycastInteractable
{
    private GameObject menuCanvas;
    private GameObject targetCanvas;

    protected override void Awake()
    {
        base.Awake();
        menuCanvas = GameObject.Find("CanvasMenu");
        string name = "Canvas" + gameObject.name;
        Debug.Log(name);
        targetCanvas = GameObject.Find(name);
        targetCanvas.SetActive(false);
    }

    protected override void Down()
    {
        menuCanvas.SetActive(false);
        targetCanvas.SetActive(true);
    }
}