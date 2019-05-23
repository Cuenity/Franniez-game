using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    // Localization Manager is a Singleton
    public static LocalizationManager instance;

    // Easy acces to get which language is been set
    public Language LanguageChoice { get; set; }

    // Private properties
    private Dictionary<string, string> localizedText;
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    // Trigger events for when the player chooses a Language when the game first starts up
    public delegate void ClickAction();
    public static event ClickAction ClickedButton;

    // Make Singleton
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (this != null)
        {
            Destroy(gameObject);
        }
    }

    // Set the language for the entire game
    public void GetLanguageSettings()
    {
        // Get Language from Player
        Language language = (Language)PlayerDataController.instance.Player.language;
        string filePath = "";

        switch (language)
        {
            case Language.Dutch:
                filePath = "localizedText_Nl.json";
                break;
            case Language.English:
                filePath = "localizedText_En.json";
                break;
            case Language.Spanish:
                filePath = "localizedText_Es.json";
                break;
        }

        // Load the text from the json file
        LoadLocalizedText(filePath);
    }

    // Method for loading localized text in Dictionary
    public void LoadLocalizedText(string filename)
    {
        // Make a new dictionary, this will delete previous loaded language
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, filename);

        // Android uses differnt filepaths
        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", filename);
        }

        string dataAsJSON = "";

        // Android can't uses File.ReadAllText. So we need WWWW for reading the file
        if (Application.platform == RuntimePlatform.Android)
        {
            WWW reader = new WWW(filePath);
            while (!reader.isDone)
            {
            }
            dataAsJSON = reader.text;
        }
        else
        {
            dataAsJSON = File.ReadAllText(filePath);
        }


        // Convert Json file in LocalizationData object
        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJSON);

        // Put every text in the dictonary
        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        // Deze kan als het goed is weg. 
        isReady = true;
    }

    // Set Language for player
    public void SetLanguageForPlayer(string filePath)
    {
        int languageNumber = 0;
        switch(filePath)
        {
            case "localizedText_Nl.json":
                LanguageChoice = Language.Dutch;
                languageNumber= 0;
                break;
            case "localizedText_En.json":
                LanguageChoice = Language.English;
                languageNumber = 1;
                break;
            case "localizedText_Es.json":
                LanguageChoice = Language.Spanish;
                languageNumber = 2;
                break;
            default:
                break;
        }

        PlayerDataController.instance.SetLanguage(languageNumber);
    }


    // Get Language from Player
    public void SetLanguage()
    {
        PlayerDataController.instance.Load();
        PlayerData player = PlayerDataController.instance.Player;

        switch (player.language)
        {
            case 0:
                LanguageChoice = Language.Dutch;
        break;
            case 1:
                LanguageChoice = Language.English;
                break;
            case 2:
                LanguageChoice = Language.Spanish;
                break;
            default:
                LanguageChoice = Language.English;
                break;
        }
    }

    // Get Text from Value
    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText != null)
        {
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }
        }
        return result;
    }

    // Deze kan waarschijnlijk weg
    public bool GetisReady()
    {
        return isReady;
    }

    // Return Language
    public Language GetLanguage()
    {
        return LanguageChoice;
    }


}
