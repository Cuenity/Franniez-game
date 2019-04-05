using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GridManager : MonoBehaviour
{

    public GameObject gridSquareBuild;

    public GameObject gridSquare;
    public List<Vector3> gridSquares = new List<Vector3>();

    public int width ;
    public int heigth;

    public List<Vector3> secretGridSquares = new List<Vector3>();

    GameState gameState;

    private void Awake()
    {
        gameState = GameState.Instance;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Build_Grid1_With_Visuals()
    {
        //variablen
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(-7.5f, 7f, 0f);


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = -7.5f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBetter(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
    }
    private void Build_SquareBetter(Vector3 gridStartingPoint)
    {
        GameObject gridSquare1;

        gridSquare1 = Instantiate(gridSquare, gridStartingPoint, new Quaternion(0, 0, 0, 0));
        gridSquare1.transform.Rotate(new Vector3(0, -90, 0));

        //add alle squares aan een lijstje
        gridSquares.Add(gridSquare1.transform.position);
        
    }

    internal void Build_Grid1_Without_Visuals()
    {
        //variablen
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(-7.5f, 7f, 0f);


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = -7.5f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                gridSquares.Add(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
    }

    public void Build_Grid_Level_Editor_Without_Visuals()
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(-7.5f, 7f, 0f);

        InputField widthObject = GameObject.Find("Width").GetComponent<InputField>();
        InputField heigthObject = GameObject.Find("Heigth").GetComponent<InputField>();

        if (widthObject.text != "" && heigthObject.text != "")
        {
            width = Convert.ToInt32(widthObject.text);
            heigth = Convert.ToInt32(heigthObject.text);
        }


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = -7.5f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                gameState.gridManager.gridSquares.Add(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;

            }
        }
        gameState.levelManager.levelPlatformen.tileList = new int[heigth*width];
    }

    internal void Build_Grid_BuildingPhase_With_Visuals()
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(-7.5f, 7f, 0f);


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = -7.5f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBest(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        gameState.levelManager.levelPlatformen.tileList = new int[heigth * width];
    }

    internal void Build_SquareBest(Vector3 startingPoint)
    {
        GameObject gridSquare1;

        gridSquare1 = Instantiate(gridSquareBuild, startingPoint, new Quaternion(0, 0, 0, 0));

        //add alle squares aan een lijstje
        gridSquares.Add(gridSquare1.transform.position);
    }

}
