using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    private void OnMouseDown()
    {
        float startRotation = gameObject.transform.parent.transform.rotation.y;
        // spiegel de ramp ipv fakking draaien fakking retard. hij moet binnen de zelfde vakjes blijven!!!!
        gameObject.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, startRotation + 90, 0));
        //gameObject.transform.parent.transform.rotation += Quaternion.Euler(new Vector3(0, 90, 0));
    }
}