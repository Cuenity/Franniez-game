﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using UnityEngine.UI;
using System.IO;

public class PlatformManager : MonoBehaviour
{

    public GameObject platform;
    public GameObject rechthoek;
    public Ramp ramp;
    public GameObject PlatformSquare;
    public GameObject Balletje;
    public Portal portal;
    public List<Portal> allPortals = new List<Portal>();
    public Trampoline trampoline;
    public BigRamp bigRamp;
    public Finish finish;

    GameState gameState;


    public GameObject lijn;
    public GameObject gridDot;
    public GameObject gridSquare;
    public List<Vector3[]> gridPunten = new List<Vector3[]>();


    Scene currentScene;

    List<Vector3> platformPositions;

    private Ramp snapRamp;
    private GameObject snapPlatform;
    private GameObject snapPlatformSquare;


    // Start is called before the first frame update
    private void Awake()
    {
        platformPositions = new List<Vector3>();

        gameState = GameState.Instance;
    }
    void Start()
    {

        currentScene = SceneManager.GetActiveScene();



    }

    public void spawnPlatformOnGrid(Vector3 position, GameObject gameObject)
    {
        GameObject gameObjectGeneric = gameObject;
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<float> distances = new List<float>();
        if (gameState.gridManager.gridSquares.Count > 0)
        {
            for (int i = 0; i < gameState.gridManager.gridSquares.Count; i++)
            {
                distances.Add(Vector3.Distance(position, gameState.gridManager.gridSquares[i]));
            }
        }
        
        int minimumValueIndex = distances.IndexOf(distances.Min());

        gameObject.transform.position = gameState.gridManager.gridSquares[minimumValueIndex] + rampAdjustment;

        if (gameState.levelManager.levelPlatformen.tileList != null)
        {
            if(gameObject.name.Contains("PlatformSquare"))
                gameState.levelManager.levelPlatformen.tileList[minimumValueIndex] = 3;
            else if(gameObject.name.Contains("RampSmall"))
                gameState.levelManager.levelPlatformen.tileList[minimumValueIndex] = 1;
        }
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    internal void BuildLevelFromText(LevelPlatformen levelPlatformen)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        for (int i = 0; i < levelPlatformen.tileList.Length; i++)
        {
            if (levelPlatformen.tileList[i] == 0)
            {

            }
            else if (levelPlatformen.tileList[i] == 1)
            {
                bigRamp.SpawnRamp(gameState.gridManager.gridSquares[i]);
            }
            else if (levelPlatformen.tileList[i] == 2)
            {
                bigRamp.SpawnRampReversed(gameState.gridManager.gridSquares[i]);
            }
            else if (levelPlatformen.tileList[i] == 3)
            {

                PlatformSquare = Instantiate(PlatformSquare, gameState.gridManager.gridSquares[i] + rampAdjustment, new Quaternion(0, 0, 0, 0));
                PlatformSquare.transform.Rotate(new Vector3(-90f, -90f, 0));
            }
        }
    }

    internal void Build_Level1(LevelPlatformen levelPlatformen)
    {

        //deze methode maakt hardcoded een level dit is voor testing doeleinden opgezet
        //later moeten levels gebouwd worden door middel van drag en drop en dit opgeslagen in een txt file
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();

        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);

        //levelPlatformen.tileList[12] = 1;
        //betekent op tile 12 staat een ramp
        //1 is ramp
        //2 is rampreversed
        //3 is platform



        //bouw stom lijstje
        RampSpots.Add(12);
        levelPlatformen.tileList[12] = 1;
        RampSpots.Add(24);
        levelPlatformen.tileList[24] = 1;
        RampSpots.Add(66);
        levelPlatformen.tileList[66] = 1;

        //bouw stom lijst voor ramps andere kant op
        RampSpotsReversed.Add(42);
        levelPlatformen.tileList[42] = 2;
        RampSpotsReversed.Add(52);
        levelPlatformen.tileList[52] = 2;
        RampSpotsReversed.Add(57);
        levelPlatformen.tileList[57] = 2;
        RampSpotsReversed.Add(97);
        levelPlatformen.tileList[97] = 2;
        RampSpotsReversed.Add(107);
        levelPlatformen.tileList[107] = 2;

