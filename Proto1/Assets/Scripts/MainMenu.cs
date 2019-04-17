using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using GameAnalyticsSDK;
using GameAnalyticsSDK.Events;

public class MainMenu : MonoBehaviour
{
   
    public Canvas StartMenuCanvas;
    public Canvas SettingsCanvas;
    public Canvas LanguageCanvas;
    public Canvas LevelSelect;
    public GameObject NormalSettings;

    private bool Sound;
    private Player player;
    private int volumeOnOf;

    public delegate void ClickAction();
    public static event ClickAction ChangedSound;
    public static event ClickAction ChangeLanguage;

    // Start is called before the first frame update

    public void Start()
    {
        SetCanvasDisable();

        // Onthouden in playerprefeb
        volumeOnOf = PlayerPrefs.GetInt("Sound");
        if (volumeOnOf <= 1)
        {
            Sound = true;
        }
        else
        {
            Sound = false;
            AudioListener.volume = 0f;
        }

        GameAnalytics.Initialize();
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, "MainMenu");


    }

    public void StartGame()
    {
        Debug.Log("Start Game");

        //SceneManager.sceneLoaded += SceneIsLoaded;
        SceneManager.LoadScene("1");
        //GameState.Instance.levelManager.AsynchronousLoadStart("1");
    }

    //private void SceneIsLoaded(Scene arg0, LoadSceneMode arg1)
    //{
    //    GameState.Instance.levelManager.InitScene(arg0.name);
    //}

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Exit game");
            Application.Quit();
        }
    }

    public void Button_GoToSettings()
    {
        // Change Canvas to Setting Canvas and disable StartMenu Canvas
        StartMenuCanvas.GetComponent<Canvas>().enabled = false;
        SettingsCanvas.GetComponent<Canvas>().enabled = true;
    }

    private void SetCanvasDisable()
    {
        // Set all Canvasses on Disabled when the Scene loaded
        SettingsCanvas.GetComponent<Canvas>().enabled = false;
        LanguageCanvas.GetComponent<Canvas>().enabled = false;
        
    }

    public void Button_ReturnMenu()
    {
        // Return to Start Menu Canvas
        StartMenuCanvas.GetComponent<Canvas>().enabled = true;
        SettingsCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void Button_ReturnSettings()
    {
        LanguageCanvas.GetComponent<Canvas>().enabled = false;
        SettingsCanvas.GetComponent<Canvas>().enabled = true;
    }

    public void Button_ChangeSound()
    {
        // Check if the user already silenced the music
        volumeOnOf = PlayerPrefs.GetInt("Sound");
        if (volumeOnOf == 1)
        {
            PlayerPrefs.SetInt("Sound", 2);
            AudioListener.volume = 0f;
        }
        else
        {
            PlayerPrefs.SetInt("Sound", 1);
            AudioListener.volume = 1f;
        }

        // Eventlistner for changing Sound Button Image
        ChangedSound();
    }

    public void Button_ChangeLanguage(int language)
    {

        PlayerDataController.instance.SetLanguage(language);
        PlayerDataController.instance.Save();
        LocalizationManager.instance.GetLanguageSettings();
        ChangeLanguage();
    }

    public void Button_GetLanguageCanvas()
    {
        SettingsCanvas.GetComponent<Canvas>().enabled = false;
        LanguageCanvas.GetComponent<Canvas>().enabled = true;
    }
}
