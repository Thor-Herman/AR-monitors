using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Aryzon;


public class PressKeyboardBtnScript : AryzonRaycastInteractable
{
    private Text textBox;
    private string text;

    protected override void Down()
    {
        // GameObject.Find doesn't work as intended when there are 2+ instances with the same name. 
        textBox = transform.parent.parent.Find("Result Box").Find("ResultTextNote").gameObject.GetComponent<Text>();

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
}
