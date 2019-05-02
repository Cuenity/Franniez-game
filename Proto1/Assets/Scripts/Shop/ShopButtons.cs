using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Timers;

public class ShopButtons : MonoBehaviour
{
    [SerializeField]
    private GameObject SkinsPanel, MusicPanel, CoinsPanel;
    [SerializeField]
    public Text AmountCoinsPlayer;
    [SerializeField]
    private GameObject warningPanel;

    // Private properties for GameAnalytics
    private float startTime;
    private float endTime;

    // Voor test alleen, moet nog verwerkt worden naar DataController want daar wilt ie de 
    // ShopCoins niet opslaan
    private PlayerData player;

    public delegate void ClickAction(string name);
    public static event ClickAction ChangeImage;
    public event ClickAction BuySkinEvent;

    public void Awake()
    {
        MusicPanel.SetActive(false);
        CoinsPanel.SetActive(false);
        AmountCoinsPlayer.text = PlayerDataController.instance.player.ShopCoins.ToString();
        player = PlayerDataController.instance.player;

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
        SkinsPanel.SetActive(false);
        MusicPanel.SetActive(false);
        CoinsPanel.SetActive(false);
        warningPanel.SetActive(false);

        switch (buttonName)
        {
            case "Skins Button":
                SkinsPanel.SetActive(true);
                break;
            case "Music Button":
                MusicPanel.SetActive(true);
                break;
            case "Coins Button":
                CoinsPanel.SetActive(true);
                break;
            default:
                break;
        }
        ChangeImage(buttonName);
    }

    private void ChangeButton()
    {

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
        AmountCoinsPlayer.text = PlayerDataController.instance.ReturnCoins().ToString();
    }

    public void Button_CloseWarningPanel()
    {
        warningPanel.SetActive(false);
    }

}
