using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{

    public GameObject gridSquare;
    public List<Vector3> gridSquares = new List<Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void Build_Grid1()
    {
        //variablen
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(-7.5f, 7f, 0f);


        for (int i = 0; i < 11; i++)
        {
            gridStartingPoint.x = -7.5f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < 15; i2++)
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
}
