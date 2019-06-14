using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Timers;

public class ShopButtons : MonoBehaviour
{
    // Serialize fields
    [SerializeField] private GameObject skinsPanel, musicPanel, coinsPanel;
    [SerializeField] public Text amountCoinsPlayer, notEnoughCoins, titleWarningPanel, buyCoinsText;
    [SerializeField] private GameObject warningPanel, notEnoughCoinsObject;

    // Private properties for GameAnalytics
    private float startTime;
    private float endTime;

    // Private properties
    private int buyAmountCoins = 0;
    private bool buyNewCoins = false;
    private PlayerDataController playerDataController;

    // Panel Category
    public delegate void ClickAction(string name);
    public event ClickAction ChangeImage;

    public void Awake()
    {
        // Disable other panels
        musicPanel.SetActive(false);
        coinsPanel.SetActive(false);

        playerDataController = PlayerDataController.instance;
        amountCoinsPlayer.text = playerDataController.Player.ShopCoins.ToString();

        //Start Time for GameAnal
        startTime = Time.time;
    }

    public void OnEnable()
    {
        GetComponentInParent<ShopFillSkins>().SkinButtonClicked += BuySkin;
        GetComponentInParent<ShopFillSkins>().BundleButtonClicked += BuyBundle;
    }

    public void ReturnMainMenu()
    {
        endTime = Time.time - startTime;
        GAManager.ShopTime(endTime);
        SceneSwitcher.Instance.AsynchronousLoadStart("StartMenu");
    }

    public void SwitchPanel(string buttonName)
    {
        // Disable all panels
        skinsPanel.SetActive(false);
        musicPanel.SetActive(false);
        coinsPanel.SetActive(false);
        warningPanel.SetActive(false);

        // Set selected panel on active
        switch (buttonName)
        {
            case "Skins Button":
                skinsPanel.SetActive(true);
                break;
            case "Music Button":
                musicPanel.SetActive(true);
                break;
            case "Coins Button":
                coinsPanel.SetActive(true);
                break;
            default:
                break;
        }

        // Change the panel buttons image
        ChangeImage(buttonName);
    }

    // Player wants to buy coins
    public void BuyCoins(int amount)
    {
        buyAmountCoins = amount;
        SetConfirmMessage(amount);
        warningPanel.SetActive(true);
    }

    public void BuySkin(SkinObject skin, ShopSkinButton button)
    {
        // If the player already bought the skin then it will only select the skin 
        if (playerDataController.Player.materialsByName.Contains(skin.skinName))
        {
            playerDataController.SetActiveMaterial(skin.material);
            button.ChangeSkinCostText(skin);
            GAManager.ShopSelectedSkin(skin.skinName);
        }

        // Buy skin when player has enough coins
        else if (playerDataController.RemoveShopCoins(skin.cost))
        {
            playerDataController.AddMaterial(skin);
            playerDataController.SetActiveMaterial(skin.material);
            button.ChangeSkinCostText(skin);
            UpdateCoins();
            GAManager.ShopBoughtSkin(skin.skinName);
        }

        // Player doesn't have enough coins
        else
        {
            SetWarningCoins(skin.cost);
            warningPanel.SetActive(true);
        }
    }

    // Buy multiple skins, bundles
    public void BuyBundle(ShopCategory category, ShopSkinButton button)
    {
        // Do nothing when the player already has the bundle
        if (playerDataController.Player.categoriesByName.Contains(category.Name)){return;}

        // Check if player has enough coins
        if (playerDataController.RemoveShopCoins(category.cost))
        {
            playerDataController.AddBundle(category);

            // Add all skins from the bundle
            foreach (SkinObject skin in category.skins)
            {
                playerDataController.AddMaterial(skin);
                button.ChangeSkinCostText(skin);
            }

            button.ChangeBundleCostImage(category);
            UpdateCoins();
            GAManager.ShopBoughtBundle(category);
        }
        else
        {
            SetWarningCoins(category.cost);
            warningPanel.SetActive(true);
        }

    }

    // Update the amount of coins 
    private void UpdateCoins()
    {
        amountCoinsPlayer.text = playerDataController.ReturnCoins().ToString();
    }

    public void Button_CloseWarningPanel()
    {
        warningPanel.SetActive(false);
    }

    public void Button_BuyCoins()
    {
        /*  
            Wanneer de panel actief werd door middel van munten kopen wordt de volgende
            methode uitgevoerd. Als dit niet het geval is kwam de panel tevoorschijn omdat er geen
            genoeg munten waren om een skin te kopen en wordt de gebruiker naar de Coins panel gestuurd.
        */
        if (buyNewCoins)
        {
            playerDataController.AddShopCoins(buyAmountCoins);
            UpdateCoins();
            warningPanel.SetActive(false);
            GAManager.ShopDonation(buyAmountCoins);
        }
        else
        {
            SwitchPanel("Coins Button");
        }
    }

    // Warning message for not enough coins
    private void SetWarningCoins(int cost)
    {
        buyCoinsText.enabled = false;
        titleWarningPanel.text = LocalizationManager.instance.GetLocalizedValue("shop_NotEnoughCoins");
        notEnoughCoinsObject.SetActive(true);
        notEnoughCoins.text = PlayerDataController.instance.Player.ShopCoins.ToString() + "/" + cost;
        buyNewCoins = false;
    }

    private void SetConfirmMessage(int coins)
    {
        // Set buyNewCoins on true, so the button knows which statement to start in the method Button_BuyCoins
        buyNewCoins = true;
        float money = 0.00f;

        switch (coins)
        {
            case 30:
                money = 0.99f;
                break;
            case 210:
                money = 4.99f;
                break;
            case 510:
                money = 9.99f;
                break;
            default:
                break;
        }

        buyCoinsText.enabled = true;

        // Set text from LocalizationManager in text
        string coinsLocalText = LocalizationManager.instance.GetLocalizedValue("shop_Coins");
        string forLocalText = LocalizationManager.instance.GetLocalizedValue("lang_for");
        buyCoinsText.text = $"€{money} {forLocalText} {coins} {coinsLocalText}?";
        titleWarningPanel.text = LocalizationManager.instance.GetLocalizedValue("shop_DonationMessage");
        notEnoughCoinsObject.SetActive(false);
    }
}
