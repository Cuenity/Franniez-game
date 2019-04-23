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

    // Private properties for GameAnalytics
    private float startTime;
    private float endTime;

    // Voor test alleen, moet nog verwerkt worden naar DataController want daar wilt ie de 
    // ShopCoins niet opslaan
    private Player player;

    public delegate void ClickAction(string name);
    public static event ClickAction ChangeImage;

    public void Awake()
    {
        MusicPanel.SetActive(false);
        CoinsPanel.SetActive(false);
        AmountCoinsPlayer.text = PlayerDataController.instance.player.ShopCoins.ToString();
        player = PlayerDataController.instance.player;

        //Start Time for GameAnal
        startTime = Time.time;
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
        player.ShopCoins = player.ShopCoins + amount;
        PlayerDataController.instance.player = player;
        UpdateCoins();
    }

    public void BuySkin(int amount)
    {

        // Check morgen (24 april) welke hij pakt - 23 April
        if (amount <= player.ShopCoins)
        {
            player.ShopCoins -= amount;
            PlayerDataController.instance.player = player;
            UpdateCoins();
        }

        if (PlayerDataController.instance.RemoveShopCoins(amount))
        {
            UpdateCoins();
            return;
        }
        else
        {
            // Geef melding dat gebruiker niet genoeg punten heeft.
        }
    }

    private void UpdateCoins()
    {
        // Check morgen (24 april) welke hij pakt - 23 April
        AmountCoinsPlayer.text = PlayerDataController.instance.ReturnCoins().ToString();
        AmountCoinsPlayer.text = player.ShopCoins.ToString();
    }

}
