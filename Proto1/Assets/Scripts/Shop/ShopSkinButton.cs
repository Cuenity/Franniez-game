using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopSkinButton : MonoBehaviour
{
    [SerializeField] public Button skinButton;
    [SerializeField] private Text titleText, costText;
    [SerializeField] private Image skinImage, coinImage;
    [SerializeField] private Sprite ownedSprite, activeSprite, buyWithCoinsSprite;

    private SkinObject skinObject;
    private SkinObject[] listSkins;
    private readonly string textActive = LocalizationManager.instance.GetLocalizedValue("shop_Active");
    private readonly string textOwned = LocalizationManager.instance.GetLocalizedValue("shop_Owned");

    // Setup for single skins
    public void Setup(SkinObject skin)
    {
        skinObject = skin;
        titleText.text = skinObject.skinName;
        skinImage.sprite = skinObject.shopImage;

        ChangeSkinCostText(skin);
    }

    // Setup for category skins
    public void SetupCategory(ShopCategory category)
    {
        titleText.text = category.Name;
        listSkins = category.skins;
        skinImage.sprite = category.Image;
        ChangeBundleCostImage(category);
    }

    private void TransformText()
    {
        // Veranderd de positie van de text omdat de afbeelding van de coin weg is, moet de tekst
        // weer in het midden geplaatst worden
        costText.GetComponent<RectTransform>().localPosition = new Vector3(0, 6);
        costText.resizeTextMaxSize = 46;
    }


    public void ChangeBundleCostImage(ShopCategory category)
    {
        if(PlayerDataController.instance.Player.categoriesByName.Contains(category.Name))
        {
            TransformText();
            coinImage.sprite = ownedSprite;
            costText.text = textOwned;
        }
        else
        {
            coinImage.sprite = buyWithCoinsSprite;
            costText.text = category.cost.ToString();
        }
    }

    public void ChangeSkinCostText(SkinObject skin)
    {
        if (PlayerDataController.instance.Player.materialsByName.Contains(skin.skinName))
        {
            // Set right adjustment for text
            TransformText();
            if (PlayerDataController.instance.ballMaterial == skin.material)
            {
                coinImage.sprite = activeSprite;
                costText.text = textActive;
            }
            else
            {
                coinImage.sprite = ownedSprite;
                costText.text = textOwned;
            }
        }
        else
        {
            coinImage.sprite = buyWithCoinsSprite;
            costText.text = skin.cost.ToString();
        }

    }

}
