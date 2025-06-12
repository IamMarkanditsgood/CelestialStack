
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameStart : BasicPopup
{
    public TMP_Text freezText;
    public TMP_Text slowmoText;
    public TMP_Text magnetText;

    public Button[] busterButton;
    public string[] busterTitle;
    public Sprite[] busterIages;

    public Button goButton;


    public override void Hide()
    {
        base.Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    public override void Subscribe()
    {
        base.Subscribe();
        goButton.onClick.AddListener(StartGame);

        for (int i = 0; i < busterButton.Length; i++)
        {
            int index = i;
            busterButton[index].onClick.AddListener(() => Buster(index));
        }
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();
        goButton.onClick.RemoveListener(StartGame);
        for (int i = 0; i < busterButton.Length; i++)
        {
            int index = i;
            busterButton[index].onClick.RemoveListener(() => Buster(index));
        }
    }
    public override void ResetPopup()
    {
       
    }

    public override void SetPopup()
    {
        freezText.text = PlayerPrefs.GetInt("Freez").ToString();
        slowmoText.text = PlayerPrefs.GetInt("Slowmo").ToString();
        magnetText.text = PlayerPrefs.GetInt("Magnet").ToString();
    }

    private void StartGame()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        Hide();
        UIManager.Instance.ShowScreen(ScreenTypes.Game);
        
    }

    private void Buster(int index)
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        BuyBonus buyBonus = (BuyBonus)UIManager.Instance.GetPopup(PopupTypes.BuyBonus);
        buyBonus.Init(busterIages[index], busterTitle[index], index);
        UIManager.Instance.ShowPopup(PopupTypes.BuyBonus);
    }

}