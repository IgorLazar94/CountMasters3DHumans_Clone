using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    private int playerCoins;

    private void Start()
    {
        playerCoins = 0;
    }

    public void AddPlayerCoin()
    {
        playerCoins++;
        uiManager.UpdateCoinText(playerCoins);
    }

    public int GetPlayerCoin()
    {
        return playerCoins;
    }

    
}
