using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    public PlayerDataController dataController;
    // Start is called before the first frame update
    public void Start()
    {
       //while(!LocalizationManager.instance.GetisReady())
       // {
       //     // Wacht 1 frame
       //     yield return null;
       // }
    }

    public void OnEnable()
    {
        LocalizationManager.ClickedButton += ButtonClicked;
    }

    public void ButtonClicked()
    {
        Player player = new Player();
        player.name = "";
        player.language = (int)LocalizationManager.instance.LanguageChoice;
        PlayerDataController.instance.player = player;

        SceneManager.LoadScene("StartMenu");
    }
}

