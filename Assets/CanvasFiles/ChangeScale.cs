using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Aryzon;

public class ChangeScale : AryzonRaycastInteractable
{
    [SerializeField] GameObject screen;
    public float scalingFactor = 0.1f, maxScale = 1.0f;
    Vector3 newScale;

    protected override void Down()
    {
        if (gameObject.name.Equals("MinusBtn"))
        {
            newScale = screen.transform.localScale - new Vector3(1, 1, 1) * scalingFactor;
            if (newScale.x >= scalingFactor) screen.transform.localScale = newScale;
        }
        else if (gameObject.name.Equals("PlusBtn"))
        {
            newScale = screen.transform.localScale + new Vector3(1, 1, 1) * scalingFactor;
            Debug.Log($"Newscale: {newScale.magnitude}");
            if (newScale.x <= maxScale) screen.transform.localScale = newScale;
        }
    }
}
