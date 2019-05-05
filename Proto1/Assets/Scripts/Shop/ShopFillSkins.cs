using Assets.Scripts.Shop.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFillSkins : MonoBehaviour
{
    [SerializeField] private ShopCategory shopCategory;
    [SerializeField] private Button button;
    [SerializeField] private SimpleObjectPool simpleObjectPool;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject marginRight;

    private List<ShopSkinButton> skinButtons;
    private List<SkinObject> skins;

    public delegate void ClickAction(SkinObject cost, ShopSkinButton button);
    public event ClickAction ButtonClicked;


    // Start is called before the first frame update
    void Start()
    {
        skinButtons = new List<ShopSkinButton>();
        MakeButtons();
    }

    private void MakeButtons()
    {
        foreach (SkinObject skin in shopCategory.skins)
        {
            GameObject newButton = simpleObjectPool.GetObject();
            newButton.transform.SetParent(contentPanel);

            ShopSkinButton skinButton = newButton.GetComponent<ShopSkinButton>();
            skinButton.Setup(skin);
            skinButton.skinButton.onClick.AddListener(delegate { BuySkin(skin, skinButton); });
            skinButtons.Add(skinButton);
        }

        marginRight = (GameObject)GameObject.Instantiate(marginRight);
        marginRight.SetActive(true);
        marginRight.transform.SetParent(contentPanel);
    }

    void BuySkin(SkinObject skin, ShopSkinButton button)
    {
        ButtonClicked(skin, button);
        RefreshButtons();
    }

    private void RefreshButtons()
    {
        int i = 0;
        foreach (ShopSkinButton button in skinButtons)
        {
            button.ChangeImage(shopCategory.skins[i]);
            i++;
        }
    }
}
