using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [SerializeField] GameObject startPanel;
    [SerializeField] GameObject losePanel;
    [SerializeField] GameObject winPanel;
    [SerializeField] GameObject playModePanel;
    [SerializeField] GameObject magazinePanel;
 
    [SerializeField] TextMeshProUGUI coinsText;
    [SerializeField] TextMeshProUGUI coinsResult;

    [SerializeField] TextMeshProUGUI coinsMagazineText;
    [SerializeField] CoinManager coinManager;

    [SerializeField] PlayerBehaviour playerBehaviour;
    private RectTransform cointTransform;
    private int price = 20;

    private void Start()
    {
        cointTransform = coinsText.GetComponent<RectTransform>();
        ShowStartPanel();
    }

    public void UpdateCoinText(int coins)
    {
        coinsText.text = coins.ToString();
        cointTransform.DOScale(1.2f, 0.1f).OnComplete(() => cointTransform.DOScale(1.0f, 0.1f));
    }

    public void UpdateMagazineText()
    {
        int coins = coinManager.GetPlayerCoinFromMagazine();
        coinsMagazineText.text = coins.ToString();
    }

    public void BuySkin()
    {
        int coins = coinManager.GetPlayerCoinFromMagazine();
        coinManager.RemoveCoins(price);
        coinManager.SavePlayerCoins(coins - price);
    }
    

    public void ShowLosePanel()
    {
        playModePanel.SetActive(false);
        losePanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void ShowWinPanel()
    {
        playModePanel.SetActive(false);
        winPanel.SetActive(true);
        Time.timeScale = 0;
    }

    private void ShowStartPanel()
    {
        startPanel.SetActive(true);
        Time.timeScale = 0;
    }

    public void HideStartPanel()
    {
        coinManager.UpdateCoinText();
        startPanel.SetActive(false);
        playModePanel.SetActive(true);
        Time.timeScale = 1;
    }

    public void CalculateResultScores(int scores)
    {
        coinsResult.text = "Your scores: " + scores.ToString();
    }

    public void EnterMagazine()
    {
        UpdateMagazineText();
        startPanel.SetActive(false);
        magazinePanel.SetActive(true);
    }

    public void ExitMagazine()
    {
        magazinePanel.SetActive(false);
        startPanel.SetActive(true);
        playerBehaviour.ChooseColor();
    }
}
