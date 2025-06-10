using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinGame : BasicPopup
{
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
        score.text = PlayerPrefs.GetString("Score");
    }

    private void Restart()
    {
        Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
    }
    private void Home()
    {
        Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}