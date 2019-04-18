using GameAnalyticsSDK;
using System;
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

    public void ChooseLanguage(string pathFile)
    {
        LocalizationManager.instance.LoadLocalizedText(pathFile);

        Player player = new Player { name = "" };
        for (int i = 0; i < player.levels.Length; i++)
        {
            player.levels[i] = new Level();
        }
        
        PlayerDataController.instance.player = player;
        PlayerDataController.instance.Save();
        LocalizationManager.instance.ReturnLanguage(pathFile);

        string language = LocalizationManager.instance.LanguageChoice.ToString();
        language = "Language:" + language;
        GameAnalytics.NewDesignEvent(language);


        SceneManager.LoadScene("StartMenu");
    }

    public void ReturnMenu()
    {
        //pas aanzetten als de levelselect er is
        //GameState.Instance.levelManager.AsynchronousLoadStart("LevelSelect");
    }

    public void NextLevel()
    {
        int levelNumber = 2;
        levelNumber = GameState.Instance.PreviousLevel + 1;

        GameState.Instance.levelManager.AsynchronousLoadStart(Convert.ToString(levelNumber));
    }

    public void RestartScene()
    {
        int levelNumber = 1;
        levelNumber = GameState.Instance.PreviousLevel;

        GameState.Instance.levelManager.AsynchronousLoadStart(levelNumber.ToString());
    }

    public void RemoveAllPlayerPlatformsFromScene()
    {
        int placedPlatformsAmount = gameState.levelManager.playerPlatforms.placedPlatforms.Count;

        while (placedPlatformsAmount > 0)
        {
            UpdatePlayerPlatforms(gameState.levelManager.playerPlatforms.placedPlatforms[0]);
            placedPlatformsAmount--;
        }
    }

    public void UpdatePlayerPlatforms(GameObject playerPlatform)
    {
        if (playerPlatform.tag == "PlatformSquare")
        {
            InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.platformSquareButton);
            GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace++;
            GameState.Instance.levelManager.playerPlatforms.UpdatePlatformSquaresLeft(button);
            button.InventoryButtonAllowed = true;
        }
        else if (playerPlatform.tag == "Ramp")
        {
            InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.rampInventoryButton);
            GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace++;
            GameState.Instance.levelManager.playerPlatforms.UpdateRampsLeft(button);
            button.InventoryButtonAllowed = true;
        }
        else if (playerPlatform.tag == "Trampoline")
        {
            InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.trampolineButton);
            GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace++;
            GameState.Instance.levelManager.playerPlatforms.UpdateTrampolinesLeft(button);
            button.InventoryButtonAllowed = true;
        }
        else if (playerPlatform.tag == "Booster")
        {
            InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.boostPlatformButton);
            GameState.Instance.levelManager.playerPlatforms.boostPlatformsLeftToPlace++;
            GameState.Instance.levelManager.playerPlatforms.UpdateBoostPlatformsLeft(button);
            button.InventoryButtonAllowed = true;
        }

        GameState.Instance.gridManager.RemoveFilledGridSpots(playerPlatform.GetComponent<Platform>().fillsGridSpot);
        GameState.Instance.levelManager.playerPlatforms.placedPlatforms.Remove(playerPlatform);
        Destroy(playerPlatform);
        GameState.Instance.playerCamera.platformDragActive = false;

    }

    private InventoryButton FindCorrectInventoryButton(InventoryButtonName inventoryButtonName)
    {
        foreach (InventoryButton inventoryButton in gameState.UIManager.instantiatedInventoryButtons)
        {
            if (inventoryButton.name == inventoryButtonName.ToString())
            {
                return inventoryButton;
            }
        }
        return null;
    }

}
