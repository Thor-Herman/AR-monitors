using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cursor : MonoBehaviour
{

    [Tooltip("First monitor in array will be the default starting one.")]
    [SerializeField] GameObject[] monitorScreens;
    int currentScreenIndex = 0; // Index of current screen

    RectTransform rectTransform;
    RectTransform currentScreen;
    [SerializeField] float mouseSpeed = 1.5f;
    bool mouseOnThisCanvas = true;

    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        currentScreen = monitorScreens[currentScreenIndex].GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");
        rectTransform.anchoredPosition = new Vector2(rectTransform.anchoredPosition.x + mouseX * mouseSpeed, rectTransform.anchoredPosition.y + mouseY * mouseSpeed);
        if (mouseX != 0)
        {
            // Mouse moved horizontally. Might have moved out of screen. 
            HandleMouseMonitorTransitions();
        }
        Debug.Log(RectTransformUtility.RectangleContainsScreenPoint(currentScreen, rectTransform.position));
        Debug.Log($"{rectTransform.localPosition} {currentScreen.rect.width}");
    }

    private void HandleMouseMonitorTransitions()
    {
        float leftSideBoundary = -currentScreen.rect.width / 2;
        float rightSideBoundary = currentScreen.rect.width / 2;
        float x = rectTransform.localPosition.x;

        if (x < leftSideBoundary && currentScreenIndex != 0) currentScreenIndex--;
        else if (x > rightSideBoundary && currentScreenIndex < monitorScreens.Length - 1) currentScreenIndex++;

        UpdateMonitor();
    }

    private void UpdateMonitor()
    {
        currentScreen = monitorScreens[currentScreenIndex].GetComponent<RectTransform>();
        this.gameObject.transform.parent = currentScreen;
    }

    public static bool RectTransformContainsAnother(RectTransform rectTransform, RectTransform another)
    {
        Vector2 yVector = new Vector2(another.rect.yMax, another.rect.yMin);
        Vector2 xVector = new Vector2(another.rect.xMax, another.rect.xMin);
        return rectTransform.rect.Contains(yVector) && rectTransform.rect.Contains(xVector);
    }
}
