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
    public Canvas canvas;
    BallKnop balknop;

    public PlayerPlatforms playerPlatforms;

    private void Start()
    {
        balknop = gameState.UIManager.canvas.GetComponentInChildren<BallKnop>();
    }
    public Boolean levelIsSpawned = false;
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
            item.gameObject.SetActive(false) ;
       }
       coinList.Clear();
       stickerObject.gameObject.SetActive(false);
       finish.gameObject.SetActive(false);
       
       gameState.collectableManager.InitCollectables(coinPositions, stickerObject.spawnpoint, finish.spawnpoint);
    }
    public void SetCoinPositions(int i)
    {
        Vector3 coinAdjustment = new Vector3(.3f, 0, 0);
        coinPositions.Add(gameState.gridManager.gridSquares[i] + coinAdjustment);
    }
    public void SetStickerPositions(int i)
    {
        Vector3 stickerAdjustment = new Vector3(.5f, 0, 0);
        stickerPosition = gameState.gridManager.gridSquares[i] + stickerAdjustment;
    }
    public void SetfinishPositions(int i)
    {
        Vector3 finishAdjustment = new Vector3(.5f, 0, 0);
        finishPosition = gameState.gridManager.gridSquares[i] + finishAdjustment;
    }
    public void InitScene(string sceneName)
    {
        currentScene = SceneManager.GetActiveScene();
        

        //gameState.platformManager.spawnLevel1();
        //gameState.platformManager.Init_Platforms();

        

        if (sceneName == "LevelEditor")
        {
            gameState.gridManager.width = 11;
            gameState.gridManager.heigth = 12;

            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0, 0);

            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
        }
        
        
        else if (sceneName == "1")
        {
            if (!levelIsSpawned)
            {
                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                //GameState.Instance.playerCamera.ManualInit();
                gameState.collectableManager.newCollectablesAreRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.heigth = 11;
                playerPlatforms = new PlayerPlatforms(6, 6, 0, 1, 1);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(1);
                //gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
                gameState.platformManager.Build_Vertical_Slice_Level6();
                SetCoinPositions(67);
                SetCoinPositions(90);
                SetCoinPositions(115);
                SetStickerPositions(177);
                SetfinishPositions(178);



                //boolean party voor elk level nu nodig

                gameState.playerManager.PlayerInit();
                gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 1;
                PlayerDataController.instance.previousScene = 1;
                levelIsSpawned = true;
            }
        }
        
        else if (sceneName == "2")
        {
            if (!levelIsSpawned)
            {
                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.heigth = 14;
                //Array.Clear(gameState.UIManager.instantiatedInventoryButtons, 0, gameState.UIManager.instantiatedInventoryButtons.Length);
                //playerPlatforms = null;
                playerPlatforms = new PlayerPlatforms(6, 2, 1, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();

                gameState.playerBallManager.SetSpawnpoint(1);
                gameState.platformManager.BuildLevelCoen();
                SetCoinPositions(110);
                SetCoinPositions(138);
                SetCoinPositions(196);
                SetStickerPositions(145);
                SetfinishPositions(254);

                gameState.playerManager.PlayerInit();
                
                gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
                //hier dan een vieze boolean

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 2;
                PlayerDataController.instance.previousScene = 2;
                levelIsSpawned = true;
            }
        }
        else if (sceneName == "3")
        {
            if (!levelIsSpawned)
            {
                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                //gameState.playerCamera = Instantiate(gameState.playerCamera);
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.heigth = 11;
                playerPlatforms = new PlayerPlatforms(8, 8, 1, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(1);
                gameState.platformManager.Build_Vertical_Slice_LevelBoost();
                SetCoinPositions(104);
                SetCoinPositions(90);
                SetCoinPositions(190);
                SetStickerPositions(75);
                SetfinishPositions(188);

                gameState.playerManager.PlayerInit();
                gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 3;
                PlayerDataController.instance.previousScene = 3;
                levelIsSpawned = true;
            }
        }
        else if (sceneName == "4")
        {
            if (!levelIsSpawned)
            {
                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                //gameState.playerCamera = Instantiate(gameState.playerCamera);
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.heigth = 11;
                playerPlatforms = new PlayerPlatforms(4, 4, 1, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(60);
                //gameState.platformManager.Build_Vertical_Slice_Level6();
                gameState.platformManager.Build_Vertical_Slice_Level7();
                SetCoinPositions(137);
                SetCoinPositions(123);
                SetCoinPositions(187);
                SetStickerPositions(12);
                SetfinishPositions(88);

                gameState.playerManager.PlayerInit();
                gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 4;
                PlayerDataController.instance.previousScene = 4;
            }
        }
    }
    public void SetRollingPhase()
    {
        gameState.BuildingPhaseActive = false;
        gameState.RollingPhaseActive = true;
        balknop.gameObject.SetActive(false);  
        gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = false;
    }
    public void SetBuildingPhase()
    {
        if (currentScene.name != "VictoryScreen")
        {
            gameState.RollingPhaseActive = false;
            gameState.playerBallManager.respawnBal();
            gameState.levelManager.RespawnCollectables();
            balknop.gameObject.SetActive(true);
            gameState.BuildingPhaseActive = true;
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = true;
        }
        else
        {
            gameState.RollingPhaseActive = false;
            gameState.BuildingPhaseActive = true;
        }

    }
    //SceneLoading and generals
    //https://www.alanzucconi.com/2016/03/30/loading-bar-in-unity/
    //
    internal void AsynchronousLoadStart(string scene)
    {
        //coinList.Clear();
        IEnumerator coroutine;
        //niet vergeten die unsubscribe te doen enzo
        SceneManager.sceneLoaded += SceneIsLoaded;
        coroutine = AsynchronousLoad(scene);
        StartCoroutine(coroutine);
        
    }

    private void SceneIsLoaded(Scene arg0, LoadSceneMode arg1)
    {
        InitScene(arg0.name);
    }

    IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
        {
            // [0, 0.9] > [0, 1]
            float progress = Mathf.Clamp01(ao.progress / 0.9f);
            Debug.Log("Loading progress: " + (progress * 100) + "%");

            // Loading completed
            if (ao.progress == 0.9f)
            {
                ao.allowSceneActivation = true;
               
            }
            yield return null;
        }
    }
}
//scheit voor scheit mensen
//if (sceneName == "lol1")
//        {
//            gameState.gridManager.width = 11;
//            gameState.gridManager.heigth = 12;
//            gameState.gridManager.Build_Grid1_Without_Visuals();
//            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
//gameState.playerManager.player.SetSpawnpoint(1);
//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);
//SetCoinPositions(1);
//SetCoinPositions(2);
//SetCoinPositions(3);
//SetStickerPositions(4);
//SetfinishPositions(5);

