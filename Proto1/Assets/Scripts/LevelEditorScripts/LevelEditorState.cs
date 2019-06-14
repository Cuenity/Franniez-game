using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorState : MonoBehaviour
{

    [SerializeField]
    public LevelEditorGridManager gridManager;
    [SerializeField]
    public LevelEditorPlatformManager platformManager;
    [SerializeField]
    public LevelEditorBall levelEditorBall;

    [SerializeField]
    public LevelEditorUIManager UIManager;
    [SerializeField]
    public LevelEditorPlatforms playerPlatforms;

    [SerializeField]
    public int width;
    [SerializeField]
    public int heigth;
    [SerializeField]
    public int SpawnBallPosition;
    [SerializeField]
    public Text Instructies;

    //opslagplek voor levelEigenschappen
    [SerializeField]
    public LevelPlatformen levelPlatformen = new LevelPlatformen();
    
    private static LevelEditorState instance;
    public static LevelEditorState Instance
    {
        get { return instance; }
    }
    
    void Start()
    {
        instance = this;
        playerPlatforms = new LevelEditorPlatforms(999,999, 999, 999, 999, 999,999,999);
        UIManager.InventoryButtons(playerPlatforms);
        //ReadLevelsFromText("Level1");
    }
    
 }

    public void SpawnFreshGrid()
    {
        string UserWidth = GameObject.Find("GridWidth").GetComponent<InputField>().text;
        string UserHeigth = GameObject.Find("GridHeigth").GetComponent<InputField>().text;
        if (UserWidth != "" && UserHeigth != "")
        {
            width = Convert.ToInt32(UserWidth);
            heigth = Convert.ToInt32(UserHeigth);
        }
        gridManager.Build_Grid_Fresh_With_Visuals(width,heigth);
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
        gridManager.Build_Grid_FromJSON_With_Visuals(levelPlatformen.width, levelPlatformen.heigth);
        platformManager.BuildLevelFromText(levelPlatformen);
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
    public void TestLevel()
    {
        //moet doen wat rollingphase starter ook doet en dat is nutteloze shit van die kankerplatforms afhalen
        //outline uit en 
        //leveleditorDragmanager verwijderen
        try
        {
            foreach (GameObject placedPlatform in playerPlatforms.placedPlatforms)
            {
                if (!placedPlatform.GetComponent<Cannon>())
                {
                    Destroy(placedPlatform.GetComponent<LevelEditorPlatformDragManager>());
                    placedPlatform.GetComponent<Outline>().enabled = false;
                    // voor welk platform wordt onderstaande code uitgevoerd?
                    if (placedPlatform.gameObject.transform.childCount > 0 && !placedPlatform.GetComponent<Cannon>())
                    {
                        placedPlatform.gameObject.transform.GetChild(0).gameObject.SetActive(false);
                    }
                }
                else if (placedPlatform.GetComponent<RedZone>())
                {
                    placedPlatform.GetComponent<MeshCollider>().isTrigger = false;
                }
                else if (!placedPlatform.name.Contains("Ramp"))
                {
                    Destroy(placedPlatform.transform.GetChild(0).GetComponent<LevelEditorPlatformDragManager>());
                }

                else
                {
                    Destroy(placedPlatform.transform.GetChild(0).transform.GetChild(0).GetComponent<LevelEditorPlatformDragManager>());
                    Destroy(placedPlatform.transform.GetChild(1).GetComponent<LevelEditorPlatformDragManager>());
                    Destroy(placedPlatform.transform.GetChild(2).GetComponent<LevelEditorPlatformDragManager>());
                    Destroy(placedPlatform.transform.GetChild(3).GetComponent<LevelEditorPlatformDragManager>());
                    // hier nog de outline op false als dat er nog bij cannon bij komt
                }

            }
        }
        catch
        {

        }
        levelEditorBall.Roll();
        //Instantiate(levelEditorBall, gridManager.gridSquares[SpawnBallPosition], new Quaternion(0, 0, 0, 0)).Roll() ;
    }
    //
    public void BackToBuilding()
    {
       // InventoryButtons moet meegeven welke platformen en hoeveel van elk, elk verschillend type krijgt één knop met daarin een platform (als afbeelding of wat dan ook) met het aantal weergegeven.
        foreach (GameObject placedPlatform in playerPlatforms.placedPlatforms)
        {
            try
            { 
                if (!placedPlatform.GetComponent<Cannon>())
                {
                    placedPlatform.AddComponent<LevelEditorPlatformDragManager>();
                    placedPlatform.GetComponent<Outline>().enabled = true;
                    // voor welk platform wordt onderstaande code uitgevoerd?
                    if (placedPlatform.gameObject.transform.childCount > 0)
                    {
                        placedPlatform.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                    }
                }
                else
                {
                    placedPlatform.transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                    placedPlatform.transform.GetChild(1).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                    placedPlatform.transform.GetChild(2).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                    placedPlatform.transform.GetChild(3).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                    // hier nog de outline op true als dat er nog bij cannon bij komt
                }
            }
            catch
            {

            }
            //placedPlatform.GetComponent<PlatformDragManager>().enabled = true;
        }
    }
    
    public void SetSpawnBallPosition()
    {
        InputField spawnballinput = GameObject.Find("BallPositionInput").GetComponent<InputField>() ;
        SpawnBallPosition = Convert.ToInt32(spawnballinput.text);
    }

    public void ToggleInstruction()
    {
        if (Instructies.gameObject.activeSelf)
        {
            Instructies.gameObject.SetActive(false);
        }
        else
        {
            Instructies.gameObject.SetActive(true);
        }
    }
}
