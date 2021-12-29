using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Globalization;

public class EscreverData : MonoBehaviour
{
    public bool weather;
    // Update is called once per frame
    void FixedUpdate()
    {
        if (!weather)
        {
            gameObject.GetComponent<Text>().text = System.DateTime.Now.ToString("dd/MM ddd HH:mm", CultureInfo.CreateSpecificCulture("en-US"));
        }
        else
        {
            gameObject.GetComponent<Text>().text = System.DateTime.Now.ToString("dddd dd MMMM yyyy", CultureInfo.CreateSpecificCulture("en-US"));
        }
    }
}
