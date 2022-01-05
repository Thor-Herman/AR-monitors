using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DoCalculationsScript : MonoBehaviour
{
    private List<string> equationParser = new List<string>();
    private Text textBox;
    private bool isEqual = false;
    private string box = "";
    private string[] splitBox;
    private float num1, num2;
    private double result = 0.0;
    private bool fstOp = false;

    // Start is called before the first frame update
    void Start()
    {
        textBox = gameObject.GetComponent<Text>();
    }

    public void ReceiveNumber(string input)
    {
        if (isEqual)
        {
            equationParser.Clear();
            isEqual = false;
        }

        equationParser.Add(input);
    }
    public void ReceiveSymbol(string input)
    {
        if (isEqual)
        {
            equationParser.Clear();
            isEqual = false;
        }

        if (input != "=" && input != ",")
        {
            equationParser.Add(" " + input + " ");
        }
        else if (input == ","){
            equationParser.Add(input);
        }
        else
        {
            isEqual = true;
            box = textBox.text;
            splitBox = box.Split(' ');

            for(int i = 0; i < splitBox.Length; i++)
            {
                if(splitBox[i] == "+" || splitBox[i] == "-" || splitBox[i] == "*" || splitBox[i] == "/")
                {
                    if(i - 1 == 0)
                    {
                        fstOp = true;
                    }
                    else
                    {
                        fstOp = false;
                    }

                    switch (splitBox[i])
                    {
                        case "+":
                            if (fstOp)
                            {
                                double num1, num2;
                                Double.TryParse(splitBox[i - 1], out num1);
                                Double.TryParse(splitBox[i + 1], out num2);
                                result = num1 + num2;
                            }
                            else
                            {
                                double num2;
                                Double.TryParse(splitBox[i + 1], out num2);
                                result += num2;
                            }
                            break;
                        case "-":
                            if (fstOp)
                            {
                                double num1, num2;
                                Double.TryParse(splitBox[i - 1], out num1);
                                Double.TryParse(splitBox[i + 1], out num2);
                                result = num1 - num2;
                            }
                            else
                            {
                                double num2;
                                Double.TryParse(splitBox[i + 1], out num2);
                                result -= num2;
                            }
                            break;
                        case "*":
                            if (fstOp)
                            {
                                double num1, num2;
                                Double.TryParse(splitBox[i - 1], out num1);
                                Double.TryParse(splitBox[i + 1], out num2);
                                result = num1 * num2;
                            }
                            else
                            {
                                double num2;
                                Double.TryParse(splitBox[i + 1], out num2);
                                result *= num2;
                            }
                            break;
                        case "/":
                            if (fstOp)
                            {
                                double num1, num2;
                                Double.TryParse(splitBox[i - 1], out num1);
                                Double.TryParse(splitBox[i + 1], out num2);
                                result = num1 / num2;
                            }
                            else
                            {
                                double num2;
                                Double.TryParse(splitBox[i + 1], out num2);
                                result /= num2;
                            }
                            break;

                    }
                }
            }
        }
       
    }

    void Update()
    {
        if (isEqual)
        {
            textBox.text = string.Join("", equationParser) + "\n" + result;
        }
        else
        {
            textBox.text = string.Join("", equationParser);
        }
       
    }
}
