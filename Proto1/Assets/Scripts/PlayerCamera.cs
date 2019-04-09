using UnityEngine;


[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    private GameState gameState;
    public bool platformDragActive = false;
    public PlayerBal Target { get; set; }

    public Vector3 TargetMovementOffset;
    public Vector3 TargetLookAtOffset;


    public float SpringForce;
    public float SpringDamper;
   // public float dragSpeed = 1;
    private Vector3 dragOrigin;
    Camera camera;
    private void Awake()
    {

        gameState = GameState.Instance;
        camera = this.GetComponent<Camera>();
        this.transform.position = gameState.playerManager.player.spawnpoint +TargetMovementOffset;
    }
    private void Start()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform.position + TargetLookAtOffset);
        }
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
                if (cameraposition.z <= -10)
                {
                    camera.transform.position = new Vector3(cameraposition.x, cameraposition.y, cameraposition.z + 1);
                }
            }
            else if (mousedata.y < 0)
            {
                //zoomout
                Vector3 cameraposition = camera.transform.position;
                if (cameraposition.z >= -30) //level groote
                {
                    camera.transform.position = new Vector3(cameraposition.x, cameraposition.y, cameraposition.z - 1);
                }
            }

        }
        if (gameState.RollingPhaseActive == false)
        {
            if (Input.GetMouseButtonDown(0))
            {
                RaycastHit hit;
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider != null)
                    {
                        GameObject platformHit = hit.collider.gameObject;
                        foreach (GameObject platform in GameState.Instance.levelManager.playerPlatforms.placedPlatforms)
                        {
                            if (platformHit == platform)
                            {
                                platformDragActive = true;
                            }
                        }
                    }
                }
            }
        }
    }


    private void LateUpdate()
    {
        if (platformDragActive == false)
        {
            Transfrom_YZ();
        }
    }


    private void Transfrom_YZ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            dragOrigin = camera.ScreenToViewportPoint(Input.mousePosition);
            return;
        }

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        Vector3 dragend = camera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 diffrence = new Vector3(dragend.x - dragOrigin.x, dragend.y - dragOrigin.y, 0);
        Vector3 move = new Vector3(diffrence.x, diffrence.y, 0);
        transform.Translate(move, Space.World);
    }

}



