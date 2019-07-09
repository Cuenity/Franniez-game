using Photon.Pun;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    //testing
    public LevelSettings levelSettings;

    [SerializeField]
    LevelSettings introLevel,
        level2RampEasy,
        level3JumpsEasy,
        Level4,
        Level5,
        Level6BlackHoleBallAndJump,
        Level7BoosterEasy,
        Level8BoosterHard,
        Level9PortalEasy,
        Level10TrampolineAdvanced,
        Level11,
        LevelSettingLevel12LiftMoeilijk,
        Level13,
        Level14,
        Level15,
        Level16,
        Level18,
        Level21,
        Level22,
        Level26,
        Level27,
        Level28,
        Level29,
        Level30,
        smiletTest,
        house,
        multiLevel1,
        multiLevel2,
        multiLevel3,
        multiLevel4,
        multiLevel5,
        multiLevel6,
        multiLevel7,
        multiLevel8,
        multiLevel9,
        multiLevel10,
        boom,
        eend,
        kroon,
        portalrooms;


    #region levelmanagerMethods
    GameState gameState;
    Scene currentScene;
    Vector3 stickerPosition;
    Vector3 finishPosition;
    public List<Coin> coinList = new List<Coin>();
    public StickerObject stickerObject;
    public Finish finish;
    public BallKnop balknop;
    public int currentLevel;

    public PlayerPlatforms playerPlatforms;

    public bool bigLevel;

    //multiplayer booleans
    public bool multiUIRequired;
    public bool p1Finish;
    public bool p2Finish;

    private void Start()
    {
        balknop = gameState.UIManager.canvas.GetComponentInChildren<BallKnop>();
    }
    public Boolean levelIsSpawned = false;

    public LevelPlatformen levelPlatformen = new LevelPlatformen();

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
        GameState.Instance.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);
        //moet later terug
        //GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen);
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
    }

    private void DefaultSceneInit()
    {
        gameState.UIManager.canvas = Instantiate(gameState.UIManager.UICanvas);
        gameState.UIManager.newLevelInventoryisRequired = true;
        gameState.collectableManager.newCollectablesAreRequired = true;
        gameState.playerCamera.ManualInit();
    }

    private void DefaultSceneEndInit()
    {
        if (!PhotonNetwork.IsConnected)
        {
            gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
        }
        gameState.BuildingPhaseActive = true;
        levelIsSpawned = true;
    }
    #endregion

    public void SpawnLevel(LevelSettings levelSetting)
    {
        DefaultSceneInit();

        levelSetting.Init();
        bigLevel = levelSetting.bigLevel;
        //dit verhaal moet in levesettings zelf gebeuren
        //playerplatforms
        playerPlatforms = new PlayerPlatforms(levelSetting.playerPlatformsArray[1], levelSetting.playerPlatformsArray[0], levelSetting.playerPlatformsArray[2], levelSetting.playerPlatformsArray[3], levelSetting.playerPlatformsArray[4]);
        //spawngrid
        gameState.gridManager.Build_Grid_FromJSON_Without_Visuals(gameState.gridManager.width, gameState.gridManager.height);
        //spawnlevel
        gameState.platformManager.BuildLevelFromLevelPlatformen(levelSetting.levelPlatformen);
        //dit is blijkbaar superbelangrijk
        //spawnpoint
        gameState.playerBallManager.SetSpawnpoint(levelSetting.spawnPoint);
        //balzooi
        gameState.playerBallManager.WhatBalls(levelSetting.ballArray[0], levelSetting.ballArray[1], levelSetting.ballArray[2]);

        DefaultSceneEndInit();
    }


    public void InitScene()
    {
        GAManager.StartGame();
        //this.sceneName = sceneName;
        currentLevel = PlayerDataController.instance.PreviousScene;
        Debug.Log(currentLevel);
        string scenestring = SceneManager.GetActiveScene().name;
        //if(scenestring == "1")
        //{
        //    SpawnLevel(levelSettings);
        //}
        if (scenestring == "LevelEditor")
        {
            gameState.gridManager.width = 11;
            gameState.gridManager.height = 12;

            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0, 0);

            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.height];

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
        }

        // af
        else if (currentLevel == 1)
        {
            if (!levelIsSpawned)
            {
                gameState.tutorialManager.StartTutorial();
                SpawnLevel(introLevel);
            }
        }

        // af
        else if (currentLevel == 2)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(level2RampEasy);
            }
        }

        // af
        else if (currentLevel == 3)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(level3JumpsEasy);
            }
        }

        // af
        else if (currentLevel == 4)
        {
            {
                SpawnLevel(Level4);
            }
        }

        // af
        else if (currentLevel == 5)
        {
            if (!levelIsSpawned)
            {
                gameState.tutorialManager.StartTutorial();
                gameState.tutorialManager.changeBallTutorial = true;
                SpawnLevel(Level5);
            }
        }

        // af
        else if (currentLevel == 6)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level6BlackHoleBallAndJump);
            }
        }

        // af
        else if (currentLevel == 7)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level7BoosterEasy);
            }
        }

        // af
        else if (currentLevel == 8)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level8BoosterHard);
            }
        }

        // af?
        else if (currentLevel == 9)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level9PortalEasy);
            }
        }

        // af? portal & trampoline
        else if (currentLevel == 10)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level10TrampolineAdvanced);
            }
        }

        // af, lift easy
        else if (currentLevel == 11)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level11);
                gameState.platformManager.InitLift(42, 84);
            }
        }

        // af, lift hard
        else if (currentLevel == 12)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(LevelSettingLevel12LiftMoeilijk);
                gameState.platformManager.InitLift(263, 43);
            }
        }

        // af, cannon level easy
        else if (currentLevel == 13)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level13);
            }
        }

        // af, cannon hard
        else if (currentLevel == 14)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level14);
            }
        }

        // af, cannon pinball
        else if (currentLevel == 15)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level15);
            }
        }

        // af
        else if (currentLevel == 16)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level16);
            }
        }

        // af
        else if (currentLevel == 17)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(portalrooms);
            }
        }

        // af
        else if (currentLevel == 18)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level18);
            }
        }

        // af
        else if (currentLevel == 19)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(house);
            }
        }

        // af
        else if (currentLevel == 20)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(boom);
            }
        }

        else if (currentLevel == 21)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level21);
            }
        }

        else if (currentLevel == 22)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level22);
            }
        }

        // af
        else if (currentLevel == 23)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(kroon);
            }
        }

        // af
        else if (currentLevel == 24)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(eend);
            }
        }

        // af
        else if (currentLevel == 25)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(smiletTest);
            }
        }

        else if (currentLevel == 26)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level26);
            }
        }

        else if (currentLevel == 27)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level27);
            }
        }

        else if (currentLevel == 28)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level28);
            }
        }

        else if (currentLevel == 29)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level29);
            }
        }

        else if (currentLevel == 30)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(Level30);
            }
        }

        else if (PlayerDataController.instance.PreviousScene == 101)
        {
            if (!levelIsSpawned)
            {

                SpawnLevel(multiLevel1);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 9, 0, 9, 10, 19, 0, 9);
                gameState.playerManager.MultiPlayerBallInit(42, 57);
                gameState.UIManager.AddMultiplayerUI();

            }
        }

        else if (PlayerDataController.instance.PreviousScene == 102)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel2);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 135, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 136);
                gameState.UIManager.AddMultiplayerUI();


            }
        }
        else if (PlayerDataController.instance.PreviousScene == 103)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel3);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 6, 0, 6, 8, 14, 0, 6);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();


            }
        }
        else if (PlayerDataController.instance.PreviousScene == 104)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel4);
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(0, 1);
                gameState.UIManager.AddMultiplayerUI();


            }
        }
        else if (PlayerDataController.instance.PreviousScene == 105)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel5);
                //gameState.gridManager.InitPlayerGridsMultiPlayer(0, 24, 0, 4, 125, 149, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();


            }
        }
        else if (PlayerDataController.instance.PreviousScene == 106)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel6);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 6, 0, 10, 8, 14, 0, 10);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (PlayerDataController.instance.PreviousScene == 107)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel7);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 9, 0, 14, 16, 24, 0, 14);
                gameState.playerManager.MultiPlayerBallInit(1, 23);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (PlayerDataController.instance.PreviousScene == 108)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel8);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 9, 0, 14, 11, 20, 0, 14);
                gameState.playerManager.MultiPlayerBallInit(1, 19);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (PlayerDataController.instance.PreviousScene == 109)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel9);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 14, 0, 4, 75, 89, 0, 4);
                gameState.playerManager.MultiPlayerBallInit(1, 13);
                gameState.UIManager.AddMultiplayerUI();

            }
        }
        else if (PlayerDataController.instance.PreviousScene == 110)
        {
            if (!levelIsSpawned)
            {
                SpawnLevel(multiLevel10);
                gameState.gridManager.InitPlayerGridsMultiPlayer(0, 9, 0, 19, 11, 20, 0, 19);
                gameState.playerManager.MultiPlayerBallInit(1, 19);
                gameState.UIManager.AddMultiplayerUI();
            }
        }
    }



    public void SetRollingPhase()
    {
        gameState.BuildingPhaseActive = false;
        gameState.RollingPhaseActive = true;
        gameState.uIRollingManager.ChangeEnviroment();

        if (currentLevel != 1 || !PhotonNetwork.InRoom)
        {
            //zet de ballknop uit in MP en level 1s
            try
            {
                balknop.gameObject.SetActive(false);
            }
            catch
            {

            }
        }
        if (PhotonNetwork.IsConnected)
        {
            gameState.playerBallManager.MultiActivePlayer1.GetComponent<Rigidbody>().isKinematic = false;
            gameState.playerBallManager.MultiActivePlayer1.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public void SetBuildingPhase()
    {
        if (PlayerDataController.instance.PreviousScene == 1)
        {
            gameState.tutorialManager.RollingFinished = true;
        }

        if (PhotonNetwork.InRoom)
        {
            //zet de flaggen op niet geraakt zodat de speler opnieuw de vlag moet raken
            PhotonView view = gameState.playerBallManager.activePlayer.GetComponent<PhotonView>();
            if (PhotonNetwork.IsMasterClient)
            {
                view.RPC("FlagUnHit", RpcTarget.All, "masterhit");
            }
            else
            {
                view.RPC("FlagUnHit", RpcTarget.All, "clienthit");
            }
        }
        if (SceneManager.GetActiveScene().name != "VictoryScreen")
        {
            gameState.RollingPhaseActive = false;
            gameState.playerBallManager.respawnBal();
            gameState.platformManager.RespawnCollectables();

            if (SceneManager.GetActiveScene().name != "1" || !PhotonNetwork.InRoom)
            {
                try
                {
                    balknop.gameObject.SetActive(true);
                }
                catch
                {

                }
            }
            gameState.BuildingPhaseActive = true;
            gameState.uIRollingManager.ChangeEnviroment();
            if (gameState.platformManager.liftLocal)
            {
                gameState.platformManager.liftLocal.ResetPlatform();
            }
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = true;

        }
        else
        {
            gameState.RollingPhaseActive = false;
            gameState.BuildingPhaseActive = true;
        }

    }
}