using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraClass : MonoBehaviour
{

    public Balletje Target { get; set; }

    public Vector3 TargetMovementOffset;
    public Vector3 TargetLookAtOffset;

    public float SpringForce;
    public float SpringDamper;

    private void Start()
    {
        transform.LookAt(Target.transform.position + TargetLookAtOffset);
    }

    void FixedUpdate()
    {
        Rigidbody Body = this.GetComponent<Rigidbody>();

        Vector3 Diff = transform.position - (Target.transform.position + TargetMovementOffset);
        Vector3 Vel = Body.velocity;

        Vector3 force = (Diff * -SpringForce) - (Vel * SpringDamper);

        Body.AddForce(force);

        transform.LookAt(Target.transform.position + TargetLookAtOffset);
    }
}
