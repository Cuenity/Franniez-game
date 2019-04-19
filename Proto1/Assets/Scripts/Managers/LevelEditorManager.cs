using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorManager : MonoBehaviour
{
    LevelPlatformen levelPlatformen = new LevelPlatformen();
    public int width;
    public int heigth;
    public List<Vector3> gridSquares = new List<Vector3>();

    //Alle vieze gameobjects
    [SerializeField]
    GameObject ramp;
    [SerializeField]
    GameObject platform;
    //Invetory variablen
    public PlayerPlatforms playerPlatforms;
    public InventoryButton inventoryButton;
    public InventoryButton[] instantiatedInventoryButtons;
    public Sprite rampImage;
    public Sprite platformSquareImage;
    public Sprite trampolineImage;
    public Sprite boostPlatformImage;

    // Start is called before the first frame update
    void Start()
    {
        playerPlatforms = new PlayerPlatforms(999, 999, 999, 999, 999);
        MakeInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ReadLevelsFromText(string levelName)
    {
        if (levelName == "")
        {
            InputField InputName = GameObject.Find("LevelName").GetComponent<InputField>();
            levelName = InputName.text;
        }
        string filePath = Application.streamingAssetsPath + "/" + levelName + ".json";

        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            levelPlatformen = JsonUtility.FromJson<LevelPlatformen>(dataAsJSON);
        }
        else
        {
            //moet file createn
            Debug.LogError("Cannot find file!");
        }
        BuildLevelFromText(levelPlatformen) ;
    }
    public void SaveLevelToText(string levelName)
    {

        if (levelName == "")
        {
            InputField InputName = GameObject.Find("LevelName").GetComponent<InputField>();
            levelName = InputName.text;
        }
        string filePath = Application.streamingAssetsPath + "/" + levelName + ".json";
        if (!string.IsNullOrEmpty(filePath))
        {
            string dataAsJson = JsonUtility.ToJson(levelPlatformen);
            File.WriteAllText(filePath, dataAsJson);
        }
    }
    public void Build_Grid_Level_Editor_Without_Visuals()
    {
        Vector3 moveRight = new Vector3(1f, 0f, 0f);
        Vector3 moveDown = new Vector3(0f, -1f, 0f);
        Vector3 gridStartingPoint = new Vector3(0f, 0f, 0f);

        InputField widthObject = GameObject.Find("Width").GetComponent<InputField>();
        InputField heigthObject = GameObject.Find("Heigth").GetComponent<InputField>();

        if (widthObject.text != "" && heigthObject.text != "")
        {
            width = Convert.ToInt32(widthObject.text);
            heigth = Convert.ToInt32(heigthObject.text);
        }


        for (int i = 0; i < heigth; i++)
        {
            gridStartingPoint.x = 0f;
            gridStartingPoint = gridStartingPoint + moveDown;

            for (int i2 = 0; i2 < width; i2++)
            {
                gridSquares.Add(gridStartingPoint);
                //place 2 the left
                gridStartingPoint = gridStartingPoint + moveRight;

            }
        }
        levelPlatformen.tileList = new int[heigth * width];
    }
    internal void BuildLevelFromText(LevelPlatformen levelPlatformen)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        for (int i = 0; i < levelPlatformen.tileList.Length; i++)
        {
            //if (levelPlatformen.tileList[i] == 0)
            //{

            //}
            //else if (levelPlatformen.tileList[i] == 1)
            //{
            //    ramp.SpawnRamp(gridSquares[i]);
            //}
            //else if (levelPlatformen.tileList[i] == 2)
            //{
            //    ramp.SpawnRampReversed(gridSquares[i]);
            //}
            //else if (levelPlatformen.tileList[i] == 3)
            //{

            //    platformSquare = Instantiate(PlatformSquare, gameState.gridManager.gridSquares[i] + rampAdjustment, new Quaternion(0, 0, 0, 0));
            //    PlatformSquare.transform.Rotate(new Vector3(-90f, -90f, 0));
            //}
        }
    }

    //code voor het bouwen van de bouw balk
    internal void MakeInventory()
    {
        PlayerPlatforms platforms = playerPlatforms;
        InventoryButtons(platforms); // InventoryButtons moet meegeven welke platformen en hoeveel van elk, elk verschillend type krijgt één knop met daarin een platform (als afbeelding of wat dan ook) met het aantal weergegeven.
        foreach (var placedPlatform in playerPlatforms.placedPlatforms)
        {
            placedPlatform.AddComponent<PlatformDragManager>();
            placedPlatform.GetComponent<Outline>().enabled = true;
            if (placedPlatform.gameObject.transform.childCount > 0)
            {
                placedPlatform.gameObject.transform.GetChild(0).gameObject.SetActive(true);
            }
            //placedPlatform.GetComponent<PlatformDragManager>().enabled = true;
        }
    }
    public void InventoryButtons(PlayerPlatforms playerPlatforms)
    {
        InstantiateInventoryButtonsCheck(playerPlatforms.inventoryButtonAmmount);

        //bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons[0] == null;
        if (instantiatedInventoryButtons[0] == null)
        {
            GameObject uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");

            int buttonDistance = Screen.width / (playerPlatforms.inventoryButtonAmmount + 1);
            int buttonHeight = Screen.height / 8;

            for (int i = 0; i < playerPlatforms.inventoryButtonAmmount; i++)
            {
                InventoryButton buttonForWidth = Instantiate(inventoryButton);
                //buttonForWidth.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 8, Screen.width / 8);
                instantiatedInventoryButtons[i] = buttonForWidth;
                instantiatedInventoryButtons[i].transform.SetParent(uiCanvas.transform);

                ChangeInventoryButtonImageAndText(i, playerPlatforms);

                instantiatedInventoryButtons[i].transform.position = new Vector3(buttonDistance * (i + 1), buttonHeight, 0);
            }
        }

        else
        {
            foreach (InventoryButton buttonToActivate in instantiatedInventoryButtons)
            {
                buttonToActivate.gameObject.SetActive(true);
            }
        }
    }

    private void InstantiateInventoryButtonsCheck(int inventoryButtonAmmount)
    {
        //bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons == null;
        bool check = instantiatedInventoryButtons.Length > 0;
        if (instantiatedInventoryButtons == null || !check)
        {
            instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
        }
    }

    public void RemoveInventoryButtons()
    {
        //instantiatedInventoryButtons = GameObject.FindGameObjectsWithTag("InventoryButton");
        if (instantiatedInventoryButtons != null)
        {
            foreach (InventoryButton buttonToDeactivate in instantiatedInventoryButtons)
            {
                buttonToDeactivate.gameObject.SetActive(false);
            }
        }
    }

    public void ChangeInventoryButtonImageAndText(int currentButton, PlayerPlatforms playerPlatforms)
    {
        GameObject buttonImage = instantiatedInventoryButtons[currentButton].transform.GetChild(0).gameObject;
        GameObject buttonText = instantiatedInventoryButtons[currentButton].transform.GetChild(3).gameObject;

        if (playerPlatforms.ramps > 0 && !playerPlatforms.rampButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = rampImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.rampInventoryButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.ramps.ToString();

            playerPlatforms.rampButtonInstantiated = true;
        }
        else if (playerPlatforms.platformSquares > 0 && !playerPlatforms.platformSquaresButtonInstantated)
        {
            buttonImage.GetComponent<Image>().sprite = platformSquareImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.platformSquareButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.platformSquares.ToString();

            playerPlatforms.platformSquaresButtonInstantated = true;
        }
        else if (playerPlatforms.trampolines > 0 && !playerPlatforms.trampolineButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = trampolineImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.trampolineButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.trampolines.ToString();

            playerPlatforms.trampolineButtonInstantiated = true;
        }
        else if (playerPlatforms.boostPlatforms > 0 && !playerPlatforms.boostPlatformButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = boostPlatformImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.boostPlatformButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.boostPlatforms.ToString();

            playerPlatforms.trampolineButtonInstantiated = true;
        }
    }
}
