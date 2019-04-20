﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LevelEditorPlatformManager : MonoBehaviour
{
    //Alle Platformen hier onderbrengen aub
    [SerializeField]
    public BigRamp bigRampClass;
    [SerializeField]
    public PlatformSquare platformSquareClass;
    [SerializeField]
    public Ramp rampClass;
    [SerializeField]
    public Trampoline trampolineClass;
    [SerializeField]
    public BoostPlatform boostPlatformClass;
    [SerializeField]
    public Cannon cannonClass;
    [SerializeField]
    public RedZone redZoneClass;
    [SerializeField]
    public LevelEditorBall ballClass;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void spawnPlatformOnGrid(Vector3 position, GameObject gameObject)
    {
        //als deze methode alleen is voor speler blokjes kunnen we hier die glow doen
        //GameObject gameObjectGeneric = gameObject;
        Vector3 rampAdjustment = new Vector3(1f, 0f, 0f);
        Vector3 gridAdjustment = new Vector3(0.5f, 0, 0);
        List<float> distances = new List<float>();
        if (LevelEditorState.Instance.gridManager.gridSquares.Count > 0)
        {
            for (int i = 0; i < LevelEditorState.Instance.gridManager.gridSquares.Count; i++)
            {

                distances.Add(Vector3.Distance(position, (LevelEditorState.Instance.gridManager.gridSquares[i]) + gridAdjustment));
            }
        }

        int minimumValueIndex = distances.IndexOf(distances.Min());

        if (minimumValueIndex % LevelEditorState.Instance.width == LevelEditorState.Instance.width - 1)
        {
            minimumValueIndex--;
        }
        //gridspot shit uitgecomment
        if (!gameObject.GetComponent<Cannon>())// && !LevelEditorState.Instance.gridManager.filledGridSpots[minimumValueIndex] && !LevelEditorState.Instance.gridManager.filledGridSpots[minimumValueIndex + 1])
        {
            gameObject.transform.position = LevelEditorState.Instance.gridManager.gridSquares[minimumValueIndex] + rampAdjustment;
            //gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
            //AddFilledGridSpots(minimumValueIndex);
        }
        else if (gameObject.GetComponent<Cannon>())// && !LevelEditorState.Instance.gridManager.filledGridSpots[minimumValueIndex])
        {
            gameObject.transform.position = LevelEditorState.Instance.gridManager.gridSquares[minimumValueIndex] + rampAdjustment;
            //gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
            //AddFilledGridSpots(minimumValueIndex);
        }
        else
        {
            gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
            GameState.Instance.buttonManager.UpdatePlayerPlatforms(gameObject);
            Handheld.Vibrate();
        }

        // code voor opslaan van levels
        // 0 = leeg;
        // 1 = rampsmall
        if (LevelEditorState.Instance.levelPlatformen.tileList != null)
        {
            if (gameObject.name.Contains("PlatformSquare"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 3;
            }
            else if (gameObject.name.Contains("RampSmall"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 1;
            }
        }
    }

    internal void BuildLevelFromText(LevelPlatformen levelPlatformen)
    {
        
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        for (int i = 0; i < levelPlatformen.tileList.Length; i++)
        {
            //Conditions broken
            if (levelPlatformen.tileList[i] == 0)
            {

            }
            else if (levelPlatformen.tileList[i] == 1)
            {
                bigRampClass.SpawnRamp(LevelEditorState.Instance.gridManager.gridSquares[i]);
            }
            else if (levelPlatformen.tileList[i] == 2)
            {
                bigRampClass.SpawnRampReversed(LevelEditorState.Instance.gridManager.gridSquares[i]);
            }
            else if (levelPlatformen.tileList[i] == 3)
            {

                Instantiate(platformSquareClass, LevelEditorState.Instance.gridManager.gridSquares[i] + rampAdjustment, new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
