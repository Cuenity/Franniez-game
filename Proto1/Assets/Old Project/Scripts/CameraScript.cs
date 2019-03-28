using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraScript : MonoBehaviour
{
    public PlayerBall Target;

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
    void FixedUpdate()
    {
        //if (Input.GetKeyDown("q"))
        //{
        //    //move behind player???
        //    this.transform.position = BehindPlayerPosition;
            
        //    //Rigidbody Body = this.GetComponent<Rigidbody>();

        //    //Vector3 Diff = transform.position - (Target.transform.position + TargetMovementOffset);
        //    //Vector3 Vel = Body.velocity;

        //    //Vector3 force = (Diff * -SpringForce) - (Vel * SpringDamper);

        //    //Body.AddForce(force);

        //    //transform.LookAt(Target.transform.position + TargetLookAtOffset);
        //}
    }

    private void LateUpdate()
    {
        
        transform.LookAt(Target.transform.position + TargetLookAtOffset);
    }
    
}