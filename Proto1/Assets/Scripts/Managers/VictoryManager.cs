using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
        PlayerDataController.instance.Save();
        PlayerDataController.instance.Load();
        player = PlayerDataController.instance.player;
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
        // Onderstaande code toepassen wanneer Anne klaar is met Gamestate
        //int previousScene = GameState.Instance.previousScene;
        int previousScene = 1;

        Level level = player.levels[previousScene - 1];

        switch (level.countCoins)
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

        if(level.gotSticker)
        {
            sticker.enabled = true;
        }

    }

    public void Restart()
    {
        SceneManager.LoadScene("");
    }

    public void NextScene()
    {
        SceneManager.LoadScene("");
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("StartMenu");
    }
}
