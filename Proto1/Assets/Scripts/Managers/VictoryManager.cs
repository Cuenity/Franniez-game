using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Photon.Pun;

public class VictoryManager : MonoBehaviour
{
    [SerializeField] private Image ribbonPlaceHolder, starImagePlaceHolder;
    [SerializeField] private Sprite star1, stars2, stars3, stars0;
    [SerializeField] private Sprite ribbon_Dutch, ribbon_English, ribbon_Spanish;
    [SerializeField] private Canvas VictoryCanvas;
    [SerializeField] private Text amountCoins;

    private PlayerData player;
    private PlayerDataController playerDataController;

    void Start()
    {
        player = PlayerDataController.instance.Player;
        playerDataController = PlayerDataController.instance;

        SetLanguage();
        GetDataFromLevel();
        SetButtonsForPhoton();
    }

    private void SetButtonsForPhoton()
    {
        // Zet buttons uit in MP voor niet host voorkomt rare situaties waar de client en host niet dezelfde scene hebben
        if (PhotonNetwork.InRoom)
        {
            if (!PhotonNetwork.IsMasterClient)
            {
                Button[] buttons = VictoryCanvas.GetComponentsInChildren<Button>();
                foreach (Button button in buttons)
                {
                    button.interactable = false;
                }
            }
        }
    }

    private void SetLanguage()
    {
        LocalizationManager.instance.SetLanguage();
        Language language = LocalizationManager.instance.GetLanguage();

        switch (LocalizationManager.instance.GetLanguage())
        {
            case Language.Dutch:
                ribbonPlaceHolder.sprite = ribbon_Dutch;
                break;
            case Language.English:
                ribbonPlaceHolder.sprite = ribbon_English;
                break;
            case Language.Spanish:
                ribbonPlaceHolder.sprite = ribbon_Spanish;
                break;
        }
    }

    private void GetDataFromLevel()
    {
        switch (playerDataController.PreviousSceneCoinCount)
        {
            case 1:
                starImagePlaceHolder.sprite = star1;
                break;
            case 2:
                starImagePlaceHolder.sprite = stars2;
                break;
            case 3:
                starImagePlaceHolder.sprite = stars3;
                break;
            default:
                starImagePlaceHolder.sprite = stars0;
                break;
        }

        // Add collected stars to shop coins
        playerDataController.AddShopCoins(playerDataController.PreviousSceneCoinCount);
        amountCoins.text = playerDataController.Player.ShopCoins.ToString();
    }

    public void Restart()
    {
        if (PhotonNetwork.InRoom)
        {
            //vieze fix fixen na releasecandidate
            if(playerDataController.PreviousScene == 101)
            {
                PhotonNetwork.LoadLevel(8);
            }
            else if (playerDataController.PreviousScene == 102)
            {

                PhotonNetwork.LoadLevel(9);
            }
            else if (playerDataController.PreviousScene == 103)
            {

                PhotonNetwork.LoadLevel(11);
            }
            else if (playerDataController.PreviousScene == 104)
            {

                PhotonNetwork.LoadLevel(12);
            }
            else if (playerDataController.PreviousScene == 105)
            {
                PhotonNetwork.LoadLevel(13);

            }
            else if (playerDataController.PreviousScene == 106)
            {

                PhotonNetwork.LoadLevel(14);
            }
            else if (playerDataController.PreviousScene == 107)
            {

                PhotonNetwork.LoadLevel(15);
            }
            else if (playerDataController.PreviousScene == 108)
            {

                PhotonNetwork.LoadLevel(16);
            }
            else if (playerDataController.PreviousScene == 109)
            {
                PhotonNetwork.LoadLevel(17);
            }
            else if (playerDataController.PreviousScene == 110)
            {

                PhotonNetwork.LoadLevel(18);
            }
            return;
        }
        SceneSwitcher.Instance.AsynchronousLoadStart("1");
    }

    public void NextScene()
    {
        
        if (PhotonNetwork.InRoom)
        {
            //vieze fix fixen na releasecandidate
            if (playerDataController.PreviousScene == 101)
            {

                PhotonNetwork.LoadLevel(9);
            }
            else if (playerDataController.PreviousScene == 102)
            {

                PhotonNetwork.LoadLevel(11);
            }
            else if (playerDataController.PreviousScene == 103)
            {

                PhotonNetwork.LoadLevel(12);
            }
            else if (playerDataController.PreviousScene == 104)
            {

                PhotonNetwork.LoadLevel(13);
            }
            else if (playerDataController.PreviousScene == 105)
            {
                PhotonNetwork.LoadLevel(14);

            }
            else if (playerDataController.PreviousScene == 106)
            {
                PhotonNetwork.LoadLevel(15);
            }
            else if (playerDataController.PreviousScene == 107)
            {

                PhotonNetwork.LoadLevel(16);
            }
            else if (playerDataController.PreviousScene == 108)
            {

                PhotonNetwork.LoadLevel(17);
            }
            else if (playerDataController.PreviousScene == 109)
            {
                PhotonNetwork.LoadLevel(18);
            }
            else if (playerDataController.PreviousScene == 110)
            {

                PhotonNetwork.LoadLevel(18);
            }
        }
        playerDataController.PreviousScene++;
        SceneSwitcher.Instance.AsynchronousLoadStart("1");
    }

    public void ReturnToMenu()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.LoadLevel(10);
            return;
        }
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("LevelSelect");
    }
}
