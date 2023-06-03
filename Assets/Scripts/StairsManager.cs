using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StairsManager : MonoBehaviour
{
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private UIManager uiManager;
    //private Dictionary<string, Transform > stairsDictionary = new Dictionary<string, Transform >(); 
    private int resultCoins;

    private void Start()
    {
        CalculateStairs();
    }

    private void CalculateStairs()
    {
        int stairsCount = transform.childCount;

        for (int i = 0; i < stairsCount; i++)
        {
            Transform child = transform.GetChild(i);
            float test = (i + 1) / 10;
            Debug.Log(test + " value");
            child.gameObject.GetComponent<Stair>().SetBonusFactor(i);







            //if (i + 1 < 10)
            //{
            //    string bonusFactor = "1." + (i + 1).ToString();
            //    float number = float.Parse(bonusFactor);
            //    child.gameObject.GetComponent<Stair>().SetBonusFactor(number);
            //    child.gameObject.GetComponentInChildren<TextMeshPro>().text = bonusFactor;
            //    //stairsDictionary.Add(bonusFactor, child);
            //}
            //else
            //{
            //    var bonusFactor = ((((i + 1) / 10) + (1 + i)) - 9).ToString();
            //    float number = float.Parse(bonusFactor);
            //    child.gameObject.GetComponent<Stair>().SetBonusFactor(number);
            //    child.gameObject.GetComponentInChildren<TextMeshPro>().text = bonusFactor;
            //    //stairsDictionary.Add(bonusFactor, child);
            //}
        }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            var test = transform.GetChild(10);
            var test2 = test.GetComponentInChildren<TextMeshPro>().text;
            float number;
            if (float.TryParse(test2, out number))
            {
                Debug.Log("Число: " + number * 10);
            }
        }
    }

    public void MultiplyCoinsByFactor(Transform stair)
    {
        int coins = coinManager.GetPlayerCoin();
        //if (stairsDictionary.ContainsValue(stair))
        //{
        //    var yourObj = stairsDictionary.Single(s => s.Key == "someValue").Value;
        //}

        //int siblingIndex = stair.transform.GetSiblingIndex();

        float bonusFactor = stair.gameObject.GetComponent<Stair>().GetBonusFactor();
        resultCoins = (int)bonusFactor + coins;
        uiManager.UpdateCoinText(resultCoins);
        Debug.Log("Finish");

    }



}
