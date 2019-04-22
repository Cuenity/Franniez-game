using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Close()
    {
        this.GetComponent<Canvas>().enabled = false;
        //GameState.Instance.UIManager.canvas.enabled = true;
        GameState.Instance.playerCamera.platformDragActive = false;
    }

    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void Exit()
    {
        SceneManager.LoadScene("StartMenu");
    }

    public void Settings()
    {
        // Doe iets met settings
    }

    public void SelectSkin()
    {
        // Doe iets met skins
    }
}
