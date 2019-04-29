using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlatformManager : MonoBehaviour
{

    public GameObject platform;
    public GameObject rechthoek;
    public Ramp ramp;
    public GameObject PlatformSquare;
    public GameObject Balletje;
    public Portal portal;
    public List<Portal> allPortals = new List<Portal>();
    public Trampoline trampoline;
    public BigRamp bigRamp;
    public Finish finish;
    public PlatformBreekbaar platformBreekbaar;
    public RedZone redZone;
    public GameObject platformSquareNoGrass;
    public Coin coin;
    public BoostPlatform boostPlatform;
    public PlatformSquare levelEditorPlatform;
    public Ramp levelEditorRampReversed;
    public Lift lift;

    GameState gameState;


    public GameObject lijn;
    public GameObject gridDot;
    public GameObject gridSquare;
    //public List<Vector3[]> gridPunten = new List<Vector3[]>();


    Scene currentScene;

    //List<Vector3> platformPositions;

    private Ramp snapRamp;
    private GameObject snapPlatform;
    private GameObject snapPlatformSquare;

    private List<Vector3> coinPositions = new List<Vector3>();
    private Vector3 finishPosition;


    private void Awake()
    {
        //platformPositions = new List<Vector3>();

        gameState = GameState.Instance;
    }
    void Start()
    {
        currentScene = SceneManager.GetActiveScene();
    }

    public void RespawnCollectables()
    {
        foreach (Coin item in gameState.levelManager.coinList)
        {
            item.gameObject.SetActive(false);
        }
        gameState.levelManager.coinList.Clear();
        //gameState.levelManager.stickerObject.gameObject.SetActive(false);
        finish.gameObject.SetActive(false);

        gameState.collectableManager.RespawnCoins(coinPositions);
    }

    public void SetCoinPosition(int position)
    {
        Vector3 coinAdjustment = new Vector3(.3f, 0, 0);
        coinPositions.Add(gameState.gridManager.gridSquares[position] + coinAdjustment);

        List<int> coinPosition = new List<int>();
        coinPosition.Add(position);
        gameState.gridManager.AddFilledGridSpots(coinPosition, SizeType.oneByOne);
    }

    public void SetfinishPosition(int position)
    {
        Vector3 finishAdjustment = new Vector3(.5f, 0, 0);
        finishPosition = gameState.gridManager.gridSquares[position] + finishAdjustment;

        List<int> finishPositionFillsGridSpot = new List<int>();
        finishPositionFillsGridSpot.Add(position);
        gameState.gridManager.AddFilledGridSpots(finishPositionFillsGridSpot, SizeType.oneByTwo);
    }

    public void spawnPlatformOnGrid(Vector3 position, GameObject gameObject)
    {
        //als deze methode alleen is voor speler blokjes kunnen we hier die glow doen
        //GameObject gameObjectGeneric = gameObject;
        Vector3 rampAdjustment = new Vector3(1f, 0f, 0f);
        Vector3 cannonAdjustment = new Vector3(1f, 0.3f, 0f);
        Vector3 gridAdjustment = new Vector3(1f, 0, 0);
        List<float> distances = new List<float>();
        if (gameState.gridManager.gridSquares.Count > 0)
        {
            for (int i = 0; i < gameState.gridManager.gridSquares.Count; i++)
            {

                distances.Add(Vector3.Distance(position, (gameState.gridManager.gridSquares[i]) + gridAdjustment));
            }
        }

        int minimumValueIndex = distances.IndexOf(distances.Min());
        if (minimumValueIndex % gameState.gridManager.width == gameState.gridManager.width - 1)
        {
            minimumValueIndex--;
        }


        bool cantPlacePlaform = false;

        List<int> newFilledGridSPots = new List<int>();
        //newFilledGridSPots.Add(minimumValueIndex);

        if (!gameObject.GetComponent<Cannon>())
        {
            // check of platform hier geplaatst kan worden
            if (!gameState.gridManager.filledGridSpots[minimumValueIndex] &&
            !gameState.gridManager.filledGridSpots[minimumValueIndex + 1])
            {
                newFilledGridSPots.Add(minimumValueIndex);

                gameObject.transform.position = gameState.gridManager.gridSquares[minimumValueIndex] + rampAdjustment;
                gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
                gameState.gridManager.AddFilledGridSpots(newFilledGridSPots, SizeType.twoByOne);
            }
            // check of platform anders in een gebied van een square hier omheen geplaatst kan worden
            else
            {
                int newMinimumValueIndex = CheckForEmptyAreaAroundWrongPlacement(minimumValueIndex);
                if (newMinimumValueIndex < 0)
                {
                    cantPlacePlaform = true;
                }
                else
                {
                    newFilledGridSPots.Add(newMinimumValueIndex);

                    gameObject.transform.position = gameState.gridManager.gridSquares[newMinimumValueIndex] + rampAdjustment;
                    gameObject.GetComponent<Platform>().fillsGridSpot = newMinimumValueIndex;
                    gameState.gridManager.AddFilledGridSpots(newFilledGridSPots, SizeType.twoByOne);
                }
            }
        }
        // als het cannon is
        else
        {
            // check of cannon hier geplaatst kan worden
            if (minimumValueIndex < gameState.gridManager.width)
            {
                minimumValueIndex = minimumValueIndex + gameState.gridManager.width;
            }
            if (!gameState.gridManager.filledGridSpots[minimumValueIndex] &&
                !gameState.gridManager.filledGridSpots[minimumValueIndex + 1] &&
                !gameState.gridManager.filledGridSpots[minimumValueIndex - gameState.gridManager.width] &&
                !gameState.gridManager.filledGridSpots[minimumValueIndex - gameState.gridManager.width + 1])
            {
                newFilledGridSPots.Add(minimumValueIndex);

                gameObject.transform.position = gameState.gridManager.gridSquares[minimumValueIndex] + cannonAdjustment;
                gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
                gameState.gridManager.AddFilledGridSpots(newFilledGridSPots, SizeType.twoByTwo);
            }
            // check of cannon anders in een gebied van een square hier omheen geplaatst kan worden
            else
            {
                cantPlacePlaform = true;
                Debug.Log("Cannon op 1 square in de buurt plaatsen kan (nog) niet");

                //int newMinimumValueIndex = CheckForEmptyAreaAroundWrongPlacement(minimumValueIndex);
                //if (newMinimumValueIndex < 0)
                //{
                //    cantPlacePlaform = true;
                //}
                //else
                //{
                //    newFilledGridSPots.Add(newMinimumValueIndex); 

                //    gameObject.transform.position = gameState.gridManager.gridSquares[newMinimumValueIndex] + rampAdjustment;
                //    gameObject.GetComponent<Platform>().fillsGridSpot = newMinimumValueIndex;
                //    gameState.gridManager.AddFilledGridSpots(newFilledGridSPots, SizeType.twoByOne);
                //}
            }
        }

        // platform kan helmaal niet geplaatst worden
        if (cantPlacePlaform)
        {
            gameObject.GetComponent<Platform>().fillsGridSpot = minimumValueIndex;
            GameState.Instance.buttonManager.UpdatePlayerPlatforms(gameObject);
            Handheld.Vibrate();
        }

        // code voor opslaan van levels
        if (gameState.levelManager.levelPlatformen.tileList != null)
        {
            if (gameObject.name.Contains("PlatformSquare"))
            {
                gameState.levelManager.levelPlatformen.tileList[minimumValueIndex] = 3;
            }
            else if (gameObject.name.Contains("RampSmall"))
            {
                gameState.levelManager.levelPlatformen.tileList[minimumValueIndex] = 1;
            }
        }
    }

    private int CheckForEmptyAreaAroundWrongPlacement(int minimumValueIndex)
    {
        int newSpot;

        //bool[,] replaceArrayLaterWithRealTwoDimentionalArray = new bool[gameState.gridManager.height, gameState.gridManager.width];
        int column = 0;
        int row = 0;
        int minimumValueIndexRow = 0;
        int minimumValueIndexColumn = 0;
        for (int filledGridSpotsIndex = 0; filledGridSpotsIndex < gameState.gridManager.filledGridSpots.Length; filledGridSpotsIndex++)
        {
            if (column == gameState.gridManager.width)
            {
                column = 0;
                row++;
            }
            if (minimumValueIndex == filledGridSpotsIndex)
            {
                minimumValueIndexRow = row;
                minimumValueIndexColumn = column;
                break;
            }

            //replaceArrayLaterWithRealTwoDimentionalArray[row, column] = gameState.gridManager.filledGridSpots[filledGridSpotsIndex];
            column++;
        }

        // een rij naar links checken
        if (minimumValueIndexColumn - 1 >= 0)
        {
            newSpot = minimumValueIndex - 1;
            if (!gameState.gridManager.filledGridSpots[newSpot] && !gameState.gridManager.filledGridSpots[newSpot + 1])
            {
                return newSpot;
            }
        }

        // een rij naar rechts checken
        if (minimumValueIndexColumn + 1 < gameState.gridManager.width - 1)
        {
            newSpot = minimumValueIndex + 1;
            if (!gameState.gridManager.filledGridSpots[newSpot] && !gameState.gridManager.filledGridSpots[newSpot + 1])
            {
                return newSpot;
            }
        }

        // een rij naar beneden checken
        if (minimumValueIndexRow < gameState.gridManager.height - 1)
        {
            newSpot = minimumValueIndex + gameState.gridManager.width;
            if (!gameState.gridManager.filledGridSpots[newSpot] && !gameState.gridManager.filledGridSpots[newSpot + 1])
            {
                return newSpot;
            }
        }

        // een rij naar boven checken
        if (minimumValueIndexRow > 0)
        {
            newSpot = minimumValueIndex - gameState.gridManager.width;
            if (!gameState.gridManager.filledGridSpots[newSpot] && !gameState.gridManager.filledGridSpots[newSpot + 1])
            {
                return newSpot;
            }
        }

        // als er geen lege plek gevonden kan worden
        return -1;
    }

    internal void BuildLevelFromLevelPlatformen(LevelPlatformen levelPlatformen, int[] coins, int finish)
    {
        foreach (int item in coins)
        {
            SetCoinPosition(item);
        }

        SetfinishPosition(finish);
        gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
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
                BigRamp ramp = Instantiate(bigRamp, GameState.Instance.gridManager.gridSquares[i + 1], new Quaternion(0, 0, 0, 0));
                ramp.transform.Rotate(new Vector3(-90f, -90f, 0));
                List<int> gridSpots = new List<int>();
                gridSpots.Add(i);
                GameState.Instance.gridManager.AddFilledGridSpots(gridSpots, SizeType.twoByOne);
                //bigRampClass.SpawnRamp(LevelEditorState.Instance.gridManager.gridSquares[i]);
            }
            else if (levelPlatformen.tileList[i] == 2)
            {
                Ramp ramp = Instantiate(levelEditorRampReversed, GameState.Instance.gridManager.gridSquares[i + 1], new Quaternion(0, 0, 0, 0));
                ramp.transform.Rotate(new Vector3(-90f, 90f, 0));
                List<int> gridSpots = new List<int>();
                gridSpots.Add(i);
                GameState.Instance.gridManager.AddFilledGridSpots(gridSpots, SizeType.twoByOne);
            }
            else if (levelPlatformen.tileList[i] == 3)
            {
                Instantiate(levelEditorPlatform, GameState.Instance.gridManager.gridSquares[i + 1], new Quaternion(0, 0, 0, 0)).transform.Rotate(new Vector3(-90, 90, 0));
                List<int> gridSpots = new List<int>();
                gridSpots.Add(i);
                GameState.Instance.gridManager.AddFilledGridSpots(gridSpots, SizeType.twoByOne);
            }
            else if (levelPlatformen.tileList[i] == 4)
            {
                Instantiate(trampoline, GameState.Instance.gridManager.gridSquares[i + 1], new Quaternion(0, 0, 0, 0));
                List<int> gridSpots = new List<int>();
                gridSpots.Add(i);
                GameState.Instance.gridManager.AddFilledGridSpots(gridSpots, SizeType.twoByOne);
            }
            else if (levelPlatformen.tileList[i] == 5)
            {
                Instantiate(boostPlatform, GameState.Instance.gridManager.gridSquares[i + 1], new Quaternion(0, 0, 0, 0));
                List<int> gridSpots = new List<int>();
                gridSpots.Add(i);
                GameState.Instance.gridManager.AddFilledGridSpots(gridSpots, SizeType.twoByOne);
            }
            else if (levelPlatformen.tileList[i] == 6)
            {
                //faka cannon
                //Instantiate(cannon, LevelEditorState.Instance.gridManager.gridSquares[i], new Quaternion(0, 0, 0, 0));
            }
            else if (levelPlatformen.tileList[i] == 7)
            {
                Instantiate(redZone, GameState.Instance.gridManager.gridSquares[i] + rampAdjustment, new Quaternion(0, 0, 0, 0));
                List<int> gridSpots = new List<int>();
                gridSpots.Add(i);
                GameState.Instance.gridManager.AddFilledGridSpots(gridSpots, SizeType.oneByOne);
            }
        }
    }

    public void BuildLevel6()
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();

        RedZoneSpots.Add(15);
        RedZoneSpots.Add(gameState.gridManager.width + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 2 + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 3 + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 4 + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 5 + 15);
        //RedZoneSpots.Add(gameState.gridManager.width * 6 + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 8 + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 9 + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 10 + 15);
        RedZoneSpots.Add(gameState.gridManager.width * 11 + 15);

        //RedZoneSpots.Add(30);
        //RedZoneSpots.Add(gameState.gridManager.width + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 2 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 3 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 4 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 5 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 6 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 7 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 8 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 9 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 10 + 30);
        RedZoneSpots.Add(gameState.gridManager.width * 11 + 30);

        SetCoinPosition(gameState.gridManager.width * 6 + 15);
        SetCoinPosition(gameState.gridManager.width * 10 + 25);
        SetCoinPosition(gameState.gridManager.width + 39);


        RedZoneSpots.Add(gameState.gridManager.width * 9 - 3);
        RedZoneSpots.Add(gameState.gridManager.width * 10 - 3);

        RedZoneSpots.Add(gameState.gridManager.width * 9 - 2);
        RedZoneSpots.Add(gameState.gridManager.width * 9 - 1);
        SetfinishPosition(gameState.gridManager.width * 11 - 1);
        rechthoekSpots.Add(gameState.gridManager.width * 12 - 2);

        //dit moet later anders zijn collectables 

        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);
        gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
        initRedZones(RedZoneSpots);
    }

    public void BuildTutorial()
    {
        gameState.UIManager.canvas.transform.GetChild(7).gameObject.SetActive(false);

        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();
        RampSpots.Add(8);
        //rechthoekSpots.Add(16);
        rechthoekSpots.Add(20);
        rechthoekSpots.Add(32);
        //rechthoekSpots.Add(34);
        rechthoekSpots.Add(36);
        RampSpotsReversed.Add(30);

        SetCoinPosition(11);
        SetCoinPosition(26);
        SetCoinPosition(29);
        SetfinishPosition(24);

        //dit moet later anders zijn collectables 

        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);
        gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
        initRedZones(RedZoneSpots);
    }

    internal void Build_Vertical_Slice_Level6()
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();
        RampSpots.Add(21);
        lift.SetStartAndEndPoints(43, 85);
        liftList.Add(lift);
        //RampSpots.Add(43);
        //RampSpots.Add(65);
        //RampSpots.Add(87);
        RampSpots.Add(109);
        //RampSpots.Add(131);
        //RampSpots.Add(153);
        //RampSpots.Add(175);
        PlatformSpots.Add(197);
        PlatformSpots.Add(198);

        SetCoinPosition(67);
        SetCoinPosition(90);
        SetCoinPosition(115);
        SetfinishPosition(178);

        //dit moet later anders zijn collectables 



        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);

        gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
        initRedZones(RedZoneSpots);
    }

    internal void Build_Vertical_Slice_LevelAnne()
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();
        RampSpots.Add(20);
        TrampolineSpots.Add(124);

        //dit moet later anders zijn collectables 



        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);

        for (int i = 0; i < CoinSpots.Count; i++)
        {
            coin = Instantiate(coin, gameState.gridManager.gridSquares[CoinSpots[i]] + rampAdjustment, new Quaternion(0, 0, 0, 0));

        }

        for (int i = 0; i < FinishSpots.Count; i++)
        {
            finish = Instantiate(finish, gameState.gridManager.gridSquares[FinishSpots[i]], new Quaternion(0, 0, 0, 0));

        }
        initRedZones(RedZoneSpots);
    }

    internal void Build_Vertical_Slice_Level5()
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();

        RampSpots.Add(21);
        TrampolineSpots.Add(144);
        CoinSpots.Add(107);
        RedZoneSpots.Add(5);
        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);

        for (int i = 0; i < CoinSpots.Count; i++)
        {
            coin = Instantiate(coin, gameState.gridManager.gridSquares[CoinSpots[i]] + rampAdjustment, new Quaternion(0, 0, 0, 0));

        }

        initRedZones(RedZoneSpots);
    }

    internal void Build_Vertical_Slice_LevelBoost()
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();
        RampSpots.Add(21);
        boosterPlatformSpots.Add(124);

        RedZoneSpots.Add(127);
        RedZoneSpots.Add(128);
        RedZoneSpots.Add(129);
        RedZoneSpots.Add(130);

        RedZoneSpots.Add(68);
        RedZoneSpots.Add(69);
        RedZoneSpots.Add(70);

        //dit moet later anders zijn collectables 
        PlatformSpots.Add(208);
        PlatformSpots.Add(209);
        PlatformSpots.Add(207);

        SetCoinPosition(104);
        SetCoinPosition(90);
        SetCoinPosition(190);
        SetfinishPosition(188);

        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);
        gameState.collectableManager.InitCollectables(coinPositions, finishPosition);

        //for (int i = 0; i < CoinSpots.Count; i++)
        //{
        //    coin = Instantiate(coin, gameState.gridManager.gridSquares[CoinSpots[i]] + rampAdjustment, new Quaternion(0, 0, 0, 0));

        //}

        //for (int i = 0; i < FinishSpots.Count; i++)
        //{
        //    finish = Instantiate(finish, gameState.gridManager.gridSquares[FinishSpots[i]], new Quaternion(0, 0, 0, 0));

        //}
        initRedZones(RedZoneSpots);
    }

    public void BuildLevelCoen()
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();


        RampSpots.Add(21);

        //PlatformSpots.Add(19);
        //PlatformSpots.Add(128);
        PlatformSpots.Add(129);
        PlatformSpots.Add(130);
        //PlatformSpots.Add(138);
        PlatformSpots.Add(214);
        PlatformSpots.Add(215);
        PlatformSpots.Add(216);
        PlatformSpots.Add(274);
        PlatformSpots.Add(275);

        PlatformSpots.Add(94);
        PlatformSpots.Add(95);

        PortalSpots.Add(131);
        PortalSpots.Add(35);

        RedZoneSpots.Add(13);
        RedZoneSpots.Add(33);
        RedZoneSpots.Add(53);
        RedZoneSpots.Add(73);

        RedZoneSpots.Add(68);
        RedZoneSpots.Add(69);
        RedZoneSpots.Add(70);

        RedZoneSpots.Add(93);
        RedZoneSpots.Add(113);
        RedZoneSpots.Add(133);
        RedZoneSpots.Add(153);
        RedZoneSpots.Add(173);
        RedZoneSpots.Add(193);
        RedZoneSpots.Add(213);
        RedZoneSpots.Add(233);
        RedZoneSpots.Add(253);

        SetCoinPosition(110);
        SetCoinPosition(138);
        SetCoinPosition(196);
        SetfinishPosition(254);

        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);
        gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
        initRedZones(RedZoneSpots);
    }

    internal void Init_Platforms(List<int> RampSpots, List<int> PlatformSpots, List<int> RampSpotsReversed, List<int> PortalSpots, List<int> TrampolineSpots, List<int> rechthoekSpots, List<int> boosterPlatformSpots, List<Lift> liftList)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        if (RampSpots.Count > 0)
        {
            for (int i = 0; i < RampSpots.Count; i++)
            {
                ramp.SpawnRamp(gameState.gridManager.gridSquares[RampSpots[i]] + new Vector3(.5f, 0, 0));
                gameState.gridManager.AddFilledGridSpots(RampSpots, SizeType.twoByOne);
            }
        }
        if (PlatformSpots.Count > 0)
        {
            for (int i = 0; i < PlatformSpots.Count; i++)
            {
                Instantiate(PlatformSquare, gameState.gridManager.gridSquares[PlatformSpots[i]] + rampAdjustment, PlatformSquare.transform.rotation);
                gameState.gridManager.AddFilledGridSpots(PlatformSpots, SizeType.oneByOne);
            }
        }
        if (RampSpotsReversed.Count > 0)
        {
            for (int i = 0; i < RampSpotsReversed.Count; i++)
            {
                ramp.SpawnRampReversed(gameState.gridManager.gridSquares[RampSpotsReversed[i]] + rampAdjustment);
                gameState.gridManager.AddFilledGridSpots(RampSpotsReversed, SizeType.twoByOne);
            }
        }
        if (PortalSpots.Count > 0)
        {
            for (int i = 0; i < PortalSpots.Count; i++)
            {
                portal = Instantiate(portal, gameState.gridManager.gridSquares[PortalSpots[i]] + new Vector3(1, .5f, 0), new Quaternion(0, 0, 0, 0));
                allPortals.Add(portal);
                gameState.gridManager.AddFilledGridSpots(PortalSpots, SizeType.twoByTwo);
            }
        }
        if (TrampolineSpots.Count > 0)
        {
            for (int i = 0; i < TrampolineSpots.Count; i++)
            {
                trampoline = Instantiate(trampoline, gameState.gridManager.gridSquares[TrampolineSpots[i]] + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0));
                gameState.gridManager.AddFilledGridSpots(TrampolineSpots, SizeType.twoByOne);
            }
        }
        if (rechthoekSpots.Count > 0)
        {
            for (int i = 0; i < rechthoekSpots.Count; i++)
            {
                rechthoek = Instantiate(rechthoek, gameState.gridManager.gridSquares[rechthoekSpots[i]] + new Vector3(1, 0, 0), rechthoek.transform.rotation);
                gameState.gridManager.AddFilledGridSpots(rechthoekSpots, SizeType.twoByOne);
            }
        }
        if (boosterPlatformSpots.Count > 0)
        {
            for (int i = 0; i < boosterPlatformSpots.Count; i++)
            {
                boostPlatform = Instantiate(boostPlatform, gameState.gridManager.gridSquares[boosterPlatformSpots[i]] + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0));
                gameState.gridManager.AddFilledGridSpots(boosterPlatformSpots, SizeType.twoByOne);
            }
        }
        if (liftList.Count > 0)
        {
            for (int i = 0; i < liftList.Count; i++)
            {
                lift = Instantiate(liftList[i]);
                lift.transform.position = lift.startPoint;
            }

        }
    }

    public void initRedZones(List<int> redZones)
    {
        if (redZones.Count > 0)
        {
            for (int i = 0; i < redZones.Count; i++)
            {
                Instantiate(redZone, gameState.gridManager.gridSquares[redZones[i]] + new Vector3(.5f, 0, 0), new Quaternion(0, 0, 0, 0));
                gameState.gridManager.AddFilledGridSpots(redZones, SizeType.oneByOne);
            }
        }
    }
    //public void initBoostPlatforms(List<int> boosterPlatformSpots) 
    //{
    //    if (boosterPlatformSpots.Count > 0)
    //    {
    //        for (int i = 0; i < boosterPlatformSpots.Count; i++)
    //        {
    //            boostPlatform = Instantiate(boostPlatform, gameState.gridManager.gridSquares[boosterPlatformSpots[i]] + new Vector3(1, 0, 0), new Quaternion(0, 0, 0, 0));

    //        }
    //    }
    //}

    internal void Build_Vertical_Slice_Level7()
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        List<int> RampSpots = new List<int>();
        List<int> PlatformSpots = new List<int>();
        List<int> FinishSpots = new List<int>();
        List<int> TrampolineSpots = new List<int>();
        List<int> PortalSpots = new List<int>();
        List<int> rechthoekSpots = new List<int>();
        List<int> RampSpotsReversed = new List<int>();
        List<int> CoinSpots = new List<int>();
        List<int> RedZoneSpots = new List<int>();
        List<int> boosterPlatformSpots = new List<int>();
        List<Lift> liftList = new List<Lift>();
        RampSpots.Add(80);
        PortalSpots.Add(91);
        PortalSpots.Add(34);
        TrampolineSpots.Add(174);
        TrampolineSpots.Add(211);
        PlatformSpots.Add(108);
        PlatformSpots.Add(109);
        RedZoneSpots.Add(7);
        RedZoneSpots.Add(27);
        RedZoneSpots.Add(47);
        RedZoneSpots.Add(67);
        RedZoneSpots.Add(87);
        RedZoneSpots.Add(107);
        RedZoneSpots.Add(127);
        RedZoneSpots.Add(147);
        RedZoneSpots.Add(148);
        RedZoneSpots.Add(149);
        RedZoneSpots.Add(150);

        SetCoinPosition(137);
        SetCoinPosition(123);
        SetCoinPosition(187);

        SetfinishPosition(88);


        //dit moet later anders zijn collectables 



        Init_Platforms(RampSpots, PlatformSpots, RampSpotsReversed, PortalSpots, TrampolineSpots, rechthoekSpots, boosterPlatformSpots, liftList);
        gameState.collectableManager.InitCollectables(coinPositions, finishPosition);
        initRedZones(RedZoneSpots);
    }


}
