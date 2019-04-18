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
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = gamestate.playerBallManager.activePlayer;
        Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
        float velocityY;
        float velocityx;
        if (velocity.y > 0)
        {
            velocityY = velocity.y;
        }
        else
        {
            velocityY = velocity.y * -1;
        }
        if ((velocityY < 2 && velocityY >0 )|| velocityY == 0)
        {
            velocityY = 4;
        }
        else if(velocityY > -2 && velocityY < 0)
        {
            velocityY = -4;
        }

        if (velocity.x <1 && velocity.x >0 )
        {
            velocityx = 2;
        }
        else if ((velocity.x > -1 && velocity.x < 0) || velocity.x == 0)
        {
            velocityx = -2;
        }
        else 
        {
            velocityx = velocity.x;
        }
       
        player.GetComponent<Rigidbody>().AddForce(velocityx, velocityY * 7f, 0, ForceMode.Impulse);
    }
}
