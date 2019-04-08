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
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
        gameState = GameState.Instance;
    }
    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.RollingPhaseActive)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<SphereCollider>().isTrigger = true;
        }

    }

    public void respawnBal()
    {
        gameState.RollingPhaseActive = false;
        Camera actualcamera = gameState.GetComponent<Camera>();
        PlayerCamera camera = gameState.playerCamera;
        this.transform.position = this.spawnpoint;
        this.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        camera.transform.position = new Vector3(this.spawnpoint.x + camera.TargetMovementOffset.x, this.spawnpoint.y + camera.TargetMovementOffset.y, this.spawnpoint.z + camera.TargetMovementOffset.z);
        camera.transform.LookAt(camera.Target.transform.position);
        camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); 
        gameState.BuildingPhaseActive = true;
    }

    public void SetSpawnpoint(int i)
    {
        gameState = GameState.Instance;
        Vector3 playeradjustment = new Vector3(.5f, 0, 0);
        this.spawnpoint = gameState.gridManager.gridSquares[i] + playeradjustment;
    }

}
