using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Progres : BasicScreen
{
    public Button back;
    public Button shop;
    public Button profile;
    public Button home;

    public Button[] achievements;
    public Sprite[] activeAchievements;

    public TMP_Text hours;
    public TMP_Text days;
    public TMP_Text score;

    private DateTime lastLoginDate;
    private int consecutiveDays;
    private float sessionStartTime;

    private void Start()
    {
        SetTimers();

        back.onClick.AddListener(Back);
        shop.onClick.AddListener(Shop);
        profile.onClick.AddListener(Profile);
        home.onClick.AddListener(Home);

        for (int i = 0; i < achievements.Length; i++)
        {
            int index = i;

            achievements[index].onClick.AddListener(() => OpenAchieve(index));
        }
    }

    private void SetTimers()
    {
        sessionStartTime = Time.time;

        // Отримати збережені значення
        string lastLoginStr = PlayerPrefs.GetString("LastLoginDate", "");
        int totalSeconds = PlayerPrefs.GetInt("TotalPlayTime", 0);
        consecutiveDays = PlayerPrefs.GetInt("ConsecutiveDays", 1);

        DateTime today = DateTime.Today;

        if (DateTime.TryParse(lastLoginStr, out lastLoginDate))
        {
            TimeSpan diff = today - lastLoginDate.Date;

            if (diff.Days == 1)
            {
                // Наступний день — збільшуємо лічильник
                consecutiveDays++;
            }
            else if (diff.Days > 1)
            {
                // Більше одного дня пропущено — обнуляємо
                consecutiveDays = 1;
            }
        }

        // Зберігаємо сьогоднішню дату
        PlayerPrefs.SetString("LastLoginDate", today.ToString("yyyy-MM-dd"));
        PlayerPrefs.SetInt("ConsecutiveDays", consecutiveDays);
        PlayerPrefs.Save();

        // Виводимо статистику
        UpdateUI(totalSeconds, consecutiveDays);
    }

    private void OnDestroy()
    {
        back.onClick.RemoveListener(Back);
        shop.onClick.RemoveListener(Shop);
        profile.onClick.RemoveListener(Profile);
        home.onClick.RemoveListener(Home);

        for (int i = 0; i < achievements.Length; i++)
        {
            int index = i;

            achievements[index].onClick.RemoveListener(() => OpenAchieve(index));
        }
    }
    void OnApplicationQuit()
    {
        SaveSessionTime();
    }

    void OnApplicationPause(bool pause)
    {
        if (pause)
            SaveSessionTime();
    }

    void SaveSessionTime()
    {
        float sessionTime = Time.time - sessionStartTime;
        int totalSeconds = PlayerPrefs.GetInt("TotalPlayTime", 0);
        totalSeconds += Mathf.RoundToInt(sessionTime);

        PlayerPrefs.SetInt("TotalPlayTime", totalSeconds);
        PlayerPrefs.Save();
    }

    void UpdateUI(int totalSeconds, int consecutiveDays)
    {
        TimeSpan time = TimeSpan.FromSeconds(totalSeconds);
        hours.text = $"{time.Hours:D1}+ \nHOURS";
        days.text = $"{consecutiveDays} DAYS \nIN A ROW";
    }

    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
        score.text = PlayerPrefs.GetInt("MaxScore").ToString();


        for (int i = 0; i < achievements.Length; i++)
        {
            string key = "Achieve" + i;
            if(PlayerPrefs.GetInt(key) == 1)
            {
                achievements[i].gameObject.GetComponent<Image>().sprite = activeAchievements[i];
            }
        }
    }

    private void OpenAchieve(int index)
    {
        Achievement achievement = (Achievement)UIManager.Instance.GetPopup(PopupTypes.Achieve);
        achievement.Init(index);
        UIManager.Instance.ShowPopup(PopupTypes.Achieve);
    }

    private void Shop()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Shop);
    }
    private void Profile()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
    private void Home()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void Back()
    {
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}