        //bouw stom lijstje
        PlatformSpots.Add(34);
        levelPlatformen.tileList[34] = 3;
        PlatformSpots.Add(35);
        levelPlatformen.tileList[35] = 3;
        PlatformSpots.Add(36);
        levelPlatformen.tileList[36] = 3;
        PlatformSpots.Add(37);
        levelPlatformen.tileList[37] = 3;
        PlatformSpots.Add(38);
        levelPlatformen.tileList[38] = 3;
        PlatformSpots.Add(39);
        levelPlatformen.tileList[39] = 3;
        PlatformSpots.Add(40);
        levelPlatformen.tileList[40] = 3;
        PlatformSpots.Add(58);
        levelPlatformen.tileList[58] = 3;
        PlatformSpots.Add(59);
        levelPlatformen.tileList[59] = 3;
        PlatformSpots.Add(60);
        levelPlatformen.tileList[60] = 3;
        PlatformSpots.Add(61);
        levelPlatformen.tileList[61] = 3;
        PlatformSpots.Add(62);
        levelPlatformen.tileList[62] = 3;
        PlatformSpots.Add(63);
        levelPlatformen.tileList[63] = 3;
        PlatformSpots.Add(64);
        levelPlatformen.tileList[64] = 3;
        PlatformSpots.Add(77);
        levelPlatformen.tileList[77] = 3;
        PlatformSpots.Add(78);
        levelPlatformen.tileList[78] = 3;
        PlatformSpots.Add(79);
        levelPlatformen.tileList[79] = 3;
        PlatformSpots.Add(80);
        levelPlatformen.tileList[80] = 3;
        PlatformSpots.Add(81);
        levelPlatformen.tileList[81] = 3;
        PlatformSpots.Add(82);
        levelPlatformen.tileList[82] = 3;
        PlatformSpots.Add(83);
        levelPlatformen.tileList[83] = 3;
        PlatformSpots.Add(84);
        levelPlatformen.tileList[84] = 3;
        PlatformSpots.Add(85);
        levelPlatformen.tileList[85] = 3;
        PlatformSpots.Add(87);
        levelPlatformen.tileList[87] = 3;

        for (int i = 0; i < RampSpots.Count; i++)
        {
            ramp.SpawnRamp(gameState.gridManager.gridSquares[RampSpots[i]]);
        }

        for (int i = 0; i < PlatformSpots.Count; i++)
        {
            PlatformSquare = Instantiate(PlatformSquare, gameState.gridManager.gridSquares[PlatformSpots[i]] + rampAdjustment, new Quaternion(0, 0, 0, 0));
            PlatformSquare.transform.Rotate(new Vector3(-90f, -90f, 0));
        }

