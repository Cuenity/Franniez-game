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

    public Button SkinsButton;
    public Button MusicButton;
    public Button CoinsButton;

    private float startTime;
    private float endTime;

    public delegate void ClickAction(string name);
    public static event ClickAction ChangeImage;

    public void Awake()
    {
        MusicPanel.SetActive(false);
        CoinsPanel.SetActive(false);

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

        GameAnalytics.NewDesignEvent("Shop:Time", endTime);
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

}
