using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostPlatform : MonoBehaviour
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
    private void OnCollisionEnter(Collision collision)
    {
        PlayerBal balletje = gameState.playerManager.player;
        Rigidbody body = balletje.GetComponent<Rigidbody>();
        body.AddForce(new Vector3(body.velocity.x * 9, body.velocity.y, 0), ForceMode.Impulse);
    }
}
