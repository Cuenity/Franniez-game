using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LocalizedText : MonoBehaviour
{
    /*
        LOCALIZEDTEXT:

        Script zit vast aan een Text Component in Editor of Prefab
        Dit script wordt ook aangeroepen wanneer de speler de taal veranderd in de settings
    */


    [SerializeField]
    private string key;

    public void Start()
    {
        SetLocalizedText();
    }

    // Add to ChangeLanguage (MainMenu.cs) trigger
    private void OnEnable()
    {
        GetComponentInParent<MainMenu>().ChangeLanguage += SetLocalizedText;
    }

    // Set the localized text with choosen Language
    public void SetLocalizedText()
    {
        // Get Text Component
        Text text = GetComponent<Text>();

        // Set the text of the component with the String from Key value.
        text.text = LocalizationManager.instance.GetLocalizedValue(key);
    }
}
