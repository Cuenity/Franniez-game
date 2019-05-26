using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class UIRollingManager : MonoBehaviour
{
    [SerializeField]
    private Text amountCoinsText, levelNumberText, amountStickersText;
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Sprite startRolling, startBuilding;

    private int amountCoins = 0;
    private string levelIndicator;
    private int amountStickers = 0;

    // Use this for initialization
    void Start()
    {
        //omdat ik niet prefabs van al die dingetjes wil maken doe ik de assignment hier mischien dat erwin een slimme oplossing 
        Canvas canvas = GameState.Instance.UIManager.canvas; //GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
        amountCoinsText = canvas.gameObject.transform.Find("CoinsText").GetComponent<Text>();
        levelNumberText = canvas.gameObject.transform.Find("Level Text").GetComponent<Text>();
        amountStickersText = canvas.gameObject.transform.Find("Sticker Text").GetComponent<Text>();
        startButton = canvas.gameObject.transform.Find("StartButton").GetComponent<Button>();


        amountCoinsText.text = "0/3";
        amountStickersText.text = "0/1";

        // Dit wil later uit een Scenemanager halen. 
        levelIndicator = PlayerDataController.instance.PreviousScene.ToString();
        levelNumberText.text = "LEVEL " + levelIndicator;
        if (LocalizationManager.instance.LanguageChoice == Language.Spanish)
        {
            levelNumberText.text = "NIVEL " + levelIndicator;
        }
        if (PhotonNetwork.InRoom)
        {
            levelNumberText.text = "Co-op";
        }
        if (GameState.Instance.BuildingPhaseActive)
        {
            startButton.GetComponent<Image>().sprite = startBuilding;
        }
        else
        {
            startButton.GetComponent<Image>().sprite = startRolling;
        }
    }

    private void OnEnable()
    {
        // Add Subscription
        Coin.PickedCoin += AddCoin;
        StickerObject.PickedSticker += AddSticker;
        ButtonManager.ChangeEnvironment += ChangeEnviroment;
    }

    private void OnDisable()
    {
        Coin.PickedCoin -= AddCoin;
        StickerObject.PickedSticker -= AddSticker;
        ButtonManager.ChangeEnvironment -= ChangeEnviroment;
    }

    public void ChangeEnviroment()
    {
        amountCoins = 0;
        amountCoinsText.text = "0/3";

        amountStickers = 0;
        amountStickersText.text = "0/1";
        if (GameState.Instance.BuildingPhaseActive)
        {
            startButton.GetComponent<Image>().sprite = startBuilding;
        }
        else
        {
            startButton.GetComponent<Image>().sprite = startRolling;
        }
    }

    private void AddSticker()
    {
        amountStickers++;
        amountStickersText.text = amountStickers + "/1";
    }

    private void AddCoin()
    {
        amountCoins++;
        amountCoinsText.text = amountCoins + "/3";
    }
}
