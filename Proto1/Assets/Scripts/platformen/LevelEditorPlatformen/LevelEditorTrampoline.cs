using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorTrampoline : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided Trampoline");
        GameObject player = collision.other.gameObject;
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
            if (player.name.Contains("Light"))
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
            if (player.name.Contains("Light"))
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
            if (player.name.Contains("Light"))
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
            if (player.name.Contains("Light"))
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

        player.GetComponent<Rigidbody>().AddForce(velocityx, velocityY * 7f, 0, ForceMode.Impulse);
    }
}
