using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Home : BasicScreen
{
    public Button playGame;
    public Button settings;
    public Button shop;
    public Button profile;
    public Button progress;

    public TMP_Text score;

    private void Start()
    {
        playGame.onClick.AddListener(StartGame);
        settings.onClick.AddListener(Settings);
        shop.onClick.AddListener(Shop);
        profile.onClick.AddListener(Profile);
        progress.onClick.AddListener(Progress); 
    }

    private void OnDestroy()
    {
        playGame.onClick.RemoveListener(StartGame);
        settings.onClick.RemoveListener(Settings);
        shop.onClick.RemoveListener(Shop);
        profile.onClick.RemoveListener(Profile);
        progress.onClick.RemoveListener(Progress);

    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        score.text = PlayerPrefs.GetInt("Score").ToString();
    }

    private void StartGame()
    {
        UIManager.Instance.ShowPopup(PopupTypes.StartGame);
    }
    private void Settings()
    {
        UIManager.Instance.ShowPopup(PopupTypes.Settings);
    }
    private void Shop()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Shop);
    }
    private void Profile()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void Progress()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Progress);
    }

}