﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class UIRollingManager : MonoBehaviour
{
    public Text amountCoinsText;
    public Text levelNumberText;
    public Text amountStickersText;

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
        //levelNumberText.text = "LEVEL " + levelIndicator;
    }

    private void OnEnable()
    {
        // Add Subscription
        Coin.PickedCoin += AddCoin;
        StickerObject.PickedSticker += AddSticker;
    }

    private void OnDisable()
    {
        Coin.PickedCoin -= AddCoin;
        StickerObject.PickedSticker -= AddSticker;
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