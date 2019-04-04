using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Deze moeten uiteindelijk uit de player preference komen
    public string LocalizationDutch = "localizedText_nl.json";
    public string LocalizationEnglish = "localizedText_en.json";
    private Player player;

    // Start is called before the first frame update

    public void Start()
    {
        //LocalizationManager localizationManager = new LocalizationManager();
        //localizationManager.LoadLocalizedText(LocalizationDutch);
    }

    public void StartGame()
    {
        Debug.Log("Start Game");
        

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Debug.Log("Exit game");
            Application.Quit();
        }
    }
}
