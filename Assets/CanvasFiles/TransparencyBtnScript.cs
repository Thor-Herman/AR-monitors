using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Aryzon;

public class TransparencyBtnScript : AryzonRaycastInteractable
{
    public CanvasGroup canvasGroup;

    protected override void Down()
    {
        if (gameObject.name.Equals("MinusBtn") && (canvasGroup.alpha - 0.17 >= 0.15))
        {
            canvasGroup.alpha = canvasGroup.alpha - 0.17f;
        }
        else if(gameObject.name.Equals("PlusBtn") && (canvasGroup.alpha + 0.17 <= 1))
        {
            canvasGroup.alpha = canvasGroup.alpha + 0.17f;
        }
    }
}
