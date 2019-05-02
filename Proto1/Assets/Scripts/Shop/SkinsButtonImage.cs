using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsButtonImage : MonoBehaviour
{
    public Sprite PanelOn;
    public Sprite PanelOff;

    private void OnEnable()
    {
        ShopButtons.ChangeImage += ChangeImage;
    }

    private void OnDisable()
    {
        ShopButtons.ChangeImage -= ChangeImage;
    }

    void ChangeImage(string name)
    {
        if(gameObject.name == name)
        {
            gameObject.GetComponent<Image>().sprite = PanelOn;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = PanelOff;
        }
    }
}
