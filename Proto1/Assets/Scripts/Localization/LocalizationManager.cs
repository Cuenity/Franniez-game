using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    public Language LanguageChoice { get; set; }
    private bool isReady = false;
    private string missingTextString = "Localized text not found";

    public delegate void ClickAction();
    public static event ClickAction ClickedButton;


    // Start is called before the first frame update
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

    public void GetLanguageSettings()
    {
        Language language = (Language)PlayerDataController.instance.player.language;
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

        LoadLocalizedText(filePath);
    }

    public void LoadLocalizedText(string filename)
    {
        localizedText = new Dictionary<string, string>();
        string filePath = Path.Combine(Application.streamingAssetsPath, filename);

        if (Application.platform == RuntimePlatform.Android)
        {
            filePath = Path.Combine("jar:file://" + Application.dataPath + "!assets/", filename);
        }

        string dataAsJSON = "";

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

        LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJSON);

        for (int i = 0; i < loadedData.items.Length; i++)
        {
            localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
        }

        //SetLanguage();
        Debug.Log("Taal is geladen.");
        isReady = true;

    }

    public void ReturnLanguage(string filePath)
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

    // Alleen public voor Debug, weer terug veranderen naar Private

    public void SetLanguage()
    {
        PlayerDataController.instance.Load();
        Player player = PlayerDataController.instance.player;

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

    public string GetLocalizedValue(string key)
    {
        string result = missingTextString;
        if (localizedText.ContainsKey(key))
        {
            result = localizedText[key];
        }

        return result;

    }

    public bool GetisReady()
    {
        return isReady;
    }

    public Language GetLanguage()
    {
        return LanguageChoice;
    }


}
