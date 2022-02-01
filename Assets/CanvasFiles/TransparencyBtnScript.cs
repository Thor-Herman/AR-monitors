using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Aryzon;

public class TransparencyBtnScript : AryzonRaycastInteractable
{
    public CanvasGroup canvasGroup;
    public SpriteRenderer borderImage;

    protected override void Down()
    {
        if (gameObject.name.Equals("MinusBtn") && (canvasGroup.alpha - 0.17 >= 0.15))
        {
            canvasGroup.alpha = canvasGroup.alpha - 0.17f;
            Color tmp = borderImage.color;
            tmp.a = tmp.a - 0.17f;
            borderImage.color = tmp;
        }
        else if (gameObject.name.Equals("PlusBtn") && (canvasGroup.alpha + 0.17 <= 1))
        {
            canvasGroup.alpha = canvasGroup.alpha + 0.17f;
            Color tmp = borderImage.color;
            tmp.a = tmp.a + 0.17f;
            borderImage.color = tmp;
        }
    }
}
