using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraClass : MonoBehaviour
{

    public Balletje Target { get; set; }

    public Vector3 TargetMovementOffset;
    public Vector3 TargetLookAtOffset;
    public Vector3 BehindPlayerPosition;

    public float SpringForce;
    public float SpringDamper;

    private void Start()
    {
        this.transform.position = BehindPlayerPosition;
        transform.LookAt(Target.transform.position + TargetLookAtOffset);
    }
    private void LateUpdate()
    {
        transform.LookAt(Target.transform.position + TargetLookAtOffset);
    }
}
