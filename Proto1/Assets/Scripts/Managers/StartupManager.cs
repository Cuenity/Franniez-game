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
        PlayerDataController.instance.MakeNewPlayer();
        SceneManager.LoadScene("StartMenu");
    }
}

