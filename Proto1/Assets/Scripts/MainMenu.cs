using UnityEngine;

public class MainMenu : MonoBehaviour
{
    /*  
        Main Menu is het eerste scherm wat de speler te zien krijgt. Wanneer de speler het spel voor het eerst opstart
        krijgt hij eerst de taal keuze scherm te zien.
    */

    // Serialize fields
    [SerializeField]
    private Canvas StartMenuCanvas, SettingsCanvas, LanguageCanvas;

    // Private properties
    private bool Sound;
    private bool Vibration;
    private PlayerData player;
    private int volumeOnOf;
    private int vibrateOf;

    // Event triggers for changing the Language and/or Sound in the setting Canvas
    public delegate void ClickAction();
    public event ClickAction ChangedSound;
    public event ClickAction ChangeLanguage;
    public event ClickAction ChangedVibration;

    public void Start()
    {
        // Only show the Main canvas
        SetCanvasDisable();

        // Get the Sound setting from PlayerPrefs
        volumeOnOf = PlayerPrefs.GetInt("Sound");
        if (volumeOnOf <= 1)
        {
            // Set volume on
            Sound = true;
            PlayerPrefs.SetInt("Sound", 1);
        }
        else
        {
            // Set volume off
            Sound = false;
            AudioListener.volume = 0f;
        }

        // Get the Sound setting from PlayerPrefs
        volumeOnOf = PlayerPrefs.GetInt("Vibration");
        if (volumeOnOf <= 1)
        {
            // Set volume on
            Vibration = true;
            PlayerPrefs.SetInt("Vibration", 1);
        }
        else
        {
            // Set volume off
            Vibration = false;
            AudioListener.volume = 0f;
        }

        // Test kut coins
        string coins = PlayerDataController.instance.Player.ShopCoins.ToString();
        //Debug.Log(coins);
        int coins2 = PlayerDataController.instance.ReturnCoins();
        //Debug.Log(coins2);

    }

    // Button Action: Go to Level Select Scene
    public void Button_StartGame()
    {
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("LevelSelect");
    }


    // In Start Method this functions get called for disabling Settings- and Languagecanvas
    private void SetCanvasDisable()
    {
        // Set all Canvasses on Disabled when the Scene loaded
        SettingsCanvas.GetComponent<Canvas>().enabled = false;
        LanguageCanvas.GetComponent<Canvas>().enabled = false;
    }

    public void Button_StartMultiplayer()
    {
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("MultiplayerStart");
    }

    #region Button Actions

    // Button Action: Set Setting Canvas on Active
    public void Button_GoToSettings()
    {
        // Change Canvas to Settings Canvas and disable StartMenu Canvas
        StartMenuCanvas.GetComponent<Canvas>().enabled = false;
        SettingsCanvas.GetComponent<Canvas>().enabled = true;
    }

    // Button Action: Close Settings canvas
    public void Button_ReturnMenu()
    {
        // Return to Start Menu Canvas
        StartMenuCanvas.GetComponent<Canvas>().enabled = true;
        SettingsCanvas.GetComponent<Canvas>().enabled = false;
    }

    // Button Action: Close Language canvas
    public void Button_ReturnSettings()
    {
        // Return to Settings Canvas
        LanguageCanvas.GetComponent<Canvas>().enabled = false;
        SettingsCanvas.GetComponent<Canvas>().enabled = true;
    }

    // Button Action: Switch between on and off for sound
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

    // Button Action: Change Language
    public void Button_ChangeLanguage(int language)
    {
        // Set new language for player
        PlayerDataController.instance.SetLanguage(language);

        // Save the settings to the player file
        PlayerDataController.instance.Save();

        // Get language data for game
        LocalizationManager.instance.GetLanguageSettings();

        /*  
            Trigger Event Listneners:
                ChangeTextColor - ChangeColorTextLanguage.cs
                ChangeFlag - LanguageSettingsImages.cs
                RefreshText - LocalizedText.cs
        */
        ChangeLanguage();
    }

    // Button Action: Set LanguageCanvas on active
    public void Button_GetLanguageCanvas()
    {
        SettingsCanvas.GetComponent<Canvas>().enabled = false;
        LanguageCanvas.GetComponent<Canvas>().enabled = true;
    }

    // Button Action: Go to Shop Scene
    public void Button_GoToShop()
    {
        SceneSwitcher.Instance.AsynchronousLoadStartNoLoadingBar("Shop");
    }

    public void Button_ChangeVibration()
    {
        // Check if the user already silenced the music
        vibrateOf = PlayerPrefs.GetInt("Vibration");
        if (vibrateOf == 1)
        {
            PlayerPrefs.SetInt("Vibration", 2);
        }
        else
        {
            PlayerPrefs.SetInt("Vibration", 1);
        }

        // Eventlistner for changing Sound Button Image
        ChangedVibration();

    }

    #endregion
}
