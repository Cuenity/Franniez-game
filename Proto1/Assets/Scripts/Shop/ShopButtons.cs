using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class ShopButtons : MonoBehaviour
{
    public GameObject SkinsPanel;
    public GameObject MusicPanel;
    public GameObject CoinsPanel;

    public Button SkinsButton;
    public Button MusicButton;
    public Button CoinsButton;

    private GameObject lastPanel;

    public delegate void ClickAction(string name);
    public static event ClickAction ChangeImage;

    public void Awake()
    {
        MusicPanel.SetActive(false);
        CoinsPanel.SetActive(false);
        lastPanel = SkinsPanel;
    }
    public void ReturnMainMenu()
    {
        SendTimeAnal();
        SceneManager.LoadScene("StartMenu");
    }

    private void SendTimeAnal()
    {
        // Verstuur hoelang de gebruiker in de shop was
        // Misschien andere naam verzinnen
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
