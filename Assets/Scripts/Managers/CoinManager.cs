using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    [SerializeField] UIManager uiManager;
    private int playerCoins;

    private void Start()
    {
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", playerCoins);
        UpdateCoinText();
        //playerCoins = 0;
    }

    public void UpdateCoinText()
    {
        uiManager.UpdateCoinText(playerCoins);

    }

    public void RemoveCoins(int value)
    {
        playerCoins -= value;
        if (playerCoins <= 0)
        {
            playerCoins = 0;
        }
    }

    public void AddPlayerCoin()
    {
        playerCoins++;
        UpdateCoinText();
    }

    public int GetPlayerCoin()
    {
        return playerCoins;
    }

    public int GetPlayerCoinFromMagazine()
    {
        playerCoins = PlayerPrefs.GetInt("PlayerCoins", playerCoins);
        Debug.Log(playerCoins + "player Coins GET");
        return playerCoins;
    }

    public void SavePlayerCoins(int _finalCoins)
    {
        PlayerPrefs.SetInt("PlayerCoins", _finalCoins);
        PlayerPrefs.Save();
    }


}
