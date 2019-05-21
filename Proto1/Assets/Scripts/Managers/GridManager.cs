using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public bool[] filledGridSpots;
    public GameObject gridSquareBuild;

    public GameObject gridSquare;
    public List<Vector3> gridSquares = new List<Vector3>();
    public int[,] gridSquares2;

    public int width;
    public int height;

    public List<Vector3> secretGridSquares = new List<Vector3>();

    GameState gameState;
    public RedZone redZone;
    public RedZone bottemredZone;
    public RedZone cornerredzone;

    [SerializeField]
    GameObject playerGridRed, playerGridGreen;

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
                foreach (int gridSpot in gridSpots)
                {
                    filledGridSpots[gridSpot - GameState.Instance.gridManager.width] = true;
                    filledGridSpots[gridSpot + 1 - GameState.Instance.gridManager.width] = true;
                    filledGridSpots[gridSpot] = true;
                    filledGridSpots[gridSpot + 1] = true;
                }
                break;
            case SizeType.oneByTwo:
                foreach (int gridSpot in gridSpots)
                {
                    filledGridSpots[gridSpot - GameState.Instance.gridManager.width] = true;
                    filledGridSpots[gridSpot] = true;
                }
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
                filledGridSpots[gridSpot - GameState.Instance.gridManager.width] = false;
                filledGridSpots[gridSpot + 1 - GameState.Instance.gridManager.width] = false;
                filledGridSpots[gridSpot] = false;
                filledGridSpots[gridSpot + 1] = false;
                break;
            case SizeType.oneByTwo:
                filledGridSpots[gridSpot - GameState.Instance.gridManager.width] = false;
                filledGridSpots[gridSpot] = false;
                break;
            default:
                break;
        }
    }
    
    internal void Build_Grid_BuildingPhase_With_Visuals()
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < height; i++)
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
        gameState.levelManager.levelPlatformen.tileList = new int[height * width];
        filledGridSpots = new bool[height * width];
    }
    internal void Build_Grid_BuildingPhase_Without_Visuals()
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < height; i++)
        {
            gridStartingPoint.x = 0f;
            RedZone redZonebefore = Instantiate(redZone);
            RedZone redZoneAfter = Instantiate(redZone);
            redZonebefore.transform.position = gridStartingPoint + new Vector3(0, -1, 1f);
            redZoneAfter.transform.position = gridStartingPoint + new Vector3(gameState.gridManager.width, -1, 1f);
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
            if (i == height - 1)
            {
                for (int i3 = 0; i3 < width; i3++)
                {
                    if (i3 == 0)
                    {
                        cornerredzone = Instantiate(cornerredzone);
                        cornerredzone.transform.position = new Vector3(i3, -height - .5f, 1f);
                        cornerredzone = Instantiate(cornerredzone);
                        cornerredzone.transform.position = new Vector3(width, -height - .5f, 1f);
                    }
                    bottemredZone = Instantiate(bottemredZone);
                    bottemredZone.transform.position = new Vector3(i3 + .5f, -height - .5f, 1f);
                }
            }

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBest_no_instance(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        gameState.levelManager.levelPlatformen.tileList = new int[height * width];
        filledGridSpots = new bool[height * width];
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
        this.height = heigth;
        this.width = width;
        //onnodige balllllshit
        //LevelEditorState.Instance.levelPlatformen.tileList = new int[heigth * width];

    }
    internal void Build_Grid_FromJSON_Without_Visuals(int width, int heigth)
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);


        for (int i = 0; i < height; i++)
        {
            gridStartingPoint.x = 0f;
            RedZone redZonebefore = Instantiate(redZone);
            RedZone redZoneAfter = Instantiate(redZone);
            redZonebefore.transform.position = gridStartingPoint + new Vector3(0, -1, 1f);
            redZoneAfter.transform.position = gridStartingPoint + new Vector3(gameState.gridManager.width, -1, 1f);
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
            if (i == height - 1)
            {
                for (int i3 = 0; i3 < width; i3++)
                {
                    if (i3 == 0)
                    {
                        cornerredzone = Instantiate(cornerredzone);
                        cornerredzone.transform.position = new Vector3(i3, -height - .5f, 1f);
                        cornerredzone = Instantiate(cornerredzone);
                        cornerredzone.transform.position = new Vector3(width, -height - .5f, 1f);
                    }
                    bottemredZone = Instantiate(bottemredZone);
                    bottemredZone.transform.position = new Vector3(i3 + .5f, -height - .5f, 1f);
                }
            }

            for (int i2 = 0; i2 < width; i2++)
            {
                Build_SquareBest_no_instance(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;
            }
        }
        filledGridSpots = new bool[heigth * width];
        this.height = heigth;
        this.width = width;
        //onnodige balllllshit
        //LevelEditorState.Instance.levelPlatformen.tileList = new int[heigth * width];

    }
    
    //multiplayer grid methodes

    internal void InitPlayerGridsMultiPlayer(int topleftgrid1,int toprightgrid1,int bottomleftgrid1,int bottomrightgrid1,int topleftgrid2,int toprightgrid2,int bottomleftgrid2,int bottomrightgrid2)
    {
        //deze method is super logisch
        List<int> player1Grid = new List<int>();
        List<int> player2Grid = new List<int>();
        int widthadjustment = 0 ;
        for (int i = topleftgrid1; i < toprightgrid1+1; i++)
        {
            widthadjustment = 0;
            for (int i2 = bottomleftgrid1 = 0; i2 < bottomrightgrid1+1; i2++)
            {
                
                player1Grid.Add(i+widthadjustment);
                widthadjustment = widthadjustment + width;
            }
        }

        for (int i = topleftgrid2; i < toprightgrid2 + 1; i++)
        {
            widthadjustment = 0;
            for (int i2 = bottomleftgrid2 = 0; i2 < bottomrightgrid2 + 1; i2++)
            {

                player2Grid.Add(i + widthadjustment);
                widthadjustment = widthadjustment + width;
            }
        }
        if (PhotonNetwork.IsMasterClient)
        {
            AddPlayerGridToFilledGridSpots(player2Grid);
        }
        else
        {
            AddPlayerGridToFilledGridSpots(player1Grid);
        }
        showPlayerGrid(player1Grid,player2Grid);
    }

    internal void showPlayerGrid(List<int> player1Grid,List<int> player2Grid)
    {
        Vector3 gridadjustment = new Vector3(0.5f, 0, 0);
       
        foreach (var spot in player1Grid)
        {
            Instantiate(playerGridRed, gridSquares[spot] + gridadjustment, new Quaternion(0, 0, 0, 0)).transform.Rotate(new Vector3(-90, 0, 0));
        }
        foreach (var spot in player2Grid)
        {
            Instantiate(playerGridGreen, gridSquares[spot] + gridadjustment, new Quaternion(0, 0, 0, 0)).transform.Rotate(new Vector3(-90, 0, 0));
        }
    }

    internal void AddPlayerGridToFilledGridSpots(List<int> playergrid)
    {
        foreach (int spot in playergrid)
        {
            filledGridSpots[spot] = true;
        }
    }
}
