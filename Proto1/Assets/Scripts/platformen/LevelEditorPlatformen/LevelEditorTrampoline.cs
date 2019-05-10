using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorTrampoline : MonoBehaviour
{

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.isTrigger)
        {
            LevelEditorBall player = LevelEditorState.Instance.levelEditorBall;
            Rigidbody body = player.GetComponent<Rigidbody>();
            Vector3 velocity = body.velocity;
            body.velocity = new Vector3(velocity.x, 0, 0);

            body.AddForce(velocity.x, 36, 0, ForceMode.Impulse);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject player = collision.other.gameObject;
        Rigidbody body = player.GetComponent<Rigidbody>();
        Vector3 velocity = body.velocity;
        body.velocity = new Vector3(velocity.x, 0, 0);

        body.AddForce(velocity.x, 36, 0, ForceMode.Impulse);
    }
}
