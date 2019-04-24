using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIRollingManager : MonoBehaviour
{
    [SerializeField]
    private Text amountCoinsText, levelNumberText, amountStickersText;
    [SerializeField]
    private Button startButton;

    [SerializeField]
    private Sprite startRolling, startBuilding;

    private int amountCoins =0;
    private string levelIndicator;
    private int amountStickers =0;
    private Scene currentScene;

    // Use this for initialization
    void Start()
    {
        //omdat ik niet prefabs van al die dingetjes wil maken doe ik de assignment hier mischien dat erwin een slimme oplossing 
        Canvas canvas  = GameObject.FindGameObjectWithTag("UICanvas").GetComponent<Canvas>();
        amountCoinsText = canvas.gameObject.transform.Find("CoinsText").GetComponent<Text>();
        levelNumberText = canvas.gameObject.transform.Find("Level Text").GetComponent<Text>();
        amountStickersText = canvas.gameObject.transform.Find("Sticker Text").GetComponent<Text>();
        startButton = canvas.gameObject.transform.Find("StartButton").GetComponent<Button>();


        amountCoinsText.text = "0/3";
        amountStickersText.text = "0/1";

        // Dit wil later uit een Scenemanager halen. 
        currentScene = SceneManager.GetActiveScene();
        levelIndicator = currentScene.name;
        levelNumberText.text = "LEVEL " + levelIndicator;
        if (LocalizationManager.instance.LanguageChoice == Language.Spanish)
        {
            levelNumberText.text = "NIVEL " + levelIndicator;
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

    private void ChangeEnviroment()
    {
        amountCoins = 0;
        amountCoinsText.text = "0/3";

        amountStickers = 0;
        amountStickersText.text = "0/1";

        if(GameState.Instance.BuildingPhaseActive)
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

    // Update is called once per frame
    void Update()
    {

    }
}
