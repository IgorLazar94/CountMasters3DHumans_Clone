using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum GateType
{
    Add, 
    Multiply
}
public class Gate : MonoBehaviour
{
    public GateType gateType;
    private TextMeshPro gateText;
    public int number;

    private void Start()
    {
        gateText = GetComponentInChildren<TextMeshPro>();

        if (gateType == GateType.Multiply)
        {
            //SetRandomMultiply();
            gateText.text = "X" + number;
        }
        else
        {
            //SetRandomAdd();
            gateText.text = number.ToString();
        }
    }

    private void SetRandomMultiply()
    {
        number = Random.Range(1, 3);
        gateText.text = "X" + number;
    }

    private void SetRandomAdd()
    {
        number = Random.Range(10, 100);

        if (number % 2 != 0)
        {
            number += 1;
        }

        gateText.text = number.ToString();
    }

}
