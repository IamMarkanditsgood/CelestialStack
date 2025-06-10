using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : BasicScreen
{
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
        ClearLevel();
    }

    public override void SetScreen()
    {
        score.text = PlayerPrefs.GetInt("Score").ToString();
        ClearLevel();
        StartGame();
    }

    public void ClearLevel()
    {

    }
    public void StartGame()
    {

    }
    private void Settings()
    {
        UIManager.Instance.ShowPopup(PopupTypes.Settings);
    }
    private void Back()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}
