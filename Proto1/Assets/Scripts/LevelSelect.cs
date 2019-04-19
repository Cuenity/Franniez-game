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
    Text stickersCollectedText;
    [SerializeField]
    Text stickersCollectedTextWorld2;
    [SerializeField]
    Text stickersCollectedTextWorld3;
    

    int stickersCollectedCount;
    List<Button> LevelSelectButtons = new List<Button>();
    Player player;

    // Start is called before the first frame update
    void Start()
    {
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
        stickersCollectedText.text = "Stickers:"+stickersCollectedCount.ToString()+"/10";
        stickersCollectedTextWorld2.text = "Stickers Needed:" + stickersCollectedCount.ToString() + "/5";
        stickersCollectedTextWorld3.text = "Stickers Needed:" + stickersCollectedCount.ToString() + "/10";
    }

    // Update is called once per frame
    void Update()
    {

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

        }
    }

    public void LaadLevel(string level)
    {
        //hardcoded fix voor het feit dat er geen levels boven 4 bestaan
        if(Convert.ToInt32(level) > 4)
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
        SceneSwitcher.Instance.AsynchronousLoadStart("StartMenu");
    }

    public void BackToWorldSelect()
    {
        worldSelectCanvas.gameObject.SetActive(true);
        levelSelectCanvas.gameObject.SetActive(false);
    }
}
