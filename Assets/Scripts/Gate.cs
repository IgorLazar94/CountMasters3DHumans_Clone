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
    [SerializeField] private GateType gateType;
    private TextMeshPro gateText;
    private int randomNumber;


    private void Start()
    {
        gateText = GetComponentInChildren<TextMeshPro>();

        if (gateType == GateType.Multiply)
        {
            SetRandomMultiply();
        }
        else
        {
            SetRandomAdd();
        }
    }

    private void SetRandomMultiply()
    {
        randomNumber = Random.Range(1, 3);
        gateText.text = "X" + randomNumber;
    }

    private void SetRandomAdd()
    {
        randomNumber = Random.Range(10, 100);

        if (randomNumber % 2 != 0)
        {
            randomNumber += 1;
        }

        gateText.text = randomNumber.ToString();
    }

}
