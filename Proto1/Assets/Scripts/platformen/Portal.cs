using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : Platform
{
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }
    internal void SpawnPortal(Vector3 spawnPosition)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        Portal ramp = Instantiate(this, spawnPosition + rampAdjustment, new Quaternion(0, 0, 0, 0));
        ramp.transform.localScale = new Vector3(200f, 50f, 50f);
        ramp.transform.Rotate(new Vector3(-90f, -90f, -180));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.isTrigger )
        {
            GameObject player = gameState.playerBallManager.activePlayer;
            List<Portal> portallist = gameState.platformManager.allPortals;
            foreach (Portal item in portallist)
            {

                if (this != item)
                {
                    Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
                    if (velocity.x > 0)
                    {
                        player.transform.position = item.transform.position + new Vector3(2, 0, 0);
                        gameState.playerCamera.transform.position = player.transform.position + gameState.playerCamera.TargetMovementOffset;
                    }
                    else
                    {
                        player.transform.position = item.transform.position + new Vector3(-2, 0, 0);
                        gameState.playerCamera.transform.position = player.transform.position + gameState.playerCamera.TargetMovementOffset;
                    }
                }
            }
        }
    }
}

