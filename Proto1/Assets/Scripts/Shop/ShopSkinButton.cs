﻿using System.Collections;
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
    private SkinObject[] listSkins;
    private string textActive = LocalizationManager.instance.GetLocalizedValue("shop_Active");
    private string textOwned = LocalizationManager.instance.GetLocalizedValue("shop_Owned");

    public void Setup(SkinObject skin)
    {
        skinObject = skin;
        title.text = skinObject.skinName;
        skinImage.sprite = skinObject.shopImage;

        ChangeSkinCostText(skin);
    }

    public void SetupCat(ShopCategory category)
    {
        title.text = category.Name;
        listSkins = category.skins;
        skinImage.sprite = category.Image;
        ChangeBundleCostImage(category);
    }

    private void TransformText()
    {
        // Veranderd de positie van de text omdat de afbeelding van de coin weg is, moet de tekst
        // weer in het midden geplaatst worden
        cost.GetComponent<RectTransform>().localPosition = new Vector3(0, 6);
        cost.resizeTextMaxSize = 46;
    }


    public void ChangeBundleCostImage(ShopCategory category)
    {
        if(PlayerDataController.instance.player.categoriesByName.Contains(category.Name))
        {
            TransformText();
            coinImage.sprite = owned;
            cost.text = textOwned;
        }
        else
        {
            coinImage.sprite = buyWithCoins;
            cost.text = category.cost.ToString();
        }
    }

    public void ChangeSkinCostText(SkinObject skin)
    {
        if (PlayerDataController.instance.player.materialsByName.Contains(skin.skinName))
        {
            if (PlayerDataController.instance.ballMaterial == skin.material)
            {
                TransformText();
                coinImage.sprite = active;
                cost.text = textActive;
            }
            else
            {
                TransformText();
                coinImage.sprite = owned;
                cost.text = textOwned;
            }
        }
        else
        {
            coinImage.sprite = buyWithCoins;
            cost.text = skin.cost.ToString();
        }

    }

}
