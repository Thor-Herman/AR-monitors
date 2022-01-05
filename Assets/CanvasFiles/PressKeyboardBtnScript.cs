using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace Aryzon
{
    public class PressKeyboardBtnScript : MonoBehaviour
    {
        private AryzonRaycastObject raycastObject;
        private Text textBox;
        private string text;

        private void Awake()
        {
            raycastObject = gameObject.GetComponent<AryzonRaycastObject>();

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
            textBox = GameObject.Find("ResultTextNote").GetComponent<Text>();

            if (gameObject.name.Equals("space"))
            {
                textBox.text = textBox.text + " ";
            }
            else if (!gameObject.name.Equals("backspace"))
            {
                textBox.text = textBox.text + gameObject.name;
            } 
            else
            {
                textBox.text = textBox.text.Remove(textBox.text.Length - 1);
            }

        }
        private void Clicked()
        {

        }
    }
}
