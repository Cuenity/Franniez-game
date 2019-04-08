﻿using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameState gameState;

    void Start()
    {
        gameState = GameState.Instance;
    }

    void Update()
    {

    }

    public void StartButton()
    {
        if (gameState.BuildingPhaseActive == true)
        {
            gameState.BuildingPhaseActive = false;
            gameState.RollingPhaseActive = true;
        }
        else if (gameState.RollingPhaseActive == true)
        {
            gameState.playerManager.player.respawnBal();           
        }
    }

    public void InventoryButton(GameObject platformSquare)
    {
        //Instantiate(platformSquare);
        //var pos = Input.mousePosition;

    }

    public void TestLang(string pathFile)
    {
        LocalizationManager.instance.LoadLocalizedText(pathFile);

        Player player = new Player();
        player.name = "";
        player.language = (int)LocalizationManager.instance.LanguageChoice;
        PlayerDataController.instance.player = player;
        PlayerDataController.instance.Save();
        SceneManager.LoadScene("StartMenu");
    }
}
