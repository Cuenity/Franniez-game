using Assets.Scripts.Shop.Items;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFillSkins : MonoBehaviour
{
    [SerializeField] private ShopCategory[] shopCategories;
    [SerializeField] private Button button;
    [SerializeField] private SimpleObjectPool simpleObjectPool;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject marginRight;

    private List<ShopSkinButton> skinButtons;
    private List<SkinObject> skins;

    public delegate void ClickAction(SkinObject cost, ShopSkinButton button);
    public event ClickAction ButtonClicked;

    void Start()
    {
        skins = new List<SkinObject>();
        skinButtons = new List<ShopSkinButton>();
        MakeButtons();
    }

    private void MakeButtons()
    {
        foreach (ShopCategory shopCategory in shopCategories)
        {
            foreach (SkinObject skin in shopCategory.skins)
            {
                GameObject newButton = simpleObjectPool.GetObject();
                newButton.transform.SetParent(contentPanel);

                ShopSkinButton skinButton = newButton.GetComponent<ShopSkinButton>();
                skinButton.Setup(skin);
                skinButton.skinButton.onClick.AddListener(delegate { BuySkin(skin, skinButton); });
                skinButtons.Add(skinButton);
                skins.Add(skin);
            }
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
        int index = 0;

        foreach (ShopSkinButton button in skinButtons)
        {
                button.ChangeImage(skins[index]);
                index++;
        }
    }
}
