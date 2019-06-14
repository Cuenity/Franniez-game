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
    public LevelEditorStar starClass;
    [SerializeField]
    public LevelEditorFinish finishClass;
    [SerializeField]
    public LevelEditorBall ballClass;


    public void spawnPlatformOnGrid(Vector3 position, GameObject platformToPlace)
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

        if (platformToPlace.name.Contains("Vlaggetje"))
        {
            platformToPlace.transform.position = LevelEditorState.Instance.gridManager.gridSquares[minimumValueIndex]+ new Vector3(0.5f,-0.5f);
        }
        //gridspot shit uitgecomment
        else if (!platformToPlace.GetComponent<Cannon>())// && !LevelEditorState.Instance.gridManager.filledGridSpots[minimumValueIndex] && !LevelEditorState.Instance.gridManager.filledGridSpots[minimumValueIndex + 1])
        {
            platformToPlace.transform.position = LevelEditorState.Instance.gridManager.gridSquares[minimumValueIndex] + rampAdjustment;
            //gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
            //AddFilledGridSpots(minimumValueIndex);
        }
        else if (platformToPlace.GetComponent<Cannon>())// && !LevelEditorState.Instance.gridManager.filledGridSpots[minimumValueIndex])
        {
            platformToPlace.transform.position = LevelEditorState.Instance.gridManager.gridSquares[minimumValueIndex] + rampAdjustment;
            //gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
            //AddFilledGridSpots(minimumValueIndex);
        }
        
        else
        {
            platformToPlace.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
            //GameState.Instance.buttonManager.UpdatePlayerPlatforms(platformToPlace);
            platformToPlace.GetComponent<Platform>().UpdatePlayerPlatforms();
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
        // 8 = star
        // 9 = finish
        if (LevelEditorState.Instance.levelPlatformen.tileList != null)
        {
            if (platformToPlace.name.Contains("RampSmall")||platformToPlace.name.Contains("BigRamp"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 1;
            }
            else if (platformToPlace.name.Contains("RampReversed"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 2;
            }
            else if (platformToPlace.name.Contains("LevelEditorPlatform"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 3;
            }
            else if (platformToPlace.name.Contains("Trampoline"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 4;
            }
            else if (platformToPlace.name.Contains("Booster"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 5;
            }
            else if (platformToPlace.name.Contains("Cannon"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 6;
            }
            else if (platformToPlace.name.Contains("RedZone"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 7;
                platformToPlace.transform.position += new Vector3(-0.5f,0,0);
            }
            else if (platformToPlace.name.Contains("Star"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 8;
                platformToPlace.transform.position += new Vector3(-0.5f, 0, 0);
            }
            else if (platformToPlace.name.Contains("Vlaggetje"))
            {
                LevelEditorState.Instance.levelPlatformen.tileList[minimumValueIndex] = 9;
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
        // 8 = star
        // 9 = finish
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
                LevelEditorTrampoline tramp = Instantiate(trampolineClass, LevelEditorState.Instance.gridManager.gridSquares[i+1],new Quaternion(0, 0, 0, 0));
                tramp.gameObject.AddComponent<LevelEditorPlatformDragManager>();
            }
            else if (levelPlatformen.tileList[i] == 5)
            {
                LevelEditorBoost boost =  Instantiate(boostPlatformClass, LevelEditorState.Instance.gridManager.gridSquares[i+1], new Quaternion(0, 0, 0, 0));
                boost.gameObject.AddComponent<LevelEditorPlatformDragManager>();
            }
            else if (levelPlatformen.tileList[i] == 6)
            {
                LevelEditorCannon cannon = Instantiate(cannonClass, LevelEditorState.Instance.gridManager.gridSquares[i], new Quaternion(0, 0, 0, 0));
                cannon.gameObject.AddComponent<LevelEditorPlatformDragManager>();
            }
            else if (levelPlatformen.tileList[i] == 7)
            {
                LevelEditorRedZone zone = Instantiate(redZoneClass, LevelEditorState.Instance.gridManager.gridSquares[i]+rampAdjustment, new Quaternion(0, 0, 0, 0));
                zone.gameObject.AddComponent<LevelEditorPlatformDragManager>();
            }
            else if (levelPlatformen.tileList[i] == 8)
            {
                LevelEditorStar star = Instantiate(starClass, LevelEditorState.Instance.gridManager.gridSquares[i] + rampAdjustment, new Quaternion(0, 0, 0, 0));
                star.gameObject.AddComponent<LevelEditorPlatformDragManager>();
            }
            else if (levelPlatformen.tileList[i] == 9)
            {
                LevelEditorFinish finish = Instantiate(finishClass, LevelEditorState.Instance.gridManager.gridSquares[i] + new Vector3(0.5f,-0.5f), new Quaternion(0, 0, 0, 0));
                finish.gameObject.AddComponent<LevelEditorPlatformDragManager>();
            }
        }
    }
}
