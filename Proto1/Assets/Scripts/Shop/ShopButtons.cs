using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Timers;

public class ShopButtons : MonoBehaviour
{
    public GameObject SkinsPanel;
    public GameObject MusicPanel;
    public GameObject CoinsPanel;
    public Text AmountCoinsPlayer;

    private float startTime;
    private float endTime;

    // Voor test alleen, moet nog verwerkt worden naar DataController want daar wilt ie de 
    // ShopCoins niet opslaan
    private Player player = PlayerDataController.instance.player;

    public delegate void ClickAction(string name);
    public static event ClickAction ChangeImage;

    public void Awake()
    {
        MusicPanel.SetActive(false);
        CoinsPanel.SetActive(false);
        AmountCoinsPlayer.text = PlayerDataController.instance.player.ShopCoins.ToString();

        //Start Time for GameAnal
        startTime = Time.time;
    }
    public void ReturnMainMenu()
    {
        endTime = Time.time - startTime;

        SendTimeAnal();
        SceneManager.LoadScene("StartMenu");
    }

    private void SendTimeAnal()
    {
        // Verstuur hoelang de gebruiker in de shop was
        // Misschien andere naam verzinnen
        //string seconds = durationShop.ToString();

        int castTimeToInt = (int)endTime;
        Debug.Log(castTimeToInt);
        GameAnalytics.NewDesignEvent("Shop:Time", castTimeToInt);
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
        UpdateCoins();
    }

    public void BuySkin(int amount)
    {
        if (amount <= player.ShopCoins)
        {
            player.ShopCoins -= amount;
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
        AmountCoinsPlayer.text = PlayerDataController.instance.ReturnCoins().ToString();
        AmountCoinsPlayer.text = player.ShopCoins.ToString();
    }

}
