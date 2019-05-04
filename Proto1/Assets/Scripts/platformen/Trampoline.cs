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
            if ((velocityY < 4 && velocityY > 0) || velocityY == 0)
            {
                if (gamestate.playerBallManager.activePlayer.name.Contains("Light"))
                {
                    velocityY = 2;
                }
                else
                {
                    velocityY = 4;
                }
            }
            else if (velocityY > -4 && velocityY < 0)
            {
                if (gamestate.playerBallManager.activePlayer.name.Contains("Light"))
                {
                    velocityY = -2;
                }
                else
                {
                    velocityY = -4;
                }
            }

            if (velocity.x < 1 && velocity.x > 0)
            {
                if (gamestate.playerBallManager.activePlayer.name.Contains("Light"))
                {
                    velocityx = 1;
                }
                else
                {
                    velocityx = 2;
                }
            }
            else if ((velocity.x > -1 && velocity.x < 0) || velocity.x == 0)
            {
                if (gamestate.playerBallManager.activePlayer.name.Contains("Light"))
                {
                    velocityx = -1;
                }
                else
                {
                    velocityx = -2;
                }
            }
            else
            {
                velocityx = velocity.x;
            }

            player.GetComponent<Rigidbody>().AddForce(velocityx, velocityY * 6f, 0, ForceMode.Impulse);
        }
    }
}
