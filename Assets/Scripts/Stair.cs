using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stair : MonoBehaviour
{
    TextMeshPro bonusText;
    private float bonusFactor;

    private void Start()
    {
        bonusText = GetComponentInChildren<TextMeshPro>();
    }
    public void SetBonusFactor(float value)
    {
        value++;
        bonusFactor = value;
        UpdateBonusText(value);
    }

    public float GetBonusFactor()
    {
        return bonusFactor;
    }

    private void UpdateBonusText(float _value)
    {
        Debug.Log(bonusText + "bonusTExt");
        Debug.Log(_value + "value");
        bonusText.text = "x" + _value.ToString();
    }
}
