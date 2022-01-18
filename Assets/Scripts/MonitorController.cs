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
    }

    void OnDestroy()
    {
        activeMonitors.Remove(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
