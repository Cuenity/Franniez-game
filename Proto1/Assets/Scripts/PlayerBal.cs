using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerBal : MonoBehaviour
{
    GameState gameState;
    public Vector3 spawnpoint;

    
    // Start is called before the first frame update
    private void Awake()
    {
        gameState = GameState.Instance;
        gameState.playerManager.player.GetComponent<Rigidbody>().useGravity = false;
        gameState.playerManager.player.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
    }
    void Start()
    {   
        

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.RollingPhaseActive)
        {
            gameState.playerManager.player.GetComponent<Rigidbody>().useGravity = true;
            gameState.playerManager.player.GetComponent<SphereCollider>().isTrigger = false;
        }
        else
        {
            gameState.playerManager.player.GetComponent<Rigidbody>().useGravity = false;
            gameState.playerManager.player.GetComponent<SphereCollider>().isTrigger = true;
        }

    }

    public void respawnBal()
    {
        gameState.RollingPhaseActive = false;
        Camera actualcamera = gameState.GetComponent<Camera>();
        PlayerCamera camera = gameState.playerCamera;
        gameState.playerManager.player.transform.position = gameState.playerManager.player.spawnpoint;
        gameState.playerManager.player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        gameState.playerManager.player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        camera.transform.position = new Vector3(gameState.playerManager.player.spawnpoint.x + camera.TargetMovementOffset.x, gameState.playerManager.player.spawnpoint.y + camera.TargetMovementOffset.y, gameState.playerManager.player.spawnpoint.z + camera.TargetMovementOffset.z);
        camera.transform.LookAt(camera.Target.transform.position);
        camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); 
        gameState.BuildingPhaseActive = true;
    }

    public void SetSpawnpoint(int i)
    {
        gameState = GameState.Instance;
        Vector3 playeradjustment = new Vector3(.5f, 0, 0);
        gameState.playerManager.player.spawnpoint = gameState.gridManager.gridSquares[i] + playeradjustment;
    }

}
