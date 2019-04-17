﻿using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameState gameState;

    public delegate void ClickAction();
    public static event ClickAction ChangeEnvironment;

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
            gameState.levelManager.SetRollingPhase();
        }
        else if (gameState.RollingPhaseActive == true)
        {
            gameState.levelManager.SetBuildingPhase();
        }
        ChangeEnvironment();
    }
    

    public void InventoryButton(GameObject platformSquare)
    {
        //Instantiate(platformSquare);
        //var pos = Input.mousePosition;

    }

    public void TestLang(string pathFile)
    {
        LocalizationManager.instance.LoadLocalizedText(pathFile);


        Player player = new Player{name = ""};
        PlayerDataController.instance.player = player;
        PlayerDataController.instance.Save();
        LocalizationManager.instance.ReturnLanguage(pathFile);
        SceneManager.LoadScene("StartMenu");
    }

    public void ReturnMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void NextLevel()
    {
        int levelNumber = 2;
        levelNumber = GameState.Instance.PreviousLevel + 1;

        GameState.Instance.levelManager.AsynchronousLoadStart(Convert.ToString(levelNumber));
        //SceneManager.LoadScene(levelNumber.ToString());
    }

    public void RestartScene()
    {
        int levelNumber = 1;
        //int levelNumber = GameState.Instance.PreviousLevel;

        SceneManager.LoadScene(levelNumber.ToString());
    }
}
