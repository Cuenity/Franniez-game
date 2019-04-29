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

    private SkinObject skinObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Setup(SkinObject skin)
    {
        skinObject = skin;
        title.text = skinObject.skinName;

        if(PlayerDataController.instance.player.materialsByName.Contains(skin.skinName))
        {
            if( PlayerDataController.instance.ballMaterial == skin.material)
            {
                cost.text = "Gekozen";
            }
            else
            {
                cost.text = "Gekocht";
            }
            
        }
        else
        {
            cost.text = skinObject.cost.ToString();
        }

        image.sprite = skinObject.shopImage;
    }

}
