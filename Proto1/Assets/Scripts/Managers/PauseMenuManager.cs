using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    /* 
        Dit is eigenlijk gewoon een Button Manager, kijken of dit in de toekomst gewoon bij Button Manager kan
        omdat deze al in Gamestate staat
    */


    // Button Action: Close the Pause Menu Canvas
    public void Close()
    {
        // Close canvas
        this.GetComponent<Canvas>().enabled = false;
        // Set Drag on deactive again
        GameState.Instance.playerCamera.platformDragActive = false;
    }

    // Button Action: Go to Level Select
    public void SelectLevel()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    // Button Action: Return to Main Menu
    public void Exit()
    {
        SceneManager.LoadScene("StartMenu");
    }

    // Button Action: Show Settings canvas
    public void Settings()
    {
        // Doe iets met settings
    }

    // Button Action: Show Skin canvas
    public void SelectSkin()
    {
        // Doe iets met skins
    }
}
