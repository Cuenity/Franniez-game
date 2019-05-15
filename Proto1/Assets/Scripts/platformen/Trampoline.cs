using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Platform
{
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Instance;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            GameObject player = gameState.playerBallManager.activePlayer;
            Rigidbody body = player.GetComponent<Rigidbody>();
            Vector3 velocity = body.velocity;
            body.velocity= new Vector3( velocity.x, 0, 0);           

            body.AddForce(velocity.x, 36, 0, ForceMode.Impulse);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (PhotonNetwork.InRoom)
        {
            GameObject player = gameState.playerBallManager.activePlayer;
            Rigidbody body = player.GetComponent<Rigidbody>();
            Vector3 velocity = body.velocity;
            body.velocity = new Vector3(velocity.x, 0, 0);

            body.AddForce(velocity.x, 36, 0, ForceMode.Impulse);
        }
    }

    public override void UpdatePlayerPlatforms()
    {
        InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.trampolineButton);
        if (gameState.levelManager.playerPlatforms.trampolinesLeftToPlace == 0)
        {
            button.InventoryButtonAllowed = true;
        }
        gameState.levelManager.playerPlatforms.trampolinesLeftToPlace++;
        gameState.levelManager.playerPlatforms.UpdateTrampolinesLeft(button);

        gameState.levelManager.playerPlatforms.placedPlatforms.Remove(gameObject);

        if (!PhotonNetwork.IsConnected)
            Destroy(gameObject);
        else if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(gameObject);
        gameState.playerCamera.platformDragActive = false;
    }
}
