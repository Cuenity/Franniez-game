using Photon.Pun;
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

    //vieze fix()
    private void Start()
    {
        if (PhotonNetwork.InRoom)
        {
            Button[] buttons  = GameState.Instance.UIManager.pauseMenuCanvas.GetComponentsInChildren<Button>(true);
            foreach(Button button in buttons)
            {
                if(button.name =="Exit Button")
                {
                    button.gameObject.SetActive(false);
                }
            }

        }
        else
        {
            Button[] buttons = GameState.Instance.UIManager.pauseMenuCanvas.GetComponentsInChildren<Button>(true);
            foreach (Button button in buttons)
            {
                if (button.name == "Exit Button")
                {
                    button.gameObject.SetActive(true);
                }
            }
        }
        
    }


    // Button Action: Go to Level Select
    public void SelectLevel()
    {
        //vieze fix
        if (PhotonNetwork.InRoom)
        {
            PhotonView view = GameState.Instance.playerBallManager.activePlayer.GetComponent<PhotonView>();
            view.RPC("BackToLevelSelect", RpcTarget.All);
        }
        else
        {
            SceneManager.LoadScene("LevelSelect");
        }
    }

    // Button Action: Return to Main Menu
    public void Exit()
    {
        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.Disconnect();
            PhotonNetwork.LoadLevel(3);
            return;
        }
        GAManager.EndGame(GameState.Instance.rollingPhaseManager.countRolling, false);
        SceneManager.LoadScene("StartMenu");
    }
}
