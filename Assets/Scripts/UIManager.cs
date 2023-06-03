using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinsText;
    private RectTransform cointTransform;
    private int test = 1;

    private void Start()
    {
        cointTransform = coinsText.GetComponent<RectTransform>();
    }

    public void UpdateCoinText(int coins)
    {
        coinsText.text = coins.ToString();
        cointTransform.DOScale(1.2f, 0.1f).OnComplete(() => cointTransform.DOScale(1.0f, 0.1f));
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        UpdateCoinText(test);
    //        test++;
    //    }
    //}
}
