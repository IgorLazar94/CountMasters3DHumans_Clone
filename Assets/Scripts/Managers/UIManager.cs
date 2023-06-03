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

    [SerializeField] PlayerBehaviour playerBehaviour;
    private RectTransform cointTransform;

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
