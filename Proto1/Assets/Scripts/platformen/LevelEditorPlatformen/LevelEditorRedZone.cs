using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelEditorRedZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collided RedZone");
        if (other.isTrigger && other.name.Contains("BlackHole"))
        {

        }
        else
        {
            Destroy(other.gameObject);
        }

    }
}
