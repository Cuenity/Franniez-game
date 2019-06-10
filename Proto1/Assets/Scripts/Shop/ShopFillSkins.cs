using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopFillSkins : MonoBehaviour
{
    // Serializefields
    [SerializeField] private ShopCategory[] shopCategories;
    [SerializeField] private Button button;
    [SerializeField] private SimpleObjectPool simpleObjectPool;
    [SerializeField] private Transform soloSkinsContent, categorySkinsContent;
    [SerializeField] private GameObject marginRight;

    // Private properties
    private List<ShopSkinButton> skinButtons, categoryButtons;
    private List<SkinObject> skins;

    // Triggers
    public delegate void ClickAction(SkinObject cost, ShopSkinButton button);
    public delegate void ClickActionBundle(ShopCategory category, ShopSkinButton button);
    public event ClickAction SkinButtonClicked;
    public event ClickActionBundle BundleButtonClicked;

    void Start()
    {
        skins = new List<SkinObject>();
        skinButtons = new List<ShopSkinButton>();
        categoryButtons = new List<ShopSkinButton>();
        MakeButtons();
    }

    private void MakeButtons()
    {
        foreach (ShopCategory shopCategory in shopCategories)
        {
            // Fill the bundle category buttons
            if (shopCategory.categoryEnum != ShopCategoryEnum.Default)
            {
                GameObject buttonCat = simpleObjectPool.GetObject();
                buttonCat.transform.SetParent(categorySkinsContent);

                ShopSkinButton catbutton = buttonCat.GetComponent<ShopSkinButton>();
                catbutton.SetupCat(shopCategory);
                catbutton.skinButton.onClick.AddListener(delegate { BuyBundle(shopCategory, catbutton); });
                categoryButtons.Add(catbutton);
            }

            // FIll the single skins buttons
            foreach (SkinObject skin in shopCategory.skins)
            {
                GameObject newButton = simpleObjectPool.GetObject();
                newButton.transform.SetParent(soloSkinsContent);

                ShopSkinButton skinButton = newButton.GetComponent<ShopSkinButton>();
                skinButton.Setup(skin);
                skinButton.skinButton.onClick.AddListener(delegate { BuySkin(skin, skinButton); });
                skinButtons.Add(skinButton);
                skins.Add(skin);
            }

        }

        // Zet een margin aan de rechterkant voor scrollview voor skins en bundles
        SetRightMarginForButtons(soloSkinsContent);
        SetRightMarginForButtons(categorySkinsContent);
    }

    void BuySkin(SkinObject skin, ShopSkinButton button)
    {
        // Send info to ShopButtons
        SkinButtonClicked(skin, button);
        ChangeCostText();
    }

    void BuyBundle(ShopCategory category, ShopSkinButton button)
    {
        int amountBundlesFromPlayer = PlayerDataController.instance.Player.categoriesByName.Count;

        BundleButtonClicked(category, button);

        // If the player had enough money the previous amount is less then the new count
        if (amountBundlesFromPlayer < PlayerDataController.instance.Player.categoriesByName.Count)
        {
            ChangeCostText();
        }
    }

    private void ChangeCostText()
    {
        int index = 0;
        foreach (ShopSkinButton singleButton in skinButtons)
        {
            singleButton.ChangeSkinCostText(skins[index]);
            index++;
        }
    }

    private void SetRightMarginForButtons(Transform contentPanel)
    {
        /*      Margin Right
         *      
         *      Wanneer deze niet toegevoegd wordt sluit de laatste button direct
         *      tegen de rechterkant van de panel aan. Om een lege ruimte te creeeëren zoals
         *      aan de linkerkant moet er een EmpyObject toegevoegd worden. Dit kan alleen via code
         *      omdat er dynamisch buttons worden aangemaakt. 
         */

        marginRight = (GameObject)GameObject.Instantiate(marginRight);
        marginRight.SetActive(true);
        marginRight.transform.SetParent(contentPanel);
    }

}
