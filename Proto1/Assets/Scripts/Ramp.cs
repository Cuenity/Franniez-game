using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : Platform
{
    private GameState gameState;

    private void Start()
    {
        gameState = GameState.Instance;
    }

    internal void SpawnRamp(Vector3 spawnPosition)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        Ramp ramp = Instantiate(this, spawnPosition + rampAdjustment, new Quaternion(0, 0, 0, 0));
        ramp.transform.Rotate(new Vector3(-90f, -90f, 0));

    }
    internal void SpawnRampReversed(Vector3 spawnPosition)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        Ramp ramp = Instantiate(this, spawnPosition + rampAdjustment, new Quaternion(0, 0, 0, 0));
        ramp.transform.Rotate(new Vector3(-90f, -90f, -180));
    }

    public override void UpdatePlayerPlatforms()
    {
        InventoryButton button = FindCorrectInventoryButton(InventoryButtonName.rampInventoryButton);
        if (gameState.levelManager.playerPlatforms.rampsLeftToPlace == 0)
        {
            button.InventoryButtonAllowed = true;
        }
        gameState.levelManager.playerPlatforms.rampsLeftToPlace++;
        gameState.levelManager.playerPlatforms.UpdateRampsLeft(button);

        gameState.levelManager.playerPlatforms.placedPlatforms.Remove(gameObject);

        if (!PhotonNetwork.IsConnected)
            Destroy(gameObject);
        else if (PhotonNetwork.IsConnected)
            PhotonNetwork.Destroy(gameObject);
        gameState.playerCamera.platformDragActive = false;
    }
}
