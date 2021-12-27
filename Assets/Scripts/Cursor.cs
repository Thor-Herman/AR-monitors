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
        if (mouseX != 0)  // Mouse moved horizontally. Might have moved out of screen. 
            HandleMouseMonitorTransitions();
    }

    private void HandleMouseMonitorTransitions()
    {
        float rightSideBoundary = currentScreen.rect.width / 2;
        float leftSideBoundary = -rightSideBoundary;
        float x = rectTransform.localPosition.x;
        bool isWithinScreenBoundary = x > leftSideBoundary && x < rightSideBoundary;
        if (isWithinScreenBoundary) return;

        bool movedLeft = x < leftSideBoundary;
        UpdateMonitor(movedLeft);
    }

    private void UpdateMonitor(bool moveLeft)
    {
        if (moveLeft && currentScreenIndex != 0) currentScreenIndex--;
        else if (!moveLeft && currentScreenIndex < monitorScreens.Length - 1) currentScreenIndex++;

        currentScreen = monitorScreens[currentScreenIndex].GetComponent<RectTransform>();
        this.gameObject.transform.parent = currentScreen;
        float newCursorXValue = moveLeft ? currentScreen.rect.width / 2 : -currentScreen.rect.width / 2;
        rectTransform.anchoredPosition = new Vector2(newCursorXValue, rectTransform.anchoredPosition.y);
    }

    public static bool RectTransformContainsAnother(RectTransform rectTransform, RectTransform another)
    {
        Vector2 yVector = new Vector2(another.rect.yMax, another.rect.yMin);
        Vector2 xVector = new Vector2(another.rect.xMax, another.rect.xMin);
        return rectTransform.rect.Contains(yVector) && rectTransform.rect.Contains(xVector);
    }
}
