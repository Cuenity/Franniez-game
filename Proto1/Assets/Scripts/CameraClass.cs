using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CameraClass : MonoBehaviour
{
    private GameState gameState;

    public Balletje Target { get; set; }

    public Vector3 TargetMovementOffset;
    public Vector3 TargetLookAtOffset;

    public float SpringForce;
    public float SpringDamper;
    public float dragSpeed = 2;
    private Vector3 dragOrigin;

    private void Start()
    {
        transform.LookAt(Target.transform.position + TargetLookAtOffset);
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }

    void FixedUpdate()
    {
        if (gameState.RollingPhaseActive == true)
        {
            Debug.Log(gameState.RollingPhaseActive.ToString());
            Rigidbody Body = this.GetComponent<Rigidbody>();

            Vector3 Diff = transform.position - (Target.transform.position + TargetMovementOffset);
            Vector3 Vel = Body.velocity;

            Vector3 force = (Diff * -SpringForce) - (Vel * SpringDamper);

            Body.AddForce(force);

            transform.LookAt(Target.transform.position + TargetLookAtOffset);
        }
        if (gameState.BuildingPhaseActive == true)
        {
            Vector2 mousedata = Input.mouseScrollDelta;
            if (mousedata.y > 0)
            {
                //zoomin
                Vector3 cameraposition = gameState.cameraClass.transform.position;
                if (cameraposition.x <= -5)
                    gameState.cameraClass.transform.position = new Vector3(cameraposition.x + 1, cameraposition.y, cameraposition.z);
            }
            else if (mousedata.y < 0)
            {
                //zoomout
                Vector3 cameraposition = gameState.cameraClass.transform.position;
                if (cameraposition.x >= -20) //level groote
                    gameState.cameraClass.transform.position = new Vector3(cameraposition.x - 1, cameraposition.y, cameraposition.z);
            }
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    dragOrigin = Input.mousePosition;
        //    return;
        //}

        //if (!Input.GetMouseButton(0)) return;
        //Vector3 pos = new Vector3(gameState.cameraClass.transform.position.x, Input.mousePosition.y - dragOrigin.y, Input.mousePosition.z - dragOrigin.z);
        //Vector3 move = new Vector3(pos.x * dragSpeed, 0, pos.y * dragSpeed);

        //transform.Translate(move, Space.World);
    }

}



