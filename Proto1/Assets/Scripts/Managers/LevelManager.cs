using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    LevelPlatformen levelPlatformen = new LevelPlatformen();


    //ik wil levels uit een textbestand kunnen opslaan en uitlezen ga ik proberen hier

    private void ReadLevelsFromText(string levelName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, levelName);

        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            levelPlatformen = JsonUtility.FromJson<LevelPlatformen>(dataAsJSON);



            //Debug.Log("Data is geladen.");
        }
        else
        {
            //moet file createn
            Debug.LogError("Cannot find file!");
        }
    }

    private void SaveLevelToText(string levelName)
    {
        string filePath = Path.Combine(Application.streamingAssetsPath, levelName);

        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(levelPlatformen);
            File.WriteAllText(filePath, dataAsJson);
        }
    }

    private void Awake()
    {
        gameState = GameState.Instance;
        //gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }

    public void InitScene()
    {
        currentScene = SceneManager.GetActiveScene();

        //gameState.platformManager.spawnLevel1();
        //gameState.platformManager.Init_Platforms();

        if (currentScene.name == "TestLevelCoen")
        {
            List<Vector3> coinPositions = new List<Vector3>();
            coinPositions.Add(new Vector3(0, 1.5f, 0));
            coinPositions.Add(new Vector3(3, 2, 0));
            coinPositions.Add(new Vector3(6, 2, 0));

            Vector3 stickerPosition = new Vector3(0, -2, 0);

            gameState.collectableManager.InitCollectables(coinPositions, stickerPosition);
        }

        else if (currentScene.name == "TestLevel1")
        {

            //lees level uit Json en vul levelPlatformen
            ReadLevelsFromText("Level1.json");

            playerPlatforms = new PlayerPlatforms(2, 3);


            //Dit moet ergens anders
            gameState.gridManager.width = 11;
            gameState.gridManager.heigth = 12;

            //deze code is superbelangrijk voor het opslaan van levels
            //levelPlatformen.width = 11;
            //levelPlatformen.heigth = 12;
            //levelPlatformen.tileList = new int[11*12];


            gameState.gridManager.Build_Grid1_Without_Visuals();

            gameState.platformManager.BuildLevelFromText(levelPlatformen);
            //gameState.platformManager.Build_Level1(levelPlatformen);



            //SaveLevelToText("Level1.json");
        }

        else if (currentScene.name == "LevelEditor")
        {
            gameState.gridManager.width = 50;
            gameState.gridManager.heigth = 25;




            gameState.gridManager.Build_Grid1_Without_Visuals();
        }
        else if (currentScene.name == "TestJaspe")
        {
            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
            gameState.gridManager.width = 11;
            gameState.gridManager.heigth = 12;
            gameState.gridManager.Build_Grid1_Without_Visuals();
            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
            gameState.platformManager.Build_Level2();
        }
    }
}
