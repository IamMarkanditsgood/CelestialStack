
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : BasicScreen
{
    public AudioSource audioSource;
    public Button back;
    public Button progress;
    public Button profile;
    public Button home;

    public Button[] buyButtons;
    public Button[] isBoght;
    public GameObject[] isSelected;

    private void Start()
    {
        back.onClick.AddListener(Back);
        progress.onClick.AddListener(Progres);
        profile.onClick.AddListener(Profile);
        home.onClick.AddListener(Home);

        for (int i = 0; i < buyButtons.Length; i++)
        {
            int index = i;

            buyButtons[index].onClick.AddListener(() => Buy(index));
        }
        for (int i = 0; i < isBoght.Length; i++)
        {
            int index = i;

            isBoght[index].onClick.AddListener(() => Select(index));
        }
    }

   
    private void OnDestroy()
    {
        back.onClick.RemoveListener(Back);
        progress.onClick.RemoveListener(Progres);
        profile.onClick.RemoveListener(Profile);
        home.onClick.RemoveListener(Home);

        for (int i = 0; i < buyButtons.Length; i++)
        {
            int index = i;

            buyButtons[index].onClick.RemoveListener(() => Buy(index));
        }

        for (int i = 0; i < isBoght.Length; i++)
        {
            int index = i;

            isBoght[index].onClick.RemoveListener(() => Select(index));
        }
    }
    

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        string key = "BG" + 0;
        if (!PlayerPrefs.HasKey(key))
        {
            PlayerPrefs.SetInt(key, 1);
        }

        int bought = 0;
        for (int i = 0; i < buyButtons.Length; i++)
        {
            key = "BG" + i;
            if (PlayerPrefs.GetInt(key) == 1)
            {
                buyButtons[i].gameObject.SetActive(false);
                isBoght[i].gameObject.SetActive(true);
                bought++;
            }
            else
            {
                buyButtons[i].gameObject.SetActive(true);
                isBoght[i].gameObject.SetActive(false);
            }
        }
        string achievekey = "Achieve" + 4;
        if (bought >= 4 && !PlayerPrefs.HasKey(achievekey))
        {
            
            PlayerPrefs.SetInt(achievekey, 1);
        }
        for (int i = 0; i < isSelected.Length; i++)
        {
            isSelected[i].SetActive(false);
        }

        isSelected[PlayerPrefs.GetInt("CurrentBG")].SetActive(true);
    }

    private void Buy(int index)
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        int coins = PlayerPrefs.GetInt("Score");
        if (coins > 150)
        {
            coins -= 150;
            PlayerPrefs.SetInt("Score", coins);
            string key = "BG" + index;
            PlayerPrefs.SetInt(key, 1);
            SetScreen();

            string achievekey = "Achieve" + 5;
            if (!PlayerPrefs.HasKey(achievekey))
            {

                PlayerPrefs.SetInt(achievekey, 1);
            }
        }
    }

    private void Select(int index)
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        PlayerPrefs.SetInt("CurrentBG", index);
        SetScreen();
    }

    private void Progres()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Progress);
    }
    private void Profile()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void Home()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void Back()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}