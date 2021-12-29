using UnityEngine;
using UnityEngine.Events;
using Aryzon;

public class OpenPdfScript : AryzonRaycastInteractable
{
    public GameObject newTab;

    protected override void OnEnable()
    {
        newTab.SetActive(false);
        base.OnEnable();
    }

    protected override void Down()
    {
        newTab.SetActive(true);
        gameObject.transform.parent.gameObject.SetActive(false);
    }
}
