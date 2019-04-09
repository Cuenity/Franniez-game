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
    List<Vector3> coinPositions = new List<Vector3>();
    Vector3 stickerPosition;
    Vector3 finishPosition;
    public List<Coin> coinList = new List<Coin>();
    public StickerObject stickerObject;
    public Finish finish;

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
        string filePath = Application.streamingAssetsPath + "/" + levelName + ".json";
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

    public void RespawnCollectables()
    {
        foreach (Coin item in coinList)
        {
            item.gameObject.SetActive(false);
        }
        stickerObject.gameObject.SetActive(false);
        finish.gameObject.SetActive(false);

        gameState.collectableManager.InitCollectables(coinPositions, stickerObject.spawnpoint, finish.spawnpoint);
    }
    public void SetCoinPositions(int i)
    {
        Vector3 coinAdjustment = new Vector3(.5f, 0, 0);
        coinPositions.Add(gameState.gridManager.gridSquares[i] + coinAdjustment);
    }
    public void SetStickerPositions(int i)
    {
        Vector3 stickerAdjustment = new Vector3(.5f, 0, 0);
      stickerPosition= gameState.gridManager.gridSquares[i] + stickerAdjustment;
    }
    public void SetfinishPositions(int i)
    {
        Vector3 finishAdjustment = new Vector3(.5f, 0, 0);
        finishPosition = gameState.gridManager.gridSquares[i] + finishAdjustment;
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
            gameState.playerManager.player.SetSpawnpoint(1);
            playerPlatforms = new PlayerPlatforms(2, 3, 1);
            SetCoinPositions(1);
            SetCoinPositions(2);
            SetCoinPositions(3);
            SetStickerPositions(4);
            SetfinishPositions(5);

            gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
        }

        else if (currentScene.name == "TestLevel1")
        {

            //lees level uit Json en vul levelPlatformen
            //ReadLevelsFromText("Level1.json");

            playerPlatforms = new PlayerPlatforms(2, 3, 1);

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

            playerPlatforms = new PlayerPlatforms(2, 3, 1);

            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
        }
        else if (currentScene.name == "TestJaspe")
        {
            playerPlatforms = new PlayerPlatforms(2, 6, 1);
            gameState.gridManager.width = 20;
            gameState.gridManager.heigth = 12;
            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];
            // gameState.gridManager.Build_Grid1_Without_Visuals();
            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
            gameState.playerManager.player.SetSpawnpoint(1);
            gameState.platformManager.Build_Level2();
            SetCoinPositions(1);
            SetCoinPositions(2);
            SetCoinPositions(3);
            SetStickerPositions(4);
            SetfinishPositions(5);

            gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
        }

        else if (currentScene.name == "VerticalSliceLevel2")
        {
            gameState.gridManager.width = 20;
            gameState.gridManager.heigth = 11;

            playerPlatforms = new PlayerPlatforms(2, 3, 1);

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();

            gameState.platformManager.Build_Vertical_Slice_Level6();
        }
        else if (currentScene.name == "VerticalSliceLevel1")
        {
            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
            gameState.gridManager.width = 20;
            gameState.gridManager.heigth = 11;

            playerPlatforms = new PlayerPlatforms(2, 12, 1);

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;

            gameState.platformManager.Build_Vertical_Slice_Level6();
            SetCoinPositions(46);
            SetCoinPositions(70);
            SetCoinPositions(97);
            SetStickerPositions(1);
            SetfinishPositions(139);

            gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
        }
        else if (currentScene.name == "VerticalSliceLevel3")
        {
            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
            gameState.gridManager.width = 27;
            gameState.gridManager.heigth = 17;

            playerPlatforms = new PlayerPlatforms(2, 3, 1);

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[0] + playeradjustment;
            gameState.platformManager.Build_Vertical_Slice_Level3();
        }
        else if (currentScene.name == "VerticalSliceLevel4")
        {
            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
            gameState.gridManager.width = 27;
            gameState.gridManager.heigth = 17;

            playerPlatforms = new PlayerPlatforms(2, 3, 1);
            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[0] + playeradjustment;
        }
    }
}
