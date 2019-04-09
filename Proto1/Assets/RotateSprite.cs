using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSprite : MonoBehaviour
{
    public PlatformType type;

    private void OnMouseDown()
    {
        switch (type)
        {
            case PlatformType.ramp:
                gameObject.transform.parent.transform.localRotation = new Quaternion(
                    gameObject.transform.parent.transform.localRotation.x * -1.0f,
                    gameObject.transform.parent.transform.localRotation.y,
                    gameObject.transform.parent.transform.localRotation.z,
                    gameObject.transform.parent.transform.localRotation.w * -1.0f);
                gameObject.transform.localPosition = new Vector3(
                    gameObject.transform.localPosition.x * -1.0f, 
                    gameObject.transform.localPosition.y * -1.0f, 
                    gameObject.transform.localPosition.z * -1.0f); //= new Vector3(-0.0101f, 0, 0);

                //gameObject.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                break;
            case PlatformType.platformSquare:
                // op zijn zeikant 90 draaien    zoiets? gameObject.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, startRotation + 90, 0));
                break;
            default:
                break;
        }
        //float startRotation = gameObject.transform.parent.transform.rotation.y;
        //// spiegel de ramp ipv fakking draaien fakking retard. hij moet binnen de zelfde vakjes blijven!!!!
        //gameObject.transform.parent.transform.rotation = Quaternion.Euler(new Vector3(0, startRotation + 90, 0));
        ////gameObject.transform.parent.transform.rotation += Quaternion.Euler(new Vector3(0, 90, 0));
    }
}