using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Aryzon
{
    public class PressCalculatorBtnScript : MonoBehaviour
    {
        private AryzonRaycastObject raycastObject;
        private DoCalculationsScript calculationScript;

        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();

        }
        private void Start()
        {
            calculationScript = GameObject.Find("ResultText").GetComponent<DoCalculationsScript>();
        }
        private void OnEnable()
        {
            if (raycastObject)
            {
                raycastObject.OnPointerOff.AddListener(Off);
                raycastObject.OnPointerOver.AddListener(Over);
                raycastObject.OnPointerUp.AddListener(Clicked);
                raycastObject.OnPointerDown.AddListener(Down);
            }
        }

        private void OnDisable()
        {
            if (raycastObject)
            {
                raycastObject.OnPointerOff.RemoveListener(Off);
                raycastObject.OnPointerOver.RemoveListener(Over);
                raycastObject.OnPointerUp.RemoveListener(Clicked);
                raycastObject.OnPointerDown.RemoveListener(Down);
            }
        }

        private void Off()
        {

        }

        private void Over()
        {

        }

        private void Down()
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
        private void Clicked()
        {

        }
    }
}