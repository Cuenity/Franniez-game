using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformSquare : Platform
{
    private GameState gameState;

    private void Start()
    {
        gameState = GameState.Instance;
    }

    public override void UpdatePlayerPlatforms()
    {
        InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.platformSquareButton);
        if (gameState.levelManager.playerPlatforms.platformSquaresLeftToPlace == 0)
        {
            button.InventoryButtonAllowed = true;
        }
        gameState.levelManager.playerPlatforms.platformSquaresLeftToPlace++;
        gameState.levelManager.playerPlatforms.UpdatePlatformSquaresLeft(button);

        gameState.levelManager.playerPlatforms.placedPlatforms.Remove(gameObject);

        if (!PhotonNetwork.IsConnected)
            Destroy(gameObject);
        else if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(gameObject);
        gameState.playerCamera.platformDragActive = false;
    }
}
