using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Achievement : BasicPopup
{
    
    public TMP_Text achieveName;
    public TMP_Text achievementDescription;
    public Image achievementImage;

    public string[] names;
    public string[] descriptions;
    public Sprite[] images; 

    private int currentAchieve;
    public void Init(int index)
    {
        currentAchieve = index;
    }
    public override void ResetPopup()
    {
    }

    public override void SetPopup()
    {
        achieveName.text= names[currentAchieve];
        achievementDescription.text = descriptions[currentAchieve];
        achievementImage.sprite = images[currentAchieve];
    }
}
