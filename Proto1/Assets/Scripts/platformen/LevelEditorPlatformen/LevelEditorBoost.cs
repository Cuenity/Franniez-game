using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorBoost : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Boost Collided");
        GameObject balletje = collision.other.gameObject;
        Rigidbody body = balletje.GetComponent<Rigidbody>();
        body.AddForce(new Vector3(body.velocity.x * 9, body.velocity.y, 0), ForceMode.Impulse);
    }
}
