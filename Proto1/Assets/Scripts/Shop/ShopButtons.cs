using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Timers;

public class ShopButtons : MonoBehaviour
{
    [SerializeField] private GameObject skinsPanel, musicPanel, coinsPanel;
    [SerializeField] public Text amountCoinsPlayer;
    [SerializeField] private GameObject warningPanel;

    // Private properties for GameAnalytics
    private float startTime;
    private float endTime;

    // Verander naar niet static events: Kijk naar Settings sound button voor voorbeeld
    public delegate void ClickAction(string name);
    public static event ClickAction ChangeImage;
    public event ClickAction BuySkinEvent;

    public void Awake()
    {
        musicPanel.SetActive(false);
        coinsPanel.SetActive(false);
        amountCoinsPlayer.text = PlayerDataController.instance.player.ShopCoins.ToString();

        //Start Time for GameAnal
        startTime = Time.time;
    }

    public void OnEnable()
    {
        GetComponentInParent<ShopFillSkins>().ButtonClicked += BuySkin;
    }

    public void ReturnMainMenu()
    {
        endTime = Time.time - startTime;
        GameAnalytics.NewDesignEvent("Shop:Time", (int)endTime);
        SceneSwitcher.Instance.AsynchronousLoadStart("StartMenu");
    }

    public void ShowSkinsPanel(string buttonName)
    {
        skinsPanel.SetActive(false);
        musicPanel.SetActive(false);
        coinsPanel.SetActive(false);
        warningPanel.SetActive(false);

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
        ChangeImage(buttonName);
    }

    public void BuyCoins(int amount)
    {
        PlayerDataController.instance.AddShopCoins(amount);
        UpdateCoins();
    }

    public void BuySkin(SkinObject skin, ShopSkinButton button)
    {

        if(PlayerDataController.instance.player.materialsByName.Contains(skin.skinName))
        {
            PlayerDataController.instance.SetActiveMaterial(skin.material);
            button.ChangeImage(skin);
        }
        else if (PlayerDataController.instance.RemoveShopCoins(skin.cost))
        {
            PlayerDataController.instance.AddMaterial(skin);
            PlayerDataController.instance.SetActiveMaterial(skin.material);
            button.ChangeImage(skin);
            UpdateCoins();
            return;
        }
        else
        {
            warningPanel.SetActive(true);
        }
    }

    private void UpdateCoins()
    {
        amountCoinsPlayer.text = PlayerDataController.instance.ReturnCoins().ToString();
    }

    public void Button_CloseWarningPanel()
    {
        warningPanel.SetActive(false);
    }

}
