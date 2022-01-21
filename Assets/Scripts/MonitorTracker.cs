using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

public class MonitorTracker : MonoBehaviour
{
    ARTrackedImageManager trackableManager;
    List<string> currentScreensActive = new List<string>();


    private void Start()
    {
        trackableManager = GameObject.Find("AR Session Origin").GetComponent<ARTrackedImageManager>();
        trackableManager.trackedImagesChanged += OnChanged;
    }

    void OnChanged(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var newImage in eventArgs.added)
        {
            ReorderMonitorList(newImage.referenceImage.name);
            currentScreensActive.Add(newImage.referenceImage.name);
        }

        foreach (var removedImage in eventArgs.removed)
        {
            currentScreensActive.Remove(removedImage.referenceImage.name);
        }
    }

    void ReorderMonitorList(string newlyAddedMonitorName)
    { //🍝🍝🍝🍝🍝🍝
        Debug.Log("Newly added monitor name: " + newlyAddedMonitorName);
        if (newlyAddedMonitorName.Equals("Left"))
            ReorderMonitor(0);
        else if (newlyAddedMonitorName.Equals("Middle"))
            ReorderMonitor(1);
    }

    void ReorderMonitor(int index)
    {
        // The monitor that was latest added will be the latest one added to the list
        var newlyAddedMonitor = MonitorController.activeMonitors[MonitorController.activeMonitors.Count - 1];
        MonitorController.activeMonitors.Remove(newlyAddedMonitor);
        MonitorController.activeMonitors.Insert(index, newlyAddedMonitor);
        Debug.Log(MonitorController.activeMonitors);
    }
}