//gameState.collectableManager.InitCollectables(coinPositions, stickerPosition, finishPosition);
//            gameState.levelManager.SetBuildingPhase();
//        }

//        else if (sceneName == "TestLevel1")
//        {

//            //lees level uit Json en vul levelPlatformen
//            //ReadLevelsFromText("Level1.json");

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);

////Dit moet ergens anders
//gameState.gridManager.width = 11;
//            gameState.gridManager.heigth = 12;

//            //deze code is superbelangrijk voor het opslaan van levels
//            //levelPlatformen.width = 11;
//            //levelPlatformen.heigth = 12;
//            //levelPlatformen.tileList = new int[11*12];

//            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.heigth];
//            gameState.gridManager.Build_Grid1_Without_Visuals();

//            gameState.platformManager.BuildLevelFromText(levelPlatformen);
//            //gameState.platformManager.Build_Level1(levelPlatformen);



//            //SaveLevelToText("Level1.json");
//        }
//    else if (sceneName == "VerticalSliceLevel2")
//        {
//            gameState.gridManager.width = 20;
//            gameState.gridManager.heigth = 11;

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);

//gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();

//            gameState.platformManager.Build_Vertical_Slice_Level6();
//        }else if (sceneName == "VerticalSliceLevel3")
//        {
//            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
//gameState.gridManager.width = 27;
//            gameState.gridManager.heigth = 17;

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);

//gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
//            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[0] + playeradjustment;
//            gameState.platformManager.Build_Vertical_Slice_Level3();
//        }
//        else if (sceneName == "VerticalSliceLevel4")
//        {
//            Vector3 playeradjustment = new Vector3(.5f, 0, 0);
//gameState.gridManager.width = 27;
//            gameState.gridManager.heigth = 17;

//            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0);
//gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
//            gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[0] + playeradjustment;
//        }
