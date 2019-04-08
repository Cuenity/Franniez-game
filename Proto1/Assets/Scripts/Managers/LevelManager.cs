using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    GameState gameState;
    Scene currentScene;

    public PlayerPlatforms playerPlatforms;
    //public PlayerPlatforms PlayerPlatforms
    //{
    //    get
    //    {
    //        return playerPlatforms;
    //    }
    //    set
    //    {
    //        playerPlatforms = value;
    //    }
    //}

    public LevelPlatformen levelPlatformen = new LevelPlatformen();


    //ik wil levels uit een textbestand kunnen opslaan en uitlezen ga ik proberen hier

    public void ReadLevelsFromText(string levelName)
    {
        if (levelName == "")
        {
            InputField InputName = GameObject.Find("LevelName").GetComponent<InputField>();
            levelName = InputName.text;
        }
        string filePath = Application.streamingAssetsPath + "/" + levelName + ".json";

        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            levelPlatformen = JsonUtility.FromJson<LevelPlatformen>(dataAsJSON);
        }
        else
        {
            //moet file createn
            Debug.LogError("Cannot find file!");
        }
        GameState.Instance.platformManager.BuildLevelFromText(levelPlatformen);
    }

    public void SaveLevelToText(string levelName)
    {
        
        if (levelName == "")
        {
            InputField InputName = GameObject.Find("LevelName").GetComponent<InputField>();
            levelName = InputName.text;
        }
        string filePath = Application.streamingAssetsPath +"/" + levelName +".json";
        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(gameState.levelManager.levelPlatformen);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void Awake()
    {
        gameState = GameState.Instance;
        levelPlatformen = new LevelPlatformen();
        //gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }

    public void InitScene()
    {
        currentScene = SceneManager.GetActiveScene();

        //gameState.platformManager.spawnLevel1();
        //gameState.platformManager.Init_Platforms();

        if (currentScene.name == "1")
        {
            gameState.gridManager.width = 11;
            gameState.gridManager.heigth = 12;
            gameState.gridManager.Build_Grid1_Without_Visuals();
            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
            playerPlatforms = new PlayerPlatforms(2, 3);
            List<Vector3> coinPositions = new List<Vector3>();
            coinPositions.Add(new Vector3(1, 1.5f, 0));
            coinPositions.Add(new Vector3(1, -2, 0));
            coinPositions.Add(new Vector3(1, -3, 0));

            Vector3 stickerPosition = new Vector3(1, -2, 0);
            Vector3 finishPosition = new Vector3(1, -10, 0);

            gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
        }

        else if (currentScene.name == "TestLevel1")
        {

            //lees level uit Json en vul levelPlatformen
            //ReadLevelsFromText("Level1.json");

            playerPlatforms = new PlayerPlatforms(2, 3);

            //Dit moet ergens anders
            gameState.gridManager.width = 11;
            gameState.gridManager.heigth = 12;

            //deze code is superbelangrijk voor het opslaan van levels
            //levelPlatformen.width = 11;
            //levelPlatformen.heigth = 12;
            //levelPlatformen.tileList = new int[11*12];

            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];
            gameState.gridManager.Build_Grid1_Without_Visuals();

            gameState.platformManager.BuildLevelFromText(levelPlatformen);
            //gameState.platformManager.Build_Level1(levelPlatformen);



            //SaveLevelToText("Level1.json");
        }

        else if (currentScene.name == "LevelEditor")
        {
            gameState.gridManager.width = 11;
            gameState.gridManager.heigth = 12;

            playerPlatforms = new PlayerPlatforms(2, 3);

            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
        }
        else if (currentScene.name == "TestJaspe")
        {
            playerPlatforms = new PlayerPlatforms(2, 6);
            
            gameState.gridManager.width = 20;
            gameState.gridManager.heigth = 12;
            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];
           // gameState.gridManager.Build_Grid1_Without_Visuals();
            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
            gameState.playerManager.player.SetSpawnpoint(1);
            gameState.platformManager.Build_Level2();
        }
        
        else if (currentScene.name == "VerticalSliceLevel2")
        {
            gameState.gridManager.width = 19;
            gameState.gridManager.heigth = 7;

            playerPlatforms = new PlayerPlatforms(2, 3);

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();

            gameState.platformManager.Build_Vertical_Slice_Level2();
        }
        else if (currentScene.name == "VerticalSliceLevel1")
        {
            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
            gameState.gridManager.width = 8;
            gameState.gridManager.heigth = 7;

            playerPlatforms = new PlayerPlatforms(2, 3);

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;

            gameState.platformManager.Build_Vertical_Slice_Level1();
        }
        else if (currentScene.name == "VerticalSliceLevel3")
        {
            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
            gameState.gridManager.width = 27;
            gameState.gridManager.heigth = 17;

            playerPlatforms = new PlayerPlatforms(2, 3);
            
            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[0] + playeradjustment;
            gameState.platformManager.Build_Vertical_Slice_Level3();
        }
    }
}
