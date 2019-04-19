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

    List<Button> LevelSelectButtons = new List<Button>();

    // Start is called before the first frame update
    void Start()
    {
        levelSelectCanvas.gameObject.SetActive(false);
        //DIT IS NIET ZUIVER REKEN HIER NIET OP 
        //maar je krijgt wel al je buttons in een lijstje zonder dat je 10 buttons moet declareren en slepen in editor
        //MAAR HET IS NIET ZUIVER JE KRIJGT OOK BACK BUTTON ENZO HOU HIER REKENING MEE
        foreach (var button in levelSelectCanvas.GetComponentsInChildren<Button>())
        {
            LevelSelectButtons.Add(button);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LoadCorrectPicturesForLevels()
    {
        Player player = PlayerDataController.instance.player;
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
}
