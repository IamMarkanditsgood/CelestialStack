using UnityEngine;
using UnityEngine.UI;

public class Settings : BasicPopup
{
    public Button music;
    public Button vibro;
    public Button sfx;
    public Button save;

    public Sprite buttonOff;
    public Sprite buttonOn;

    private int musicState;
    private int vibroState;
    private int sfxState;

    public override void Hide()
    {
        base.Hide();
        Time.timeScale = 1;
    }
    public override void Subscribe()
    {
        base.Subscribe();
        music.onClick.AddListener(Music);
        vibro.onClick.AddListener(Vibro);
        sfx.onClick.AddListener(Sfx);
        save.onClick.AddListener(Save);
    }

    public override void Unsubscribe()
    {
        base.Unsubscribe();

        music.onClick.RemoveListener(Music);
        vibro.onClick.RemoveListener(Vibro);
        sfx.onClick.RemoveListener(Sfx);
        save.onClick.RemoveListener(Save);
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
        musicState = PlayerPrefs.GetInt("Music");
        vibroState = PlayerPrefs.GetInt("Vibro");
        sfxState = PlayerPrefs.GetInt("Sfx");

        if(musicState == 1)
        {
            music.gameObject.GetComponent<Image>().sprite = buttonOn;
        }
        else
        {
            music.gameObject.GetComponent<Image>().sprite = buttonOff;
        }

        if (vibroState == 1)
        {
            vibro.gameObject.GetComponent<Image>().sprite = buttonOn;
        }
        else
        {
            vibro.gameObject.GetComponent<Image>().sprite = buttonOff;
        }

        if (sfxState == 1)
        {
            sfx.gameObject.GetComponent<Image>().sprite = buttonOn;
        }
        else
        {
            sfx.gameObject.GetComponent<Image>().sprite = buttonOff;
        }

    }

    private void Music()
    {
        if (musicState == 1)
        {
            musicState = 0;
            music.GetComponent<Image>().sprite = buttonOff;
        }
        else
        {
            musicState = 1;
            music.GetComponent<Image>().sprite = buttonOn;
        }
    }

    private void Vibro()
    {
        if (vibroState == 1)
        {
            vibroState = 0;
            vibro.GetComponent<Image>().sprite = buttonOff;
        }
        else
        {
            vibroState = 1;
            vibro.GetComponent<Image>().sprite = buttonOn;
        }
    }

    private void Sfx()
    {
        if (sfxState == 1)
        {
            sfxState = 0;
            sfx.GetComponent<Image>().sprite = buttonOff;
        }
        else
        {
            sfxState = 1;
            sfx.GetComponent<Image>().sprite = buttonOn;
        }
    }

    private void Save()
    {
        PlayerPrefs.SetInt("Music", musicState);
        PlayerPrefs.SetInt("Vibro", vibroState);
        PlayerPrefs.SetInt("Sfx", sfxState);
        Hide();
    }
}
