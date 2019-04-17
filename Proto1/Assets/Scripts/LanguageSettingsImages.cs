using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LanguageSettingsImages : MonoBehaviour
{
    public Sprite SpanishFlag;
    public Sprite UKFlag;
    public Sprite DutchFlag;

    private Button button;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        ChangeFlag();
    }

    private void OnEnable()
    {
        MainMenu.ChangeLanguage += ChangeFlag;
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
