using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;
using Aryzon;

public class PressCalculatorBtnScript : AryzonRaycastInteractable
{
    private DoCalculationsScript calculationScript;

    private void Start()
    {
        calculationScript = transform.parent.parent.Find("Result Box").Find("ResultText").GetComponent<DoCalculationsScript>();
    }

    protected override void Down()
    {
        if (gameObject.name.Equals("=") || gameObject.name.Equals("+") || gameObject.name.Equals("-") || gameObject.name.Equals("*") || gameObject.name.Equals("/") || gameObject.name.Equals(","))
        {
            calculationScript.ReceiveSymbol(gameObject.name);
        }
        else
        {
            calculationScript.ReceiveNumber(gameObject.name);
        }
    }
}