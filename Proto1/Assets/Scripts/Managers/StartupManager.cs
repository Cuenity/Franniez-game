using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartupManager : MonoBehaviour
{
    public PlayerDataController dataController;

    public void Start()
    {

    }

    public void OnEnable()
    {
        LocalizationManager.ClickedButton += ButtonClicked;
    }

    public void ButtonClicked()
    {
        PlayerData player = new PlayerData();
        player.name = "Player";
        player.language = (int)LocalizationManager.instance.LanguageChoice;
        PlayerDataController.instance.SetPlayer(player);
        PlayerDataController.instance.Save();

        SceneManager.LoadScene("StartMenu");
    }
}

