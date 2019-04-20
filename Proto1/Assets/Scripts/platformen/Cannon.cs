using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Platform
{
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision);
        if (collision.gameObject.GetComponent<PlayerBallManager>())
        {
            GameObject playerBall = collision.gameObject;


            gameObject.GetComponentInChildren<CannonFirePoint>().FireCannon(this, playerBall);
        }
    }
}