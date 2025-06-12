using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Game : BasicScreen
{
    public AudioSource audioSource;

    public GamePlayManager gamePlayManager;
    public Button back;
    public Button settings;

    public TMP_Text score;

    public TMP_Text freezText;
    public TMP_Text slowmoText;
    public TMP_Text magnetText;

    public Button freezButton;
    public Button slowmoButton;
    public Button magnetButton;

    private void Start()
    {
        back.onClick.AddListener(Back);
        settings.onClick.AddListener(Settings);
        freezButton.onClick.AddListener(Freez);
        slowmoButton.onClick.AddListener(Slowmo);
        magnetButton.onClick.AddListener(Magnet);
    }

    private void OnDestroy()
    {
        back.onClick.RemoveListener(Back);
        settings.onClick.RemoveListener(Settings);
        freezButton.onClick.RemoveListener(Freez);
        slowmoButton.onClick.RemoveListener(Slowmo);
        magnetButton.onClick.RemoveListener(Magnet);
    }
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {

        SetText();


        StartGame();
    }

    public void SetText()
    {
        score.text = PlayerPrefs.GetInt("Score").ToString();
        freezText.text = PlayerPrefs.GetInt("Freez").ToString();
        slowmoText.text = PlayerPrefs.GetInt("Slowmo").ToString();
        magnetText.text = PlayerPrefs.GetInt("Magnet").ToString();
    }
    public void StartGame()
    {
        gamePlayManager.StartGame();
    }
    private void Settings()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        Time.timeScale = 0;
        UIManager.Instance.ShowPopup(PopupTypes.Settings);
    }
    private void Back()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        gamePlayManager.CleanGame();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }

    private void Freez()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        gamePlayManager.Freez();
    }

    private void Slowmo()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        gamePlayManager.SlowMo();
    }

    private void Magnet()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        gamePlayManager.Magnet();
    }
}