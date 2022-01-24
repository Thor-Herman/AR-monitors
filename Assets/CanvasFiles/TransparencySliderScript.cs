using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransparencySliderScript : MonoBehaviour
{
    //private Slider slider;
    public CanvasGroup canvasGroup;

    void Update()
    {
        canvasGroup.alpha = gameObject.GetComponent<Slider>().value;
    }
}
