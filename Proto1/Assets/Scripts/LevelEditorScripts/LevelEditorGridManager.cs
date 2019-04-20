using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorGridManager : MonoBehaviour
{
    //nodig voor niet op dezelfde plek nutteloos voor dit
    //public bool[] filledGridSpots;

    public GameObject gridSquareBuild;

    public GameObject gridSquare;
    public List<Vector3> gridSquares = new List<Vector3>();
    

    public List<Vector3> secretGridSquares = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //voor het laden van levels
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

        //onnodige balllllshit
        //LevelEditorState.Instance.levelPlatformen.tileList = new int[heigth * width];
        //filledGridSpots = new bool[heigth * width];
    }
    
    internal void Build_Grid_Fresh_With_Visuals(int width, int heigth)
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
        LevelEditorState.Instance.levelPlatformen.width = width;
        LevelEditorState.Instance.levelPlatformen.heigth = heigth;
        LevelEditorState.Instance.levelPlatformen.tileList = new int[heigth * width];
    }

    private void Build_SquareBest(Vector3 gridStartingPoint)
    {
        GameObject gridSquare1;

        gridSquare1 = Instantiate(gridSquareBuild, gridStartingPoint, new Quaternion(0, 0, 0, 0));

        //add alle squares aan een lijstje
        gridSquares.Add(gridSquare1.transform.position);

    }
}
