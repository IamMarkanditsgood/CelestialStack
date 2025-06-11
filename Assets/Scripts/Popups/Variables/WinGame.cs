using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : BasicPopup
{
    public GamePlayManager PlayManager;
    public TMP_Text score;

    public Button restart;
    public Button home;

    public override void Subscribe()
    {
        base.Subscribe();
        restart.onClick.AddListener(Restart);
        home.onClick.AddListener(Home);
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        restart.onClick.RemoveListener(Restart);
        home.onClick.RemoveListener(Home);
    }
    
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
    }

    public void SetText(int scoreReward)
    {
        int newTotalScore = PlayerPrefs.GetInt("MaxScore") + scoreReward;
        PlayerPrefs.SetInt("MaxScore", newTotalScore);
        score.text = scoreReward.ToString();
    }

    private void Restart()
    {
        Hide();
        PlayManager.CleanGame();
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
    }
    private void Home()
    {
        Hide();
        PlayManager.CleanGame();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}