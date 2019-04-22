﻿using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public bool[] filledGridSpots;
    public GameObject gridSquareBuild;

    public GameObject gridSquare;
    public List<Vector3> gridSquares = new List<Vector3>();

    public int width;
    public int heigth;

    public List<Vector3> secretGridSquares = new List<Vector3>();

    GameState gameState;
    public RedZone redZone;
    public RedZone bottemredZone;
    public RedZone cornerredzone;

    private void Awake()
    {
        gameState = GameState.Instance;
    }

    public void AddFilledGridSpots(List<int> gridSpots, SizeType type)
    {
        switch (type)
        {
            case SizeType.oneByOne:
                foreach (int gridSpot in gridSpots)
                {
                    filledGridSpots[gridSpot] = true;
                }
                break;
            case SizeType.twoByOne:
                foreach (int gridSpot in gridSpots)
                {
                    filledGridSpots[gridSpot] = true;
                    filledGridSpots[gridSpot + 1] = true;
                }
                break;
            case SizeType.twoByTwo:
                Debug.Log("not finished portal filledgridspots");
                // portal
                break;
            case SizeType.oneByTwo:
                Debug.Log("not finished flag filledgridspots");
                // vlag
                break;
            default:
                break;
        }
    }

    public void RemoveFilledGridSpots(int gridSpot, SizeType type)
    {
        switch (type)
        {
            case SizeType.oneByOne:
                filledGridSpots[gridSpot] = false;
                break;
            case SizeType.twoByOne:
                filledGridSpots[gridSpot] = false;
                filledGridSpots[gridSpot + 1] = false;
                break;
            case SizeType.twoByTwo:
                Debug.Log("not finished portal filledgridspots");
                // portal
                break;
            case SizeType.oneByTwo:
                Debug.Log("not finished flag filledgridspots");
                // vlag
                break;
            default:
                break;
        }
    }

    //internal void Build_Grid1_With_Visuals()
    //{
    //    //variablen
    //    Vector3 moveRight = new Vector3(1f, 0f, 0f);
    //    Vector3 moveDown = new Vector3(0f, -1f, 0f);
    //    Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


    //    for (int i = 0; i < heigth; i++)
    //    {
    //        gridStartingPoint.x = 0f;
    //        gridStartingPoint = gridStartingPoint + moveDown;

    //        for (int i2 = 0; i2 < width; i2++)
    //        {
    //            Build_SquareBetter(gridStartingPoint);
    //            //place 2 the left
    //            gridStartingPoint = gridStartingPoint + moveRight;
    //        }
    //    }
    //}
    //private void Build_SquareBetter(Vector3 gridStartingPoint)
    //{
    //    GameObject gridSquare1;

    //    gridSquare1 = Instantiate(gridSquare, gridStartingPoint, new Quaternion(0, 0, 0, 0));
    //    gridSquare1.transform.Rotate(new Vector3(0, -90, 0));

    //    //add alle squares aan een lijstje
    //    gridSquares.Add(gridSquare1.transform.position);

    //}

    //internal void Build_Grid1_Without_Visuals()
    //{
    //    //variablen
    //    Vector3 moveRight = new Vector3(1f, 0f, 0f);
    //    Vector3 moveDown = new Vector3(0f, -1f, 0f);
    //    Vector3 gridStartingPoint = new Vector3(0, 0f, 0f);


    //    for (int i = 0; i < heigth; i++)
    //    {
    //        gridStartingPoint.x = 0f;
    //        gridStartingPoint = gridStartingPoint + moveDown;

    //        for (int i2 = 0; i2 < width; i2++)
    //        {
    //            gridSquares.Add(gridStartingPoint);
    //            //place 2 the left
    //            gridStartingPoint = gridStartingPoint + moveRight;
    //        }
    //    }
    //}

    //public void Build_Grid_Level_Editor_Without_Visuals()
    //{
    //    Vector3 moveRight = new Vector3(1f, 0f, 0f);
    //    Vector3 moveDown = new Vector3(0f, -1f, 0f);
    //    Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);

    //    InputField widthObject = GameObject.Find("Width").GetComponent<InputField>();
    //    InputField heigthObject = GameObject.Find("Heigth").GetComponent<InputField>();

    //    if (widthObject.text != "" && heigthObject.text != "")
    //    {
    //        width = Convert.ToInt32(widthObject.text);
    //        heigth = Convert.ToInt32(heigthObject.text);
    //    }


    //    for (int i = 0; i < heigth; i++)
    //    {
    //        gridStartingPoint.x = 0f;
    //        gridStartingPoint = gridStartingPoint + moveDown;

    //        for (int i2 = 0; i2 < width; i2++)
    //        {
    //            gameState.gridManager.gridSquares.Add(gridStartingPoint);
    //            //place 2 the left
    //            gridStartingPoint = gridStartingPoint + moveRight;

    //        }
    //    }
    //    gameState.levelManager.levelPlatformen.tileList = new int[heigth * width];
    //}

    internal void Build_Grid_BuildingPhase_With_Visuals()
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = 0f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBest(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        gameState.levelManager.levelPlatformen.tileList = new int[heigth * width];
        filledGridSpots = new bool[heigth * width];
    }
    internal void Build_Grid_BuildingPhase_Without_Visuals()
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = 0f;
            RedZone redZonebefore = Instantiate(redZone);
            RedZone redZoneAfter = Instantiate(redZone);
            redZonebefore.transform.position = gridStartingPoint + new Vector3(0, -1, 1f);
            redZoneAfter.transform.position = gridStartingPoint + new Vector3(gameState.gridManager.width , -1, 1f);
            //for (int i4 = 0; i4 < width; i4++)
            //{
            //    if (i4 == 0)
            //    {
            //        cornerredzone = Instantiate(cornerredzone);
            //        cornerredzone.transform.position = new Vector3(i4, -0.5f, 1f);
            //        cornerredzone = Instantiate(cornerredzone);
            //        cornerredzone.transform.position = new Vector3(width, -0.5f, 1f);
            //    }
            //    bottemredZone = Instantiate(bottemredZone);
            //    bottemredZone.transform.position = new Vector3(i4+.5f , -0.5f, 1f);
            //}

            gridStartingPoint = gridStartingPoint + moveDown;
            if (i == heigth - 1)
            {
                for (int i3 = 0; i3 < width; i3++)
                {
                    if (i3 == 0)
                    {
                        cornerredzone = Instantiate(cornerredzone);
                        cornerredzone.transform.position = new Vector3(i3, -heigth - .5f, 1f);
                        cornerredzone = Instantiate(cornerredzone);
                        cornerredzone.transform.position = new Vector3(width, -heigth - .5f, 1f);
                    }
                    bottemredZone = Instantiate(bottemredZone);
                    bottemredZone.transform.position = new Vector3(i3+.5f, -heigth - .5f, 1f);
                }
            }

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBest_no_instance(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        gameState.levelManager.levelPlatformen.tileList = new int[heigth * width];
        filledGridSpots = new bool[heigth * width];
    }

    internal void Build_SquareBest(Vector3 startingPoint)
    {
        GameObject gridSquare1;

        gridSquare1 = Instantiate(gridSquareBuild, startingPoint, new Quaternion(0, 0, 0, 0));

        //add alle squares aan een lijstje
        gridSquares.Add(gridSquare1.transform.position);
    }
    internal void Build_SquareBest_no_instance(Vector3 startingPoint)
    {
        //GameObject gridSquare1;

        //gridSquare1 = Instantiate(gridSquareBuild, startingPoint, new Quaternion(0, 0, 0, 0));

        //add alle squares aan een lijstje
        gridSquares.Add(startingPoint);
    }
    //LevelEditor Code
    internal void Build_Grid_FromJSON_With_Visuals(int width, int heigth)
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = 0f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBest(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        filledGridSpots = new bool[heigth * width];
        this.heigth = heigth;
        this.width = width;
        //onnodige balllllshit
        //LevelEditorState.Instance.levelPlatformen.tileList = new int[heigth * width];

    }
    internal void Build_Grid_FromJSON_Without_Visuals(int width, int heigth)
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = 0f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBest_no_instance(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        filledGridSpots = new bool[heigth * width];
        this.heigth = heigth;
        this.width = width;
        //onnodige balllllshit
        //LevelEditorState.Instance.levelPlatformen.tileList = new int[heigth * width];

    }
}
