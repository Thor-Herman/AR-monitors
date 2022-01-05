using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class Cursor : MonoBehaviour
{

    [Tooltip("First monitor in array will be the default starting one.")]
    [SerializeField] GameObject[] monitorScreens;
    int currentScreenIndex = 0; // Index of current screen

    RectTransform rectTransform;
    RectTransform newScreen;
    Collider boxCollider;
    [SerializeField] float mouseSpeed = 1.5f;
    bool mouseOnThisCanvas = true;

    float prevMouseX, prevMouseY;

    // Start is called before the first frame update
    void Start()
    {
        Input.simulateMouseWithTouches = false;
        // UnityEngine.Cursor.lockState = CursorLockMode.Locked; !!!! ENABLE THIS LINE WHEN PORTING TO MOBILE
        rectTransform = GetComponent<RectTransform>();
        newScreen = monitorScreens[currentScreenIndex].GetComponent<RectTransform>();
        boxCollider = GetComponent<BoxCollider>();
        boxCollider.enabled = false;
        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        // MouseCaptureController.CaptureMouse(captureMouse());
        float mouseX = Input.mousePosition.x - prevMouseX;
        float mouseY = Input.mousePosition.y - prevMouseY;
        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;

        SetNewAnchorPos(rectTransform.anchoredPosition.x + mouseX * mouseSpeed, rectTransform.anchoredPosition.y + mouseY * mouseSpeed);
        if (mouseX != 0)  // Mouse moved horizontally. Might have moved out of screen. 
            HandleMouseMonitorTransitions();
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(MouseClick());
        }
    }

    private void SetNewAnchorPos(float x, float y)
    {
        float rightSideBoundary = newScreen.rect.width / 2;
        float leftSideBoundary = -rightSideBoundary;
        float topBoundary = newScreen.rect.width / 2;
        float bottomBoundary = -topBoundary;

        float xDelta = 1f; // Used so that the cursor can be outside of the boundary for HandleMouseMonitorTransitions

        float newXValue = Mathf.Clamp(x, leftSideBoundary - xDelta, rightSideBoundary + xDelta);
        float newYValue = Mathf.Clamp(y, bottomBoundary, topBoundary);

        rectTransform.anchoredPosition = new Vector2(newXValue, newYValue);
    }

    private IEnumerator MouseClick()
    {
        boxCollider.enabled = true;
        yield return new WaitForSeconds(0.1f);
        boxCollider.enabled = false;
    }

    private void HandleMouseMonitorTransitions()
    {
        float rightSideBoundary = newScreen.rect.width / 2;
        float leftSideBoundary = -rightSideBoundary;
        float x = rectTransform.localPosition.x;

        bool isWithinScreenBoundary = x > leftSideBoundary && x < rightSideBoundary;
        if (isWithinScreenBoundary) return;

        bool movedLeft = x < leftSideBoundary;
        UpdateMonitor(movedLeft);
    }

    private void UpdateMonitor(bool moveLeft)
    {
        int oldCurrentScreenIndex = currentScreenIndex;

        if (moveLeft && currentScreenIndex != 0) currentScreenIndex--;
        else if (!moveLeft && currentScreenIndex < monitorScreens.Length - 1) currentScreenIndex++;

        bool changedScreen = oldCurrentScreenIndex != currentScreenIndex;
        if (changedScreen) ChangeActiveMonitor(moveLeft);
    }

    private void ChangeActiveMonitor(bool moveLeft)
    {
        newScreen = monitorScreens[currentScreenIndex].GetComponent<RectTransform>();
        this.gameObject.transform.SetParent(newScreen, false);
        float newCursorXValue = moveLeft ? newScreen.rect.width / 2 : -newScreen.rect.width / 2;
        SetNewAnchorPos(newCursorXValue, rectTransform.anchoredPosition.y);
    }

    public static bool RectTransformContainsAnother(RectTransform rectTransform, RectTransform another)
    {
        Vector2 yVector = new Vector2(another.rect.yMax, another.rect.yMin);
        Vector2 xVector = new Vector2(another.rect.xMax, another.rect.xMin);
        return rectTransform.rect.Contains(yVector) && rectTransform.rect.Contains(xVector);
    }
}
