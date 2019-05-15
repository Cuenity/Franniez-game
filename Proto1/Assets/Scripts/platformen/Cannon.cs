using Photon.Pun;
using UnityEngine;

public class Cannon : Platform
{
    private GameState gameState;

    private void Start()
    {
        gameState = GameState.Instance;
        transform.GetChild(4).GetComponent<Canvas>().worldCamera = GameState.Instance.playerCamera.GetComponent<Camera>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.GetComponent<PlayerBallManager>())
        {
            GameObject playerBall = collision.gameObject;
            gameObject.GetComponentInChildren<CannonFirePoint>().FireCannon(playerBall, this);
        }
    }

    public override void UpdatePlayerPlatforms()
    {
        InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.cannonPlatformButton);
        if (gameState.levelManager.playerPlatforms.cannonPlatformsLeftToPlace == 0)
        {
            button.InventoryButtonAllowed = true;
        }
        gameState.levelManager.playerPlatforms.cannonPlatformsLeftToPlace++;
        gameState.levelManager.playerPlatforms.UpdateCannonPlatformsLeft(button);

        gameState.levelManager.playerPlatforms.placedPlatforms.Remove(gameObject);

        if (!PhotonNetwork.IsConnected)
            Destroy(gameObject);
        else if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(gameObject);
        gameState.playerCamera.platformDragActive = false;
    }
}