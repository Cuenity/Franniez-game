using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    private GameState gameState;

    public Balletje Target { get; set; }

    public Vector3 TargetMovementOffset;
    public Vector3 TargetLookAtOffset;

    public float SpringForce;
    public float SpringDamper;
    public float dragSpeed = 2;
    private Vector3 dragOrigin;
    Camera camera;
    private void Awake()
    {
        gameState = GameState.Instance;
        camera = this.GetComponent<Camera>();
        this.transform.position = (new Vector3(0, 5, -10));
    }
    private void Start()
    {
        transform.LookAt(Target.transform.position + TargetLookAtOffset);
    }

    void FixedUpdate()
    {
        if (gameState.RollingPhaseActive == true)
        {
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
                Vector3 cameraposition = camera.transform.position;
                if (cameraposition.x <= -5)
                    camera.transform.position = new Vector3(cameraposition.x + 1, cameraposition.y, cameraposition.z);
            }
            else if (mousedata.y < 0)
            {
                //zoomout
                Vector3 cameraposition = camera.transform.position;
                if (cameraposition.x >= -20) //level groote
                    camera.transform.position = new Vector3(cameraposition.x - 1, cameraposition.y, cameraposition.z);
            }
        }
        Transfrom_YZ();


    }

    private void Transfrom_YZ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log(camera.ScreenToViewportPoint(Input.mousePosition));
            dragOrigin = camera.ScreenToViewportPoint(Input.mousePosition);
            return;
        }

        if (!Input.GetMouseButton(0)) return;

        Vector3 dragend = camera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 diffrence = new Vector3(0, dragend.y - dragOrigin.y, dragend.x - dragOrigin.x);


        // Vector3 move = new Vector3(diffrence.x * dragSpeed, diffrence.y * dragSpeed, 0);
        Vector3 move2 = new Vector3(0, diffrence.x * dragSpeed, diffrence.y * dragSpeed);

        // transform.Translate(move, Space.World);
        transform.Translate(move2, Space.World);
    }

}



