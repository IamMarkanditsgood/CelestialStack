using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyBonus : BasicPopup
{
    public Button buy;
    public TMP_Text name;
    public Image image;
    private int buster = -1;

    public void Init(Sprite busterSprite, string busterName, int index)
    {
        name.text = busterName;
        image.sprite = busterSprite;
        buster = index;
    }

    public override void Subscribe()
    {
        base.Subscribe();
        buy.onClick.AddListener(Buy);
    }
    public override void Unsubscribe()
    {
        base.Unsubscribe();
        buy.onClick.RemoveListener(Buy);
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
    }

    public void Buy()
    {
        int coins = PlayerPrefs.GetInt("Score");
        if (coins >= 500)
        {
            switch(buster)
            {
                case 0:
                    PlayerPrefs.SetInt("Freez", (PlayerPrefs.GetInt("Freez") + 1));
                    break;
                case 1:
                    PlayerPrefs.SetInt("Slowmo", (PlayerPrefs.GetInt("Slowmo") + 1));
                    break;
                case 2:
                    PlayerPrefs.SetInt("Magnet", (PlayerPrefs.GetInt("Magnet") + 1));
                    break;
            }
            coins -= 500;
            PlayerPrefs.SetInt("Score", coins);

            Hide();
            UIManager.Instance.ShowPopup(PopupTypes.StartGame);
        }
        else
        {
            Hide();
            UIManager.Instance.ShowPopup(PopupTypes.StartGame);
        }
    }
}
