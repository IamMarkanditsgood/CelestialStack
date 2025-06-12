using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockController : MonoBehaviour
{
    public Transform upPoint;

    public bool isVibrated;

    private void OnCollisionEnter(Collision collision)
    {
        if(!isVibrated)
            TriggerVibration();
    }

    private void TriggerVibration()
    {
        isVibrated = true;
#if UNITY_ANDROID || UNITY_IOS
        Handheld.Vibrate(); // Стандартна коротка вібрація
#endif
    }
}
