using UnityEngine;
using UnityEngine.Events;
using Aryzon;

public class MenuButtonsScript : AryzonRaycastInteractable
{
    private GameObject menuCanvas;
    [SerializeField] private GameObject targetCanvas;

    protected override void Awake()
    {
        base.Awake();
        menuCanvas = GameObject.Find("CanvasMenu");
        targetCanvas.SetActive(false);
    }

    protected override void Down()
    {
        menuCanvas.SetActive(false);
        targetCanvas.SetActive(true);
    }
}