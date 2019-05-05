using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinButton : MonoBehaviour
{
    [SerializeField] public Button skinButton;
    [SerializeField] private Text title, cost;
    [SerializeField] private Image skinImage, coinImage;
    [SerializeField] private Sprite owned, active, buyWithCoins;

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
                coinImage.sprite = active;
                cost.text = "Gekozen";
            }
            else
            {
                TransformText();
                coinImage.sprite = owned;
                cost.text = "Gekocht";
            }
        }
        else
        {
            coinImage.sprite = buyWithCoins;
            cost.text = skinObject.cost.ToString();
        }

        skinImage.sprite = skinObject.shopImage;
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
                coinImage.sprite = active;
                cost.text = "Gekozen";
            }
            else
            {
                TransformText();
                coinImage.sprite = owned;
                cost.text = "Gekocht";
            }
        }
        else
        {
            coinImage.sprite = buyWithCoins;
            cost.text = skin.cost.ToString();
        }
    }

}