        for (int i = 0; i < RampSpotsReversed.Count; i++)
        {
            ramp.SpawnRampReversed(gameState.gridManager.gridSquares[RampSpotsReversed[i]]);
        }

    }

    internal void Build_Vertical_Slice_Level2()
    {
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        RampSpots.Add(20);
        TrampolineSpots.Add(117);
        for (int i = 0; i < RampSpots.Count; i++)
        {
            bigRamp.SpawnRamp(gameState.gridManager.gridSquares[RampSpots[i]]);
        }

        for (int i = 0; i < PlatformSpots.Count; i++)
        {
            PlatformSquare = Instantiate(PlatformSquare, gameState.gridManager.gridSquares[PlatformSpots[i]] + rampAdjustment, new Quaternion(0, 0, 0, 0));
            PlatformSquare.transform.Rotate(new Vector3(-90f, -90f, 0));
        }
        for (int i = 0; i < FinishSpots.Count; i++)
        {
            finish = Instantiate(finish, gameState.gridManager.gridSquares[FinishSpots[i]], new Quaternion(0, 0, 0, 0));

        }
        for (int i = 0; i < TrampolineSpots.Count; i++)
        {
            trampoline = Instantiate(trampoline, gameState.gridManager.gridSquares[TrampolineSpots[i]] + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0));
        }
    }

    internal void Build_Vertical_Slice_Level1()
    {
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();

        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);

        RampSpots.Add(9);
        RampSpots.Add(29);

        PlatformSpots.Add(38);
        PlatformSpots.Add(39);
        FinishSpots.Add(31);

        for (int i = 0; i < RampSpots.Count; i++)
        {
            bigRamp.SpawnRamp(gameState.gridManager.gridSquares[RampSpots[i]]);
        }

        for (int i = 0; i < PlatformSpots.Count; i++)
        {
            PlatformSquare = Instantiate(PlatformSquare, gameState.gridManager.gridSquares[PlatformSpots[i]] + rampAdjustment, new Quaternion(0, 0, 0, 0));
            PlatformSquare.transform.Rotate(new Vector3(-90f, -90f, 0));
        }
        for (int i = 0; i < FinishSpots.Count; i++)
        {
            finish = Instantiate(finish, gameState.gridManager.gridSquares[FinishSpots[i]], new Quaternion(0, 0, 0, 0));
            
        }
    }

    internal void Build_Level2()
    {

        //deze methode maakt hardcoded een level dit is voor testing doeleinden opgezet
        //later moeten levels gebouwd worden door middel van drag en drop en dit opgeslagen in een txt file
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();

        //levelPlatformen.tileList[12] = 1;
        //betekent op tile 12 staat een ramp
        //1 is ramp
        //2 is rampreversed
        //3 is platform



        //bouw stom lijstje
        RampSpots.Add(21);
        RampSpots.Add(42);
        //RampSpotsReversed.Add(14);
        //PortalSpots.Add(24);
        // PortalSpots.Add(54);
        // PlatformSpots.Add(53);
        TrampolineSpots.Add(63);
        TrampolineSpots.Add(26);
        rechthoekSpots.Add(66);
        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots);

    }
    internal void Init_Platforms(List<int> RampSpots, List<int> PlatformSpots, List<int> RampSpotsReversed, List<int> PortalSpots, List<int> TrampolineSpots, List<int> rechthoekSpots)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        if (RampSpots.Count > 0)
        {
            for (int i = 0; i < RampSpots.Count; i++)
            {
                ramp.SpawnRamp(gameState.gridManager.gridSquares[RampSpots[i]]);
            }
        }
        if (PlatformSpots.Count > 0)
        {
            for (int i = 0; i < PlatformSpots.Count; i++)
            {
                PlatformSquare = Instantiate(PlatformSquare, gameState.gridManager.gridSquares[PlatformSpots[i]] + rampAdjustment, new Quaternion(0, 0, 0, 0));
                PlatformSquare.transform.Rotate(new Vector3(-90f, -90f, 0));
            }
        }
        if (RampSpotsReversed.Count > 0)
        {
            for (int i = 0; i < RampSpotsReversed.Count; i++)
            {
                ramp.SpawnRampReversed(gameState.gridManager.gridSquares[RampSpotsReversed[i]]);
            }
        }
        if (PortalSpots.Count > 0)
        {
            for (int i = 0; i < PortalSpots.Count; i++)
            {
                portal = Instantiate(portal, gameState.gridManager.gridSquares[PortalSpots[i]] + new Vector3(.5f, .5f, 0), new Quaternion(0, 0, 0, 0));
                allPortals.Add(portal);
            }
        }
        if (TrampolineSpots.Count > 0)
        {
            for (int i = 0; i < TrampolineSpots.Count; i++)
            {
                trampoline = Instantiate(trampoline, gameState.gridManager.gridSquares[TrampolineSpots[i]] + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0));
            }
        }
        if (rechthoekSpots.Count > 0)
        {
            for (int i = 0; i < rechthoekSpots.Count; i++)
            {
                rechthoek = Instantiate(rechthoek, gameState.gridManager.gridSquares[rechthoekSpots[i]] + new Vector3(.5f, 0, 0), new Quaternion(0, 0, 0, 0));

            }
        }
    }



}
