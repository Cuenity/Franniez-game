using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RollingPhaseManager : MonoBehaviour
{
    private int amountCoins = 0;
    private bool pickedSticker = false;

    private int levelNumber;
    private Player player;
    private Level level;


    private void Awake()
    {
        Scene scene = SceneManager.GetActiveScene();
        int.TryParse(scene.name, out levelNumber);

        player = PlayerDataController.instance.player;

        level = player.levels[levelNumber - 1];

        if (level.gotSticker)
        {
            pickedSticker = true;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        amountCoins = 0;
        level.playedLevel = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init() {
        Debug.Log("spawn rolling UI");
    }

    private void OnEnable()
    {
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
        pickedSticker = true;
    }

    private void AddCoin()
    {
        amountCoins++;
    }

    private void Finished()
    {
        level.gotSticker = pickedSticker;
        level.completed = true;

        int amountOfCoinsLevel = level.countCoins;
        if (amountCoins > amountOfCoinsLevel)
        {
            level.countCoins = amountCoins;
        }

        player.levels[levelNumber - 1] = level;
        player.coins += amountCoins;

        PlayerDataController.instance.player = player;
    }

}
