using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryManager : MonoBehaviour
{
    public Image ribbon;
    public Image starIMage;

    public Sprite star1;
    public Sprite stars2;
    public Sprite stars3;

    public Sprite ribbon_Dutch;
    public Sprite ribbon_English;
    public Sprite ribbon_Spanish;


    private void Awake()
    {


        PlayerDataController.instance.Save();
        PlayerDataController.instance.Load();
        Player player = PlayerDataController.instance.player;
        LocalizationManager.instance.SetLanguage();
        Language language = LocalizationManager.instance.GetLanguage();

        switch (LocalizationManager.instance.GetLanguage())
        {
            case Language.Dutch:
                ribbon.sprite = ribbon_Dutch;
                break;
            case Language.English:
                ribbon.sprite = ribbon_English;
                break;
            case Language.Spanish:
                ribbon.sprite = ribbon_Spanish;
                break;
        }

        starIMage.sprite = stars2;


    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
