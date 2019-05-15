using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlatform : Platform
{
    private GameState gameState;

    private void Start()
    {
        gameState = GameState.Instance;
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject balletje = gameState.playerBallManager.activePlayer;
        Rigidbody body = balletje.GetComponent<Rigidbody>();
        body.AddForce(new Vector3(body.velocity.x * 9, body.velocity.y, 0), ForceMode.Impulse);
    }

    public override void UpdatePlayerPlatforms()
    {
        InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.boostPlatformButton);
        if (gameState.levelManager.playerPlatforms.boostPlatformsLeftToPlace == 0)
        {
            button.InventoryButtonAllowed = true;
        }
        gameState.levelManager.playerPlatforms.boostPlatformsLeftToPlace++;
        gameState.levelManager.playerPlatforms.UpdateBoostPlatformsLeft(button);

        gameState.levelManager.playerPlatforms.placedPlatforms.Remove(gameObject);

        if (!PhotonNetwork.IsConnected)
            Destroy(gameObject);
        else if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(gameObject);
        gameState.playerCamera.platformDragActive = false;
    }
}
