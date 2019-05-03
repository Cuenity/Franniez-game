using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotEnoughCoinsText : MonoBehaviour
{
    [SerializeField]
    private Text text;

    private void OnEnable()
    {
        GetComponentInParent<ShopFillSkins>().ButtonClicked += SetText;
    }

    public void SetText(SkinObject skin, ShopSkinButton button)
    {
        text.text = PlayerDataController.instance.player.ShopCoins.ToString() + "/" + skin.cost;
    }
}
