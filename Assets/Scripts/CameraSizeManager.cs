using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeManager : MonoBehaviour
{
    [SerializeField] private Transform sceneRoot;
    [SerializeField, Range(0f, 1f)] private float baseAspect = 0.33f;

    private void Start()
    {
        AdjustSceneSize();
    }

    public void AdjustSceneSize()
    {
        if (sceneRoot == null)
        {
            Debug.LogWarning("Scene Root is not assigned.");
            return;
        }

        float currentAspect = (float)Screen.width / Screen.height;
        float scale = currentAspect / baseAspect;

        sceneRoot.localScale = new Vector3(scale, sceneRoot.localScale.y, 1);
        Debug.Log("Scene size adjusted.");
    }
}
