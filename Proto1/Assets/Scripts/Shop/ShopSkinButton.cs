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
    private string textActive = LocalizationManager.instance.GetLocalizedValue("shop_Active");
    private string textOwned = LocalizationManager.instance.GetLocalizedValue("shop_Owned");

    public void Setup(SkinObject skin)
    {
        skinObject = skin;
        title.text = skinObject.skinName;
        skinImage.sprite = skinObject.shopImage;

        changeText(skin);
    }

    private void TransformText()
    {
        // Veranderd de positie van de text omdat de afbeelding van de coin weg is, moet de tekst
        // weer in het midden geplaatst worden
        cost.GetComponent<RectTransform>().localPosition = new Vector3(0, 6);
        cost.resizeTextMaxSize = 46;
    }

    // Kijk even waar deze wordt aangeroepen, want dit is nu alleen een doorlink methode
    public void ChangeImage(SkinObject skin)
    {
        changeText(skin);
    }

    private void changeText(SkinObject skin)
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
