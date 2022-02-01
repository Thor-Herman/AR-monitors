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
            currentScreensActive.Add(newImage.referenceImage.name);
            ReorderMonitorList(newImage.referenceImage.name);
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
            StartCoroutine(ReorderMonitor(0));
        else if (newlyAddedMonitorName.Equals("Middle"))
            StartCoroutine(ReorderMonitor(1));
    }

    IEnumerator ReorderMonitor(int index)
    {
        // The monitor that was latest added will be the latest one added to the list
        yield return new WaitForSeconds(1); // So that monitor controller script will happen first
        if (!(index == 1 && MonitorController.activeMonitors.Count == 1)) // If not middle is the only added screen. 
        {
            var newlyAddedMonitor = MonitorController.activeMonitors[MonitorController.activeMonitors.Count - 1];
            MonitorController.activeMonitors.Remove(newlyAddedMonitor);
            MonitorController.activeMonitors.Insert(index, newlyAddedMonitor);
            Debug.Log($"New Monitor Order for list of length {MonitorController.activeMonitors.Count}:");
            foreach (MonitorController m in MonitorController.activeMonitors)
            {
                Debug.Log("Monitor: " + m.ToString());
            }
        }
    }
}
