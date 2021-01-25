//using GameAnalyticsSDK;
using Photon.Pun;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    private GameState gameState;

    public delegate void ClickAction();
    public static event ClickAction ChangeEnvironment;

    public bool tutorialActive;

    void Start()
    {
        gameState = GameState.Instance;
    }

    public void StartButton()
    {
        if (tutorialActive && !gameState.tutorialManager.changeBallTutorial)
        {
            gameState.tutorialManager.TurnTutorialMaskOff();
            gameState.tutorialManager.RollingFinished = false;
        }

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

    public void ChooseLanguage(string pathFile)
    {
        LocalizationManager.instance.LoadLocalizedText(pathFile);

        PlayerData player = new PlayerData { name = "" };
        for (int i = 0; i < player.levels.Length; i++)
        {
            player.levels[i] = new Level();
        }

        player.materialsByName.Add("Franniez");
        player.activeMaterial = "Franniez";

        PlayerDataController.instance.Player = player;
        PlayerDataController.instance.Save();
        PlayerDataController.instance.SetMaterial();
        LocalizationManager.instance.SetLanguageForPlayer(pathFile);

        string language = LocalizationManager.instance.LanguageChoice.ToString();
        language = "Language:" + language;
        //GameAnalytics.NewDesignEvent(language);


        SceneManager.LoadScene("StartMenu");
    }

    private void RemoveFilledGridSpots(GameObject platform)
    {
        int filledGridSpotToRemove = platform.GetComponent<Platform>().fillsGridSpot;
        if (!platform.GetComponent<Cannon>())
        {
            GameState.Instance.gridManager.RemoveFilledGridSpots(filledGridSpotToRemove, SizeType.twoByOne);
        }
        else
        {
            GameState.Instance.gridManager.RemoveFilledGridSpots(filledGridSpotToRemove, SizeType.twoByTwo);
        }
    }

    public void RemoveAllPlayerPlatformsFromScene()
    {
        // the UpdatePlayerPlatforms method removes that platform from the placedPlatforms list
        while (gameState.levelManager.playerPlatforms.placedPlatforms.Count > 0)
        {
            RemoveFilledGridSpots(gameState.levelManager.playerPlatforms.placedPlatforms[0]);
            gameState.levelManager.playerPlatforms.placedPlatforms[0].GetComponent<Platform>().UpdatePlayerPlatforms();
        }
    }

    //private InventoryButton FindCorrectInventoryButton(InventoryButtonName inventoryButtonName)
    //{
    //    foreach (InventoryButton inventoryButton in gameState.UIManager.instantiatedInventoryButtons)
    //    {
    //        if (inventoryButton.name == inventoryButtonName.ToString())
    //        {
    //            return inventoryButton;
    //        }
    //    }
    //    return null;
    //}

    public void ChangeBall()
    {
        if (tutorialActive && gameState.tutorialManager.changeBallTutorial)
        {
            gameState.tutorialManager.TurnTutorialMaskOff();
            gameState.tutorialManager.RollingFinished = false;
        }
        // check of active bal in de lijst voorkomt en dan de volgende in de lijst spawnen
        gameState = GameState.Instance;
        GameObject prev_ball = gameState.playerBallManager.activePlayer;
        List<Bal> list = gameState.playerBallManager.ballList;
        int index = 0 ;
        foreach (Bal item in list)
        {
            if (index == list.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
            if (prev_ball.name.Contains(item.ToString()))
            {
                Destroy(prev_ball);
                gameState.playerBallManager.InitTypeBall(list[index]);
                break;
            }     
        }
    }

    public void ChangeCannonAngle()
    {
        transform.root.GetChild(0).GetChild(0).transform.localRotation = Quaternion.Euler(GetComponent<Slider>().value, 0, 0);
    }

    public void CannonSliderOnPointerDown()
    {
        gameState.playerCamera.platformDragActive = true;
    }

    public void CannonSliderOnPointerUp()
    {
        gameState.playerCamera.platformDragActive = false;
    }

    public void MenuButton()
    {
        gameState.UIManager.pauseMenu.enabled = true;      
        gameState.playerCamera.platformDragActive = true;
    }
}
