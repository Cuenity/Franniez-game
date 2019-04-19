using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class VictoryManager : MonoBehaviour
{
    public Image ribbon;
    public Image starImage;
    public Image sticker;

    public Sprite star1;
    public Sprite stars2;
    public Sprite stars3;
    public Sprite stars0;

    public Sprite ribbon_Dutch;
    public Sprite ribbon_English;
    public Sprite ribbon_Spanish;

    private Player player;


    private void Awake()
    {
        player = PlayerDataController.instance.player;
        SetLanguage();
        sticker.enabled = false;
        GetData();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetLanguage()
    {
        LocalizationManager.instance.SetLanguage();
        Language language = LocalizationManager.instance.GetLanguage();

        switch (LocalizationManager.instance.GetLanguage())
        {
            case Language.Dutch:
                ribbon.sprite = ribbon_Dutch;
                break;
            case Language.English:
                ribbon.sprite = ribbon_English;
                break;
            case Language.Spanish:
                ribbon.sprite = ribbon_Spanish;
                break;
        }
    }

    private void GetData()
    {
        //ok? hij doet dit 2 keer
        //Onderstaande code toepassen wanneer Anne klaar is met Gamestate
        int previousScene = PlayerDataController.instance.previousScene;

        player = PlayerDataController.instance.player;
        //hij pakt nu de level zoals het is opgeslagen (dus niet wat daadwerkelijk is behaald)
        Level level = player.levels[previousScene - 1];

        //nu pakt hij de coincount van previousscene die wordt opgeslagen bij het einde van previousSceneCoinCount
        
        switch (PlayerDataController.instance.previousSceneCoinCount)
        {
            case 1:
                starImage.sprite = star1;
                break;
            case 2:
                starImage.sprite = stars2;
                break;
            case 3:
                starImage.sprite = stars3;
                break;
            default:
                starImage.sprite = stars0;
                break;
        }

        if (level.gotSticker)
        {
            sticker.enabled = true;
        }
    }

    public void Restart()
    {
        //previous level
        string prevlvl = Convert.ToString(PlayerDataController.instance.previousScene);

        SceneSwitcher.Instance.AsynchronousLoadStart(prevlvl);

        //SceneManager.LoadScene(prevlvl);
    }

    public void NextScene()
    {
        //previous level + 1
        int prevlvl = PlayerDataController.instance.previousScene;
        string prevlvlString = Convert.ToString(prevlvl + 1);
        SceneSwitcher.Instance.AsynchronousLoadStart(prevlvlString);
        //SceneManager.LoadScene(prevlvlString);
    }

    public void ReturnToMenu()
    {
        //moet lvl select worden
        //SceneManager.LoadScene("StartMenu");
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("LevelSelect");
    }
}
