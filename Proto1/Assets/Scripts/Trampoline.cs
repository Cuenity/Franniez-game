using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
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
    private void OnTriggerEnter(Collider other)
    {
        PlayerBal player = gamestate.playerManager.player;
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
        float velocityY = velocity.y * -1;
        player.GetComponent<Rigidbody>().AddForce(velocity.x*1.5f, velocityY*5, 0, ForceMode.Impulse);
    }
    private void OnCollisionEnter(Collision collision)
    {
        PlayerBal player = gamestate.playerManager.player;
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
        float velocityY = velocity.y * -1;
        player.GetComponent<Rigidbody>().AddForce(velocity.x*5, velocityY * 4, 0, ForceMode.Impulse);
    }
}
