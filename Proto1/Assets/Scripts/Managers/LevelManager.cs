using GameAnalyticsSDK;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class LevelManager : MonoBehaviour
{
    #region levelArrays
    /// <summary > Arrays voor het bouwen van levels
    int[] level5 = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
    int[] levelTeGroot = new int[] { 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 2, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 5, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 4, 0, 7, 7, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 3, 0, 7, 7, 7, 7, 1, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 7, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 7, 0, 7, 0, 7, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 3, 0, 3, 0, 4, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 7, 7, 7, 0, 0, 0, 2, 0, 3, 0, 7, 0, 0, 0, 7, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 2, 0, 3, 0, 3, 0, 0, 7, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 2, 0, 3, 0, 3, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 5, 0, 3, 0, 3, 0, 3, 0, 3, 0 };
    int[] levelTeGrootMaarDanKlein = new int[] { 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0 };
    int[] levelDom = new int[] { 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 7, 7, 7, 7, 7, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0 };
    int[] levelMulti = new int[] { 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 0, 7, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 7, 0 };
    int[] level2RampsEasy = new int[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 3, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 2, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 3, 0, 3, 0, 0, 0, 0, 0 };
    int[] level3JumpEasy = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
       };
    int[] level4JumpHard = new int[] {
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 4, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
        0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0 };
    /// </summary>
    #endregion

    #region levelmanagerMethods
    GameState gameState;
    Scene currentScene;
    //List<Vector3> coinPositions = new List<Vector3>();
    Vector3 stickerPosition;
    Vector3 finishPosition;
    public List<Coin> coinList = new List<Coin>();
    public StickerObject stickerObject;
    public Finish finish;
    public Canvas canvas;
    public BallKnop balknop;
    private string sceneName;

    public PlayerPlatforms playerPlatforms;

    public bool bigLevel;

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


        //gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }
    #endregion

    public void InitScene(string sceneName)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, sceneName.ToString());
        this.sceneName = sceneName;
        currentScene = SceneManager.GetActiveScene();

        if (sceneName == "LevelEditor")
        {
            gameState.gridManager.width = 11;
            gameState.gridManager.height = 12;

            playerPlatforms = new PlayerPlatforms(2, 3, 1, 0, 0);

            levelPlatformen.tileList = new int[gameState.gridManager.width * gameState.gridManager.height];

            gameState.gridManager.Build_Grid_BuildingPhase_With_Visuals();
        }

        // af
        else if (sceneName == "1")
        {
            if (!levelIsSpawned)
            {
                bigLevel = false;
                gameState.tutorialManager.StartTutorial();

                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                gameState.collectableManager.newCollectablesAreRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                //Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 8;
                gameState.gridManager.height = 5;
                playerPlatforms = new PlayerPlatforms(0, 2, 0, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(0);
                //gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
                gameState.platformManager.BuildTutorial();


                //boolean party voor elk level nu nodig
                gameState.playerBallManager.WhatBalls(true, false, false);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
                //gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 1;
                PlayerDataController.instance.previousScene = 1;
                levelIsSpawned = true;
            }
        }

        // af
        else if (sceneName == "2")
        {
            if (!levelIsSpawned)
            {
                bigLevel = false;
                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                gameState.collectableManager.newCollectablesAreRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                levelPlatformen.tileList = level2RampsEasy;
                levelPlatformen.width = 10;
                levelPlatformen.heigth = 8;
                gameState.gridManager.width = 10;
                gameState.gridManager.height = 8;
                playerPlatforms = new PlayerPlatforms(2, 1, 0, 0, 0);
                gameState.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);
                gameState.playerBallManager.SetSpawnpoint(0);
                //gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
                //gameState.platformManager.BuildLevel2RampsEasy();

                int[] coinarray = new int[] { 24, 28, 47 };
                int finishPosition = 62;
                GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen, coinarray, finishPosition);

                //boolean party voor elk level nu nodig
                gameState.playerBallManager.WhatBalls(true, false, false);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
                //gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 1;
                PlayerDataController.instance.previousScene = 1;
                levelIsSpawned = true;
            }
        }

        // af
        else if (sceneName == "3")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;
                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                gameState.collectableManager.newCollectablesAreRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                levelPlatformen.tileList = level3JumpEasy;
                levelPlatformen.width = 15;
                levelPlatformen.heigth = 10;
                gameState.gridManager.width = 15;
                gameState.gridManager.height = 10;
                playerPlatforms = new PlayerPlatforms(1, 1, 0, 0, 0);
                gameState.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);
                gameState.playerBallManager.SetSpawnpoint(0);
                //gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
                //gameState.platformManager.BuildLevel2RampsEasy();

                int[] coinarray = new int[] { 40, 63, 95 };
                int finishPosition = 88;
                GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen, coinarray, finishPosition);

                //boolean party voor elk level nu nodig
                gameState.playerBallManager.WhatBalls(true, false, false);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
                //gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 1;
                PlayerDataController.instance.previousScene = 1;
                levelIsSpawned = true;
            }
        }

        // af
        else if (sceneName == "4")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;
                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                gameState.collectableManager.newCollectablesAreRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                levelPlatformen.tileList = level4JumpHard;
                levelPlatformen.width = 40;
                levelPlatformen.heigth = 15;
                gameState.gridManager.width = 40;
                gameState.gridManager.height = 15;
                playerPlatforms = new PlayerPlatforms(0, 0, 5, 0, 0);
                gameState.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);
                gameState.playerBallManager.SetSpawnpoint(0);
                //gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
                //gameState.platformManager.BuildLevel2RampsEasy();

                int[] coinarray = new int[] { 456, 370, 155 };
                int finishPosition = 559;
                GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen, coinarray, finishPosition);

                //boolean party voor elk level nu nodig
                gameState.playerBallManager.WhatBalls(true, false, false);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
                //gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 1;
                PlayerDataController.instance.previousScene = 1;
                levelIsSpawned = true;
            }
        }

        else if (sceneName == "7")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                //gameState.playerCamera = Instantiate(gameState.playerCamera);
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 11;
                playerPlatforms = new PlayerPlatforms(8, 8, 1, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(1);
                gameState.platformManager.Build_Vertical_Slice_LevelBoost();

                gameState.playerBallManager.WhatBalls(true, true, false);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 3;
                PlayerDataController.instance.previousScene = 3;
                levelIsSpawned = true;
            }
        }

        // moet met minder platformen en makkelijker haalbaar
        else if (sceneName == "8")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                levelPlatformen.tileList = levelTeGrootMaarDanKlein;
                levelPlatformen.width = 20;
                levelPlatformen.heigth = 10;
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 10;
                playerPlatforms = new PlayerPlatforms(5, 5, 5, 4, 0);
                GameState.Instance.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);

                gameState.playerBallManager.SetSpawnpoint(41);
                int[] coinarray = new int[] { 71, 103, 136 };
                int finishPosition = 171;
                GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen, coinarray, finishPosition);

                gameState.playerBallManager.WhatBalls(false, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 5;
                PlayerDataController.instance.previousScene = 5;
            }
        }

        else if (sceneName == "11")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 14;
                //Array.Clear(gameState.UIManager.instantiatedInventoryButtons, 0, gameState.UIManager.instantiatedInventoryButtons.Length);
                //playerPlatforms = null;
                playerPlatforms = new PlayerPlatforms(6, 2, 1, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();

                gameState.playerBallManager.SetSpawnpoint(1);
                gameState.platformManager.BuildLevelCoen();


                gameState.playerBallManager.WhatBalls(true, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                //hier dan een vieze boolean

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 2;
                PlayerDataController.instance.previousScene = 2;
                levelIsSpawned = true;
            }
        }

        else if (sceneName == "12")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

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
                gameState.gridManager.height = 11;
                playerPlatforms = new PlayerPlatforms(6, 6, 0, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(1);
                //gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[1] + playeradjustment;
                gameState.platformManager.Build_Vertical_Slice_Level6();


                //boolean party voor elk level nu nodig
                gameState.playerBallManager.WhatBalls(true, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);
                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 1;
                PlayerDataController.instance.previousScene = 1;
                levelIsSpawned = true;
            }
        }

        else if (sceneName == "15")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                //gameState.playerCamera = Instantiate(gameState.playerCamera);
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 40;
                gameState.gridManager.height = 12;
                playerPlatforms = new PlayerPlatforms(3, 4, 0, 0, 2);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(41);
                gameState.platformManager.BuildLevel6();


                gameState.playerBallManager.WhatBalls(true, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 4;
                PlayerDataController.instance.previousScene = 4;
            }
        }



        else if (sceneName == "17")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                //gameState.collectableManager.newCollectablesAreRequired = true;
                //coinPositions.Clear();
                //coinList.Clear();
                GameState.Instance.playerCamera.ManualInit();
                //gameState.playerCamera = Instantiate(gameState.playerCamera);
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 11;
                playerPlatforms = new PlayerPlatforms(4, 4, 1, 0, 0);
                gameState.gridManager.Build_Grid_BuildingPhase_Without_Visuals();
                gameState.playerBallManager.SetSpawnpoint(60);
                //gameState.platformManager.Build_Vertical_Slice_Level6();
                gameState.platformManager.Build_Vertical_Slice_Level7();


                gameState.playerBallManager.WhatBalls(true, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 4;
                PlayerDataController.instance.previousScene = 4;
            }
        }

        else if (sceneName == "18")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                gameState.UIManager.canvas = Instantiate(canvas);
                gameState.UIManager.newLevelInventoryisRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                levelPlatformen.tileList = levelDom;
                levelPlatformen.width = 20;
                levelPlatformen.heigth = 10;
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 10;
                playerPlatforms = new PlayerPlatforms(7, 7, 4, 3, 0);
                GameState.Instance.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);

                gameState.playerBallManager.SetSpawnpoint(41);
                int[] coinarray = new int[] { 46, 127, 133 };
                int finishPosition = 39;
                GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen, coinarray, finishPosition);

                gameState.playerBallManager.WhatBalls(true, true, true);
                gameState.playerManager.PlayerInit(gameState.playerBallManager.ballList[0]);

                gameState.BuildingPhaseActive = true;
                GameState.Instance.PreviousLevel = 6;
                PlayerDataController.instance.previousScene = 6;
            }
        }
        else if (sceneName == "MultiplayerLevel1")
        {
            if (!levelIsSpawned)
            {
                bigLevel = true;

                gameState.UIManager.canvas = Instantiate(canvas);
                //fuck die button shit see if i give a shit fuckboi
                //Button[] canvasButtons = gameState.UIManager.canvas.GetComponentsInChildren<Button>();
                //for (int i = 0; i < canvasButtons.Length; i++)
                //{
                //    if (canvasButtons[i].name == "BallKnop")
                //    {
                //        canvasButtons[i].gameObject.SetActive(true);
                //    }
                //}
                gameState.UIManager.newLevelInventoryisRequired = true;
                GameState.Instance.playerCamera.ManualInit();
                Vector3 playeradjustment = new Vector3(.5f, 0, 0);
                levelPlatformen.tileList = levelMulti;
                levelPlatformen.width = 20;
                levelPlatformen.heigth = 20;
                gameState.gridManager.width = 20;
                gameState.gridManager.height = 20;
                playerPlatforms = new PlayerPlatforms(7, 7, 4, 3, 0);
                GameState.Instance.gridManager.Build_Grid_FromJSON_Without_Visuals(levelPlatformen.width, levelPlatformen.heigth);


                int[] coinarray = new int[] { 131, 132, 133 };
                int finishPosition = 250;
                GameState.Instance.platformManager.BuildLevelFromLevelPlatformen(levelPlatformen, coinarray, finishPosition);
                gameState.playerManager.MultiPlayerBallInit(45, 55);

                gameState.BuildingPhaseActive = true;
                //dit is wel poep moet echt es anders
                GameState.Instance.PreviousLevel = 6;
                PlayerDataController.instance.previousScene = 6;
            }
        }
    }
    public void SetRollingPhase()
    {
        gameState.BuildingPhaseActive = false;
        gameState.RollingPhaseActive = true;

        //if (sceneName != "1" || !PhotonNetwork.IsConnected)
        //{
        //    balknop.gameObject.SetActive(false);
        //}
        if (sceneName != "1" || !PhotonNetwork.InRoom)
        {
            //idk man fucking error bullshit magic spell try catch
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
            //deze oplossing is zo smerig wil niet eens een comment schrijven
            //de bal van de andere speler wordt altijd als een prefabnaam(Clone) gespawnd en zo kan ik ervoor zorgen dat ie op de client die dit uitvoert niet verplaatst wordt
            //ENIGE manier die tot nu toe werkt
            if (PhotonNetwork.IsMasterClient)
            {
                GameObject.Find("player1ball").GetComponent<Rigidbody>().isKinematic = false;
                GameObject.Find("Photon Player Ball(Clone)").GetComponent<Rigidbody>().isKinematic = true;

            }
            else
            {
                GameObject.Find("player2ball").GetComponent<Rigidbody>().isKinematic = false;
                GameObject.Find("Photon Player Ball(Clone)").GetComponent<Rigidbody>().isKinematic = true;
            }
            //ik wil hetlater toch echt zo doen dit hierboven zo WACK
            //if (gameState.playerBallManager.activePlayer.GetPhotonView().IsMine)
            //{
            //    GameObject mijnBal = gameState.playerBallManager.activePlayer;
            //    mijnBal.GetComponent<Rigidbody>().isKinematic = false;
            //}
            //else if (gameState.playerBallManager.activePlayer.GetPhotonView().IsMine)
            //{
            //    GameObject andereBal = gameState.playerBallManager.activePlayer;
            //    andereBal.GetComponent<Rigidbody>().isKinematic = true;
            //}
        }
        else
        {
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = false;
        }
    }
    public void SetBuildingPhase()
    {
        if (sceneName == "1")
        {
            gameState.tutorialManager.RollingFinished = true;
        }

        if (PhotonNetwork.InRoom)
        {
            //zet vlag geraakt op false voor multi
            bool hitflag = false;
            Hashtable hash = new Hashtable();
            hash.Add("hitflag", hitflag);
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);
            Debug.Log(PhotonNetwork.LocalPlayer.CustomProperties["hitflag"]);
        }
        if (sceneName != "VictoryScreen")
        {
            gameState.RollingPhaseActive = false;
            gameState.playerBallManager.respawnBal();
            gameState.platformManager.RespawnCollectables();

            //if (sceneName != "1"||!PhotonNetwork.IsConnected)
            //{
            //    balknop.gameObject.SetActive(true);
            //}
            if (sceneName != "1" || !PhotonNetwork.InRoom)
            {
                //idk man fucking error bullshit magic spell try catch
                try
                {
                    balknop.gameObject.SetActive(true);
                }
                catch
                {

                }
            }
            gameState.platformManager.lift.ResetPlatform();
            gameState.BuildingPhaseActive = true;
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = true;

        }
        else
        {
            gameState.RollingPhaseActive = false;
            gameState.BuildingPhaseActive = true;
        }

    }




    ///////////////////////////////////////////
    //////dit hieronder kan allemaal weg??///// idk, you tell me Denk t wel zal het uitcommenten voor de zekerheid
    ///////////////////////////////////////////


    ////SceneLoading and generals
    ////https://www.alanzucconi.com/2016/03/30/loading-bar-in-unity/
    ////
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
//}
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
