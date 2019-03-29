﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platform;




    public GameObject lijn;
    public GameObject gridDot;
    public List<Vector3[]> gridPunten = new List<Vector3[]>();



    List<Vector3> platformPositions;

    // Start is called before the first frame update
    private void Awake()
    {
        platformPositions = new List<Vector3>();
    }
    void Start()
    {
        Build_Grid();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void spawnLevel1()
    {
       // platform = Instantiate(platform);

    }
    internal void Init_Platforms()
    {
        
        Vector3 platform1 = new Vector3(1, 1, 1);
        Vector3 platform2 = new Vector3(3, 5, 1);
     
        platformPositions.Add(platform1);
        platformPositions.Add(platform2);
        foreach (Vector3 item in platformPositions)
        {
            platform= Instantiate(platform);
            platform.transform.position = item;
            platform.transform.Rotate(new Vector3(0, 0, 2));


        }

        //spawn level in grid
    }
    internal void Build_Grid()
    {
        //variablen
        Vector3 moveRight = new Vector3(0f, 0f, -1f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f,0f,0f);


        for (int i = 0; i < 10; i++)
        {
            gridStartingPoint = gridStartingPoint + moveDown;
            gridStartingPoint.z = 0;

            for (int i2 = 0; i2 < 5; i2++)
            {
                Build_Square(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        int testbreak = 10 ;
    }
    internal void Build_Square(Vector3 startingPoint)
    {
        GameObject lijn1 = lijn;
        GameObject lijn2 = lijn;
        GameObject lijn3 = lijn;
        GameObject lijn4 = lijn;

        GameObject gridDot1 = lijn;
        GameObject gridDot2 = lijn;
        GameObject gridDot3 = lijn;
        GameObject gridDot4 = lijn;


        //              ____________3_________
        //              |                    |
        //              |                    |
        //             1|                    |2
        //              |                    |
        //              |                    |
        //              |                    |
        //              ____________4________|
        Vector3 point1 = startingPoint;
        Vector3 point2 = startingPoint + new Vector3(0f, 0f, -1f);
        Vector3 point3 = startingPoint + new Vector3(0f, -0.5f, -0.5f);//new Vector3(1, 0.5f, 0.5f); 
        Vector3 point4 = startingPoint + new Vector3(0f, 0.5f, -0.5f);//new Vector3(1, 1.5f, 0.5f);

        Vector3 rotate = new Vector3(90f, 0f, 0f);
        

        //spawn lijn 1
        lijn1 = Instantiate(lijn, point1, new Quaternion(0f, 0f, 0f, 0f));
        //spawn lijn 2
        lijn2 = Instantiate(lijn, point2, new Quaternion(0f, 0f, 0f, 0f));
        //spawn lijn 3
        lijn3 = Instantiate(lijn, point3, new Quaternion(0f, 0f, 0f, 0f));
        lijn3.transform.Rotate(rotate);
        //spawn lijn 4
        lijn4 = Instantiate(lijn, point4, new Quaternion(0f, 0f, 0f, 0f));
        lijn4.transform.Rotate(rotate);

        Vector3[] vectors = new Vector3[4];

        vectors[0] = startingPoint + new Vector3(0f, 0.5f,  0f);
        vectors[1] = startingPoint + new Vector3(0f, 0.5f, -1f);
        vectors[2] = startingPoint + new Vector3(0f, -0.5f  , 0f);
        vectors[3] = startingPoint + new Vector3(0f, -0.5f  , -1f);

        //build dots
        gridDot1 = Instantiate(gridDot, vectors[0], new Quaternion(0f, 0f, 0f, 0f));
        gridDot2 = Instantiate(gridDot, vectors[1], new Quaternion(0f, 0f, 0f, 0f));
        gridDot3 = Instantiate(gridDot, vectors[2], new Quaternion(0f, 0f, 0f, 0f));
        gridDot4 = Instantiate(gridDot, vectors[3], new Quaternion(0f, 0f, 0f, 0f));

        //toevoegen aan gridpunten
        gridPunten.Add(vectors);
    }
}
