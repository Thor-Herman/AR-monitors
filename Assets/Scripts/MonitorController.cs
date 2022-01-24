using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorController : MonoBehaviour
{
    public static List<MonitorController> activeMonitors = new List<MonitorController>();
    // Start is called before the first frame update
    void Start()
    {
        activeMonitors.Add(this);
        Debug.Log($"Added {gameObject.name} to monitorcontrollers");
    }

    void OnDestroy()
    {
        activeMonitors.Remove(this);
    }

    public override string ToString()
    {
        return this.gameObject.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
