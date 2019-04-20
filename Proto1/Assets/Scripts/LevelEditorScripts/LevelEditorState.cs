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
    
    LevelEditorBall levelEditorBall;

    [SerializeField]
    public LevelEditorUIManager UIManager;
    [SerializeField]
    public LevelEditorPlatforms playerPlatforms;

    [SerializeField]
    public int width;
    [SerializeField]
    public int heigth;

    //opslagplek voor levelEigenschappen
    [SerializeField]
    public LevelPlatformen levelPlatformen = new LevelPlatformen();
    
    private static LevelEditorState instance;
    public static LevelEditorState Instance
    {
        get { return instance; }
    }

    //Now the earth was formless and empty, 
    //darkness was over the surface of the deep, and the Spirit of God was hovering over the waters.
    void Start()
    {
        instance = this;
        playerPlatforms = new LevelEditorPlatforms(1,999, 999, 999, 999, 999,999);
        UIManager.InventoryButtons(playerPlatforms);
        // And God said, “Let there be light,” and there was light.  God saw that the light was good, and he separated the light from the darkness. 
            
        // God called the light “day,” and the darkness he called “night.” And there was evening, and there was morning—the first day.
        //ReadLevelsFromText("Level1");
    }
    
    void Update()
    {
        
    }

    public void SpawnFreshGrid()
    {
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
        levelEditorBall = GameObject.FindGameObjectWithTag("Player").GetComponent<LevelEditorBall>();
        levelEditorBall.Roll();
    }
}
