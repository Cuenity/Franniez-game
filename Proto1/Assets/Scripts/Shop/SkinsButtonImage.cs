using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsButtonImage : MonoBehaviour
{
    // Side buttons for Shop

    [SerializeField] private Sprite panel_OnSprite, panel_OffSprite;

    private void OnEnable()
    {
        GetComponentInParent<ShopButtons>().ChangeImage += ChangeImage;
    }

    void ChangeImage(string name)
    {
        if(gameObject.name == name)
        {
            gameObject.GetComponent<Image>().sprite = panel_OnSprite;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = panel_OffSprite;
        }
    }
}
