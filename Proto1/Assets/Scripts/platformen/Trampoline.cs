using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : Platform
{
    GameState gamestate;
    // Start is called before the first frame update
    void Start()
    {
        gamestate = GameState.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            GameObject player = gamestate.playerBallManager.activePlayer;
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
            GameObject player = gamestate.playerBallManager.activePlayer;
            Rigidbody body = player.GetComponent<Rigidbody>();
            Vector3 velocity = body.velocity;
            body.velocity = new Vector3(velocity.x, 0, 0);

            body.AddForce(velocity.x, 36, 0, ForceMode.Impulse);
        }
    }
}
