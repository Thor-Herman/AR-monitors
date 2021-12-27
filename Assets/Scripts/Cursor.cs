using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{

    [Tooltip("First monitor in array will be the default starting one.")]
    [SerializeField] GameObject[] monitorScreens;

    RectTransform rectTransform;
    RectTransform currentScreen;
    [SerializeField] float mouseSpeed = 1.5f;
    bool mouseOnThisCanvas = true;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentScreen = monitorScreens[0].GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Mouse X");
        float vertical = Input.GetAxisRaw("Mouse Y");
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + horizontal * mouseSpeed, rectTransform.anchoredPosition.y + vertical * mouseSpeed);
        Debug.Log(RectTransformUtility.RectangleContainsScreenPoint(currentScreen, rectTransform.position));
        Debug.Log($"{rectTransform.localPosition} {currentScreen.position}");
    }

    public static bool RectTransformContainsAnother(RectTransform rectTransform, RectTransform another)
    {
        Vector2 yVector = new Vector2(another.rect.yMax, another.rect.yMin);
        Vector2 xVector = new Vector2(another.rect.xMax, another.rect.xMin);
        return rectTransform.rect.Contains(yVector) && rectTransform.rect.Contains(xVector);
    }
}
