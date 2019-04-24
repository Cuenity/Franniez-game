using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour
{
    [SerializeField]
    Canvas worldSelectCanvas;

    [SerializeField]
    Canvas levelSelectCanvas;

    [SerializeField]
    Sprite stars0;
    [SerializeField]
    Sprite stars1;
    [SerializeField]
    Sprite stars2;
    [SerializeField]
    Sprite stars3;
    [SerializeField]
    Sprite stickerCollected;
    [SerializeField]
    Sprite levelCompleted;


    [SerializeField]
    Text stickersCollectedText;
    [SerializeField]
    Text stickersCollectedTextWorld2;
    [SerializeField]
    Text stickersCollectedTextWorld3;

    //Dit moet later weg is vieze oplossing voor viez probleem
    [SerializeField]
    Image stickerIndicationLevel1;
    [SerializeField]
    Image stickerIndicationLevel2;
    [SerializeField]
    Image stickerIndicationLevel3;
    [SerializeField]
    Image stickerIndicationLevel4;
    [SerializeField]
    Image stickerIndicationLevel5;
    [SerializeField]
    Image stickerIndicationLevel6;
    [SerializeField]
    Image stickerIndicationLevel7;
    [SerializeField]
    Image stickerIndicationLevel8;
    [SerializeField]
    Image stickerIndicationLevel9;
    [SerializeField]
    Image stickerIndicationLevel10;

    Image[] stickerIndicators;
    [SerializeField]
    Image[] completedIndicators;


    int stickersCollectedCount;
    List<Button> LevelSelectButtons = new List<Button>();
    PlayerData player;
    bool textIsUpdated = false;

    // Start is called before the first frame update
    void Start()
    {
        stickerIndicators = new Image[] { stickerIndicationLevel1, stickerIndicationLevel2, stickerIndicationLevel3, stickerIndicationLevel4, stickerIndicationLevel5, stickerIndicationLevel6, stickerIndicationLevel7, stickerIndicationLevel8, stickerIndicationLevel9, stickerIndicationLevel10 };
        levelSelectCanvas.gameObject.SetActive(false);
        
        player = PlayerDataController.instance.player;
        //DIT IS NIET ZUIVER REKEN HIER NIET OP 
        //maar je krijgt wel al je buttons in een lijstje zonder dat je 10 buttons moet declareren en slepen in editor
        //MAAR HET IS NIET ZUIVER JE KRIJGT OOK BACK BUTTON ENZO HOU HIER REKENING MEE
        foreach (var button in levelSelectCanvas.GetComponentsInChildren<Button>())
        {
            LevelSelectButtons.Add(button);
        }
        for (int i = 0; i < player.levels.Length; i++)
        {
            if (player.levels[i].gotSticker)
            {
                stickersCollectedCount++;
            }
        }
        
    }
    

    // Update is called once per frame
    void Update()
    {
        if (!textIsUpdated)
        {
            stickersCollectedText.text = stickersCollectedText.text +" " + stickersCollectedCount.ToString();
            stickersCollectedTextWorld2.text = stickersCollectedTextWorld2.text +" " +stickersCollectedCount.ToString() + "/5";
            stickersCollectedTextWorld3.text = stickersCollectedTextWorld3.text +" " +stickersCollectedCount.ToString() + "/10";
            textIsUpdated = true;
        }
    }

    void LoadCorrectPicturesForLevels()
    {
        for (int i = 0; i < 10; i++)
        {
            switch (player.levels[i].countCoins)
            {
                case (0):
                    LevelSelectButtons[i].image.sprite = stars0;
                    break;
                case (1):
                    LevelSelectButtons[i].image.sprite = stars1;
                    break;
                case (2):
                    LevelSelectButtons[i].image.sprite = stars2;
                    break;
                case (3):
                    LevelSelectButtons[i].image.sprite = stars3;
                    break;

                default:
                    LevelSelectButtons[i].image.sprite = stars0;
                    break;
            }
            switch (player.levels[i].gotSticker)
            {
                case (true):
                    stickerIndicators[i].sprite = stickerCollected;
                    break;
                case (false):
                    stickerIndicators[i].gameObject.SetActive(false);
                    break;
                default:
                    break;
            }
            switch (player.levels[i].completed)
            {
                case (true):
                    completedIndicators[i].sprite = levelCompleted;
                    break;
                case (false):
                    completedIndicators[i].gameObject.SetActive(false);
                    break;
                default:
                    break;
            }

        }
    }

    public void LaadLevel(string level)
    {
        //hardcoded fix voor het feit dat er geen levels boven 4 bestaan
        if(Convert.ToInt32(level) > 10)
        {
            level = "4";
        }
        worldSelectCanvas.gameObject.SetActive(false);
        levelSelectCanvas.gameObject.SetActive(false);
        SceneSwitcher.Instance.AsynchronousLoadStart(level);
    }
    public void ShowLevelSelect()
    {
        worldSelectCanvas.gameObject.SetActive(false);
        LoadCorrectPicturesForLevels();
        levelSelectCanvas.gameObject.SetActive(true);
        
    }

    public void BackToStartMenu()
    {
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("StartMenu");
    }

    public void BackToWorldSelect()
    {
        worldSelectCanvas.gameObject.SetActive(true);
        levelSelectCanvas.gameObject.SetActive(false);
    }
}
