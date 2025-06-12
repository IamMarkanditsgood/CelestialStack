using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Profile : BasicScreen
{
    [SerializeField] private AudioSource musicSource;
    public AudioSource audioSource;
    public Button back;
    public Button info; 
    public Button shop;
    public Button home;
    public Button progress;

    public Button music;
    public Button vibro;
    public Button sfx;

    public Sprite buttonOn;
    public Sprite buttonOff;

    public Button avatar;
    public TMP_InputField nameText;

    public AvatarManager avatarManager;

    private void Start()
    {
        nameText.text = PlayerPrefs.GetString("Name", "Use name");


        avatar.onClick.AddListener(avatarManager.PickFromGallery);

        back.onClick.AddListener(Back);
        info.onClick.AddListener(Info);
        shop.onClick.AddListener(Shop);
        home.onClick.AddListener(Home);
        progress.onClick.AddListener(Progress);

        music.onClick.AddListener(Music);
        vibro.onClick.AddListener(Vibro);
        sfx.onClick.AddListener(Sfx);
    }

    private void OnDestroy()
    {
        avatar.onClick.RemoveListener(avatarManager.PickFromGallery);

        back.onClick.RemoveListener(Back);
        info.onClick.RemoveListener(Info);
        shop.onClick.RemoveListener(Shop);
        home.onClick.RemoveListener(Home);
        progress.onClick.RemoveListener(Progress);

        music.onClick.RemoveListener(Music);
        vibro.onClick.RemoveListener(Vibro);
        sfx.onClick.RemoveListener(Sfx);

    }
    public override void ResetScreen()
    {
        if(PlayerPrefs.GetString("Name", "Use name") != nameText.text)
        {
            PlayerPrefs.SetString("Name", nameText.text);
        }
    }

    public override void SetScreen()
    {
        avatarManager.SetSavedPicture();
        nameText.text = PlayerPrefs.GetString("Name", "Use name");

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            musicSource.enabled = true;
            music.GetComponent<Image>().sprite = buttonOn;
        }
        else
        {
            musicSource.enabled = false;
            music.GetComponent<Image>().sprite = buttonOff;
        }

        if (PlayerPrefs.GetInt("Vibro") == 1)
        {
            vibro.GetComponent<Image>().sprite = buttonOn;
        }
        else
        {
            vibro.GetComponent<Image>().sprite = buttonOff;
        }
        if (PlayerPrefs.GetInt("Sfx") == 1)
        {
            sfx.GetComponent<Image>().sprite = buttonOn;
        }
        else
        {
            sfx.GetComponent<Image>().sprite = buttonOff;
        }
    }

    private void Music()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        if (PlayerPrefs.GetInt("Music") == 1)
        {
            PlayerPrefs.SetInt("Music", 0);
            music.GetComponent<Image>().sprite = buttonOff;
        }
        else
        {
            PlayerPrefs.SetInt("Music", 1);
            music.GetComponent<Image>().sprite = buttonOn;
        }
    }

    private void Vibro()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        if (PlayerPrefs.GetInt("Vibro") == 1)
        {
            PlayerPrefs.SetInt("Vibro", 0);
            vibro.GetComponent<Image>().sprite = buttonOff;
        }
        else
        {
            PlayerPrefs.SetInt("Vibro", 1);
            vibro.GetComponent<Image>().sprite = buttonOn;
        }
    }

    private void Sfx()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        if (PlayerPrefs.GetInt("Sfx") == 1)
        {
            PlayerPrefs.SetInt("Sfx", 0);
            sfx.GetComponent<Image>().sprite = buttonOff;
        }
        else
        {
            PlayerPrefs.SetInt("Sfx", 1);
            sfx.GetComponent<Image>().sprite = buttonOn;
        }
    }
    private void Shop()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Shop);
    }
    private void Home()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
    private void Progress()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Progress);
    }
    private void Info()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Info);
    }

    private void Back()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Home);
    }
}