using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StairsManager : MonoBehaviour
{
    [SerializeField] private CoinManager coinManager;
    [SerializeField] private UIManager uiManager;
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
            child.gameObject.GetComponent<Stair>().SetBonusFactor(i);
        }
    }

    public void MultiplyCoinsByFactor(Transform stair)
    {
        int coins = coinManager.GetPlayerCoin();

        float bonusFactor = stair.gameObject.GetComponent<Stair>().GetBonusFactor();
        resultCoins = (int)bonusFactor + coins;
        coinManager.SavePlayerCoins(resultCoins);
        uiManager.UpdateCoinText(resultCoins);
        uiManager.CalculateResultScores(resultCoins);
        uiManager.ShowWinPanel();
    }
}
