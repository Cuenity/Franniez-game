using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIRollingManager : MonoBehaviour
{
    public Text amountCoinsText;
    public Text levelNumberText;
    public Text amountStickersText;
    public Button startButton;

    public Sprite startRolling;
    public Sprite startBuilding;

    private int amountCoins =0;
    private string levelIndicator;
    private int amountStickers =0;
    private Scene currentScene;

    // Use this for initialization
    void Start()
    {
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
