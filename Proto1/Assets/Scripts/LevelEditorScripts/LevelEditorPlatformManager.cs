using System;
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
    public Ramp rampReversedClass;
    [SerializeField]
    public LevelEditorTrampoline trampolineClass;
    [SerializeField]
    public LevelEditorBoost boostPlatformClass;
    [SerializeField]
    public LevelEditorCannon cannonClass;
    [SerializeField]
    public LevelEditorRedZone redZoneClass;
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
        // 2 = rampsmallReversed
        // 3 = platformSwaure
        // 4 = trampoline
        // 5 = boostplatform
        // 6 = cannon
        // 7 = redzone
        if (LevelEditorState.Instance.levelPlatformen.tileList != null)
        {
            if (gameObject.name.Contains("RampSmall"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 1;
            }
            else if (gameObject.name.Contains("RampReversed"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 2;
            }
            else if (gameObject.name.Contains("LevelEditorPlatform"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 3;
            }
            else if (gameObject.name.Contains("Trampoline"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 4;
            }
            else if (gameObject.name.Contains("Booster"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 5;
            }
            else if (gameObject.name.Contains("Cannon"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 6;
            }
            else if (gameObject.name.Contains("RedZone"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 7;
                gameObject.transform.position += new Vector3(-0.5f,0,0);
            }
        }
    }

    internal void BuildLevelFromText(LevelPlatformen levelPlatformen)
    {
        // code voor opslaan/laden van levels
        // 0 = leeg;
        // 1 = rampsmall
        // 2 = rampsmallReversed
        // 3 = platformSwaure
        // 4 = trampoline
        // 5 = boostplatform
        // 6 = cannon
        // 7 = redzone
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        for (int i = 0; i < levelPlatformen.tileList.Length; i++)
        {

            if (levelPlatformen.tileList[i] == 0)
            {

            }
            else if (levelPlatformen.tileList[i] == 1)
            {
                BigRamp ramp = Instantiate(bigRampClass, LevelEditorState.Instance.gridManager.gridSquares[i+1], new Quaternion(0, 0, 0, 0));
                ramp.gameObject.AddComponent<LevelEditorPlatformDragManager>();
                ramp.transform.Rotate(new Vector3(-90f, -90f, 0));

            }
            else if (levelPlatformen.tileList[i] == 2)
            {
                Ramp ramp = Instantiate(rampReversedClass, LevelEditorState.Instance.gridManager.gridSquares[i + 1], new Quaternion(0, 0, 0, 0));
                ramp.gameObject.AddComponent<LevelEditorPlatformDragManager>();
                ramp.transform.Rotate(new Vector3(-90f, 90f, 0));
            }
            else if (levelPlatformen.tileList[i] == 3)
            {
                PlatformSquare square = Instantiate(platformSquareClass, LevelEditorState.Instance.gridManager.gridSquares[i+1] , new Quaternion(0, 0, 0, 0));
                square.gameObject.AddComponent<LevelEditorPlatformDragManager>();
                square.transform.Rotate(new Vector3(-90, 90, 0));

            }
            else if (levelPlatformen.tileList[i] == 4)
            {
                Instantiate(trampolineClass, LevelEditorState.Instance.gridManager.gridSquares[i+1],new Quaternion(0, 0, 0, 0));
            }
            else if (levelPlatformen.tileList[i] == 5)
            {
                Instantiate(boostPlatformClass, LevelEditorState.Instance.gridManager.gridSquares[i+1], new Quaternion(0, 0, 0, 0));
            }
            else if (levelPlatformen.tileList[i] == 6)
            {
                Instantiate(cannonClass, LevelEditorState.Instance.gridManager.gridSquares[i], new Quaternion(0, 0, 0, 0));
            }
            else if (levelPlatformen.tileList[i] == 7)
            {
                Instantiate(redZoneClass, LevelEditorState.Instance.gridManager.gridSquares[i]+rampAdjustment, new Quaternion(0, 0, 0, 0));
            }
        }
    }
}
