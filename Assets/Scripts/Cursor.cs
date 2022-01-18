using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class Cursor : MonoBehaviour
{

    [Tooltip("First monitor in array will be the default starting one.")]
    [SerializeField] GameObject[] monitorScreens;
    [SerializeField] Collider downTrigger, upTrigger;
    int currentScreenIndex = 0; // Index of current screen

    RectTransform rectTransform;
    RectTransform newScreen;
    [SerializeField] float mouseSpeed = 1.5f;

    float prevMouseX, prevMouseY;
    bool pressedMouseDown;
    ARTrackedImageManager trackableManager;
    GameObject targetPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Input.simulateMouseWithTouches = false;
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        rectTransform = GetComponent<RectTransform>();
        newScreen = monitorScreens[currentScreenIndex].GetComponent<RectTransform>();
        upTrigger.enabled = false;
        downTrigger.enabled = false;
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
            pressedMouseDown = true;
            StartCoroutine(MouseClick(downTrigger));
        }
        else if (!Input.GetMouseButton(0) && pressedMouseDown)
        {
            pressedMouseDown = false;
            StartCoroutine(MouseClick(upTrigger));
        }

        //trackableManager = GameObject.Find("AR Session Origin").GetComponent<ARTrackedImageManager>();

        // targetPrefab = trackableManager.trackedImagePrefab;


        if (MonitorController.activeMonitors.Count == 3) monitorScreens[2] = MonitorController.activeMonitors[2].gameObject;
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

    private IEnumerator MouseClick(Collider trigger)
    {
        trigger.enabled = true;
        yield return new WaitForSeconds(0.1f);
        trigger.enabled = false;
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
        else if (!moveLeft && currentScreenIndex < monitorScreens.Length - 1 && monitorScreens[currentScreenIndex + 1] != null) currentScreenIndex++;

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
