using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Cursor : MonoBehaviour
{
    [SerializeField] Collider downTrigger, upTrigger;
    int currentScreenIndex = 0; // Index of current screen

    RectTransform rectTransform;
    RectTransform screen;
    [SerializeField] float mouseSpeed = 1.5f;

    float prevMouseX, prevMouseY, screenWidth;
    bool pressedMouseDown;
    ARTrackedImageManager trackableManager;

    // Start is called before the first frame update
    void Start()
    {
        Input.simulateMouseWithTouches = false;
        // UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rectTransform = GetComponent<RectTransform>();
        upTrigger.enabled = false;
        downTrigger.enabled = false;
        prevMouseX = Input.mousePosition.x;
        prevMouseY = Input.mousePosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (MonitorController.activeMonitors.Count == 0) return; // Don't do anything with mouse when there are no screens

        if (screen == null) // Previous frame there were no monitors. Now first monitor is enabled.
        {
            screen = MonitorController.activeMonitors[currentScreenIndex].gameObject.GetComponent<RectTransform>();
            ToggleMonitorBorder(screen, true);
            this.gameObject.transform.SetParent(screen, false);
            screenWidth = 2.3f;
            Debug.Log("Added first screen");
        }

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
            pressedMouseDown = true;
            StartCoroutine(MouseClick(downTrigger));
        }
        else if (!Input.GetMouseButton(0) && pressedMouseDown)
        {
            pressedMouseDown = false;
            StartCoroutine(MouseClick(upTrigger));
        }
        // Debug.Log(rectTransform.anchoredPosition);
    }

    private void SetNewAnchorPos(float x, float y)
    {
        float rightSideBoundary = screenWidth / 2;
        float leftSideBoundary = -rightSideBoundary;
        float topBoundary = 0.9f / 2;
        float bottomBoundary = -topBoundary - 0.1f;

        float xDelta = 0.001f; // Used so that the cursor can be outside of the boundary for HandleMouseMonitorTransitions

        float newXValue = Mathf.Clamp(x, leftSideBoundary - xDelta, rightSideBoundary + xDelta);
        float newYValue = Mathf.Clamp(y, bottomBoundary, topBoundary);

        rectTransform.anchoredPosition = new Vector2(newXValue, newYValue);
    }

    private IEnumerator MouseClick(Collider trigger)
    {
        trigger.enabled = true;
        yield return new WaitForSeconds(0.1f);
        trigger.enabled = false;
    }

    private void HandleMouseMonitorTransitions()
    {
        float rightSideBoundary = screenWidth / 2;
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
        else if (!moveLeft && currentScreenIndex < MonitorController.activeMonitors.Count - 1) currentScreenIndex++;

        bool changedScreen = oldCurrentScreenIndex != currentScreenIndex;
        if (changedScreen) ChangeActiveMonitor(moveLeft, oldCurrentScreenIndex);
    }

    private void ChangeActiveMonitor(bool moveLeft, int oldCurrentScreenIndex)
    {
        RectTransform oldScreen = MonitorController.activeMonitors[oldCurrentScreenIndex].GetComponent<RectTransform>();
        screen = MonitorController.activeMonitors[currentScreenIndex].GetComponent<RectTransform>();
        this.gameObject.transform.SetParent(screen, false);
        float newCursorXValue = moveLeft ? screenWidth / 2 : -screenWidth / 2;
        SetNewAnchorPos(newCursorXValue, rectTransform.anchoredPosition.y);
        Debug.Log("Changed to screen " + this.gameObject.transform.parent.gameObject.name);
        ToggleMonitorBorder(oldScreen, false);
        ToggleMonitorBorder(screen, true);
    }

    public static bool RectTransformContainsAnother(RectTransform rectTransform, RectTransform another)
    {
        Vector2 yVector = new Vector2(another.rect.yMax, another.rect.yMin);
        Vector2 xVector = new Vector2(another.rect.xMax, another.rect.xMin);
        return rectTransform.rect.Contains(yVector) && rectTransform.rect.Contains(xVector);
    }

    private void ToggleMonitorBorder(RectTransform toggleScreen, bool enabled)
    {
        toggleScreen.Find("Border").gameObject.SetActive(enabled);
    }
}
