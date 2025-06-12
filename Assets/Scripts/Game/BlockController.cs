using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public AudioSource audioSource;
    public Transform upPoint;

    public bool isVibrated;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isVibrated)
            TriggerVibration();
    }

    private void TriggerVibration()
    {
        
        if (PlayerPrefs.GetInt("Sfx") == 1)
        {
            
            audioSource.Play();
        }

        isVibrated = true;
#if UNITY_ANDROID || UNITY_IOS
        if (PlayerPrefs.GetInt("Vibro") ==1 )
            Handheld.Vibrate(); // Стандартна коротка вібрація
#endif

        
    }
}
