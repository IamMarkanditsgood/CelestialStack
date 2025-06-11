using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : BasicScreen
{
    public GamePlayManager gamePlayManager;
    public Button back;
    public Button settings;

    public TMP_Text score;

    private void Start()
    {
        back.onClick.AddListener(Back);
        settings.onClick.AddListener(Settings);
    }

    private void OnDestroy()
    {
        back.onClick.RemoveListener(Back);
        settings.onClick.RemoveListener(Settings);
    }
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        score.text = PlayerPrefs.GetInt("Score").ToString();
        StartGame();
    }

    public void StartGame()
    {
        gamePlayManager.StartGame();
    }
    private void Settings()
    {
        Time.timeScale = 0;
        UIManager.Instance.ShowPopup(PopupTypes.Settings);
    }
    private void Back()
    {
        gamePlayManager.CleanGame();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}
