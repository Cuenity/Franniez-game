using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{
    public GameObject platform;
    public GameObject ramp;



    public GameObject lijn;
    public GameObject gridDot;
    public GameObject gridSquare;
    public List<Vector3[]> gridPunten = new List<Vector3[]>();

    Scene currentScene;

    List<Vector3> platformPositions;

    // Start is called before the first frame update
    private void Awake()
    {
        platformPositions = new List<Vector3>();
    }
    void Start()
    {

        GameState gameState = GetComponent<GameState>();

        currentScene = SceneManager.GetActiveScene();

        if (currentScene.name == "TestAnne")
        {
            Build_Grid();
            Build_level();
        }

        if (currentScene.name == "TestLevel1")
        {
            Build_Grid1();
        }

        
    }

    private void Build_Grid1()
    {
        //variablen
        Vector3 moveRight = new Vector3(0f, 0f, -1f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < 10; i++)
        {
            gridStartingPoint = gridStartingPoint + moveDown;
            gridStartingPoint.z = 0;

            for (int i2 = 0; i2 < 20; i2++)
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
        //wat roept dit aan?
        if (currentScene.name == "TestAnne")
        {
            Vector3 platform1 = new Vector3(1, 1, 1);
            Vector3 platform2 = new Vector3(3, 5, 1);

            platformPositions.Add(platform1);
            platformPositions.Add(platform2);
            foreach (Vector3 item in platformPositions)
            {
                platform = Instantiate(platform);
                platform.transform.position = item;
                platform.transform.Rotate(new Vector3(0, 0, 2));


            }
        }
        //spawn level in grid
    }

    internal void Build_level()
    {
        Vector3 blok1Spawn = gridPunten[0][0];

        GameObject plankje1;
        GameObject plankje2;
        GameObject ramp1;
        GameObject ramp2;
        GameObject ramp3;

        GameObject snapRamp;

        Vector3 rampcorrection = new Vector3(0, -0.5f, -0.5f);

        //nu ipv hardcoded positie enzo gebruik maken van gridpoints
        ramp1 = Instantiate(ramp, gridPunten[0][0]+rampcorrection, new Quaternion(0, 0, 0, 0));
        ramp1.transform.localScale = new Vector3(1, 50, 50);
        ramp1.transform.Rotate(new Vector3(-90, 0, 0));

        ramp2 = Instantiate(ramp, gridPunten[101][0] + rampcorrection, new Quaternion(0, 0, 0, 0));
        ramp2.transform.localScale = new Vector3(1, 50, 50);
        ramp2.transform.Rotate(new Vector3(-90, 0, 0));

        ramp3 = Instantiate(ramp, gridPunten[202][0] + rampcorrection, new Quaternion(0, 0, 0, 0));
        ramp3.transform.localScale = new Vector3(1, 50, 50);
        ramp3.transform.Rotate(new Vector3(-90, 0, 0));

        //werkende code maar wel hardcoded
        //plankje1 = Instantiate(platform, new Vector3(0,-1,-4.5f),new Quaternion(0f,0f,0f,0f));
        //plankje1.transform.localScale = new Vector3(1, 112.5f, 100);
        //plankje1.transform.Rotate(new Vector3(-90, 0, 0));

        //plankje2 = Instantiate(platform, new Vector3(0, -5, -8.5f), new Quaternion(0f, 0f, 0f, 0f));
        //plankje2.transform.localScale = new Vector3(1, 112.5f, 100);
        //plankje2.transform.Rotate(new Vector3(-180, 0, 0));

        //ramp1 = Instantiate(ramp, new Vector3(0, -1, -9.5f), new Quaternion(0, 0, 0, 0));
        //ramp1.transform.localScale = new Vector3(1, 50, 50);
        //ramp1.transform.Rotate(new Vector3(-90, 0, 0));

        //snaptest
        snapRamp = Instantiate(ramp, new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));
        snapRamp.transform.localScale = new Vector3(1, 50, 50);
        snapRamp.transform.Rotate(new Vector3(-90, 0, 0));
        //if (gridPunten[0][4] != snapRamp.transform.position)
        //{
        //    snapRamp.transform.position = gridPunten[0][4];
        //}

        //
        //
        //
        //
        //
        //
        //dit moet bij elke spawn
        bool isInGrid = false;
        for (int i = 0; i < gridPunten.Count; i++)
        {
            //check of gridpunten[i][5] hetzelfde is als het gespawnde object
            if (gridPunten[i][4] == snapRamp.transform.position)
            {
                isInGrid = true;
            }
        }
        if (!isInGrid)
        {
            //vind het dichtsbijzijnde gridpunt?
            Vector3 difference = snapRamp.transform.position - gridPunten[0][4];
            Vector3 closest = gridPunten[0][4];
            for (int i = 0; i < gridPunten.Count; i++)
            {
                //if (difference > snapRamp.transform.position - gridPunten[i][5])
                //{

                //}
            }
        }

    }

    internal void Build_Grid()
    {
        //variablen
        Vector3 moveRight = new Vector3(0f, 0f, -1f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f,0f,0f);


        for (int i = 0; i < 100; i++)
        {
            gridStartingPoint = gridStartingPoint + moveDown;
            gridStartingPoint.z = 0;

            for (int i2 = 0; i2 < 100; i2++)
            {
                Build_Square(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
    }
    internal void Build_Square(Vector3 startingPoint)
    {
        GameObject lijn1;
        GameObject lijn2;
        GameObject lijn3;
        GameObject lijn4;

        GameObject gridDot1;
        GameObject gridDot2;
        GameObject gridDot3;
        GameObject gridDot4;
        GameObject gridDot5;


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

        //opslaan gridpunten
        //1linksboven
        //2rechtsboven
        //3linksonder
        //4rechtsonder
        //5midden
        Vector3[] vectors = new Vector3[5];

        vectors[0] = startingPoint + new Vector3(0f, 0.5f,  0f);
        vectors[1] = startingPoint + new Vector3(0f, 0.5f, -1f);
        vectors[2] = startingPoint + new Vector3(0f, -0.5f  , 0f);
        vectors[3] = startingPoint + new Vector3(0f, -0.5f  , -1f);
        vectors[4] = startingPoint + new Vector3(0, 0, -0.5f);

        //build dots
        gridDot1 = Instantiate(gridDot, vectors[0], new Quaternion(0f, 0f, 0f, 0f));
        gridDot2 = Instantiate(gridDot, vectors[1], new Quaternion(0f, 0f, 0f, 0f));
        gridDot3 = Instantiate(gridDot, vectors[2], new Quaternion(0f, 0f, 0f, 0f));
        gridDot4 = Instantiate(gridDot, vectors[3], new Quaternion(0f, 0f, 0f, 0f));
        //gridDot5 = Instantiate(gridDot, vectors[4], new Quaternion(0f, 0f, 0f, 0f));


        //toevoegen aan gridpunten
        gridPunten.Add(vectors);
    }
}
