using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ButtonObject : MonoBehaviour
{
    [SerializeField] public Image image;
    [SerializeField] public Sprite on;
    [SerializeField] public Sprite of;
    [SerializeField] public bool volumeIsOn = true;

    public void ChangeIcon()
    {
        volumeIsOn = !volumeIsOn;
        if(volumeIsOn)
            image.sprite = on;
        else
            image.sprite = of;

    }


}
