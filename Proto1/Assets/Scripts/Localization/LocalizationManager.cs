﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LocalizationManager : MonoBehaviour
{
    public static LocalizationManager instance;

    private Dictionary<string, string> localizedText;
    //private Language Language;
    public Language LanguageChoice { get; set; }
    private bool isReady = false;
    private string missingTextString = "Localized text not found";



    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
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

        if (File.Exists(filePath))
        {
            string dataAsJSON = File.ReadAllText(filePath);
            LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJSON);

            for (int i = 0; i < loadedData.items.Length; i++)
            {
                localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
            }

            SetLanguage();
            Debug.Log("Data is geladen.");
        }
        else
        {

            Debug.LogError("Cannot find file!");
        }

        isReady = true;
        
    }

    private void SetLanguage()
    {
        string key = "language";
        if(localizedText == null){ return;}
        if (localizedText.ContainsKey(key))
        {
            string languageValue = localizedText[key].ToLower();

            if (languageValue == "dutch")
            {
                LanguageChoice = Language.Dutch;
                Debug.Log("Taal is Nederlands");
            }
            else if (languageValue == "english")
            {
                LanguageChoice = Language.English;
                Debug.Log("Taal is Engels");
            }
            else if (languageValue == "spanish")
            {
                LanguageChoice = Language.Spanish;
                Debug.Log("Taal is Spaans");
            }

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
