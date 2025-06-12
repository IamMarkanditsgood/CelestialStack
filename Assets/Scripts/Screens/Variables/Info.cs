using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Info : BasicScreen
{
    public AudioSource audioSource;
    public Button back;

    private void Start()
    {
        back.onClick.AddListener(Profile);
    }

    private void OnDestroy()
    {
        back.onClick.RemoveListener(Profile);
    }
    public override void ResetScreen()
    {
    }

    public override void SetScreen()
    {
    }

    private void Profile()
    {
        if (PlayerPrefs.GetInt("Sfx") == 1)
            audioSource.Play();
        UIManager.Instance.ShowScreen(ScreenTypes.Profile);
    }
}