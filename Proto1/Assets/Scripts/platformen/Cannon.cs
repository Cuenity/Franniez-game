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
            Debug.Log("Cannon fire!");
            collision.gameObject.SetActive(false);
        }
    }
}
