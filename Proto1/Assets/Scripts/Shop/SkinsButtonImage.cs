using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkinsButtonImage : MonoBehaviour
{
    [SerializeField] private Sprite panel_On, panel_Off;

    private void OnEnable()
    {
        GetComponentInParent<ShopButtons>().ChangeImage += ChangeImage;
    }

    void ChangeImage(string name)
    {
        if(gameObject.name == name)
        {
            gameObject.GetComponent<Image>().sprite = panel_On;
        }
        else
        {
            gameObject.GetComponent<Image>().sprite = panel_Off;
        }
    }
}
