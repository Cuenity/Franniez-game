using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinButton : MonoBehaviour
{
    public Button skinButton;
    public Text title;
    public Text cost;
    public Image image;
    public Sprite owned;
    public Sprite active;
    public Sprite buyWithCoins;
    public Image buttonImage;

    private SkinObject skinObject;

    public void Setup(SkinObject skin)
    {
        skinObject = skin;
        title.text = skinObject.skinName;

        if(PlayerDataController.instance.player.materialsByName.Contains(skin.skinName))
        {
            if( PlayerDataController.instance.ballMaterial == skin.material)
            {
                TransformText();
                buttonImage.sprite = active;
                cost.text = "Gekozen";
            }
            else
            {
                TransformText();
                buttonImage.sprite = owned;
                cost.text = "Gekocht";
            }
        }
        else
        {
            buttonImage.sprite = buyWithCoins;
            cost.text = skinObject.cost.ToString();
        }

        image.sprite = skinObject.shopImage;
    }

    private void TransformText()
    {
        cost.GetComponent<RectTransform>().localPosition = new Vector3(0, 6);
        cost.resizeTextMaxSize = 46;
    }

    public void ChangeImage(SkinObject skin)
    {
        if (PlayerDataController.instance.player.materialsByName.Contains(skin.skinName))
        {
            if (PlayerDataController.instance.ballMaterial == skin.material)
            {
                TransformText();
                buttonImage.sprite = active;
                cost.text = "Gekozen";
            }
            else
            {
                TransformText();
                buttonImage.sprite = owned;
                cost.text = "Gekocht";
            }
        }
        else
        {
            buttonImage.sprite = buyWithCoins;
            cost.text = skin.cost.ToString();
        }
    }

}
