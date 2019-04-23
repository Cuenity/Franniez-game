using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSettingsImages : MonoBehaviour
{
    // Private serializable properties
    [SerializeField]
    private Sprite SpanishFlag, UKFlag, DutchFlag;

    // Private properties
    private Button button;

    void Start()
    {
        button = GetComponent<Button>();
        ChangeFlag();
    }

    // Add ChangeFlag method to ChangeLanguage Trigger (Main Menu)
    private void OnEnable()
    {
        MainMenu.ChangeLanguage += ChangeFlag;
    }

    // Change Flag images on bases of the players language
    public void ChangeFlag()
    {
        switch (PlayerDataController.instance.player.language)
        {
            case 0:
                button.GetComponent<Image>().sprite = DutchFlag;
                break;
            case 1:
                button.GetComponent<Image>().sprite = UKFlag;
                break;
            case 2:
                button.GetComponent<Image>().sprite = SpanishFlag;
                break;
            default:
                break;
        }
    }
}
