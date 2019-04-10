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

    }
    private void Start()
    {
        if (Target != null)
        {
            transform.LookAt(Target.transform.position + TargetLookAtOffset);
        }
    }

    internal void InitCamera()
    {
        
        this.transform.position = gameState.playerManager.player.spawnpoint + TargetMovementOffset;
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
            if (Application.platform != RuntimePlatform.Android)
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
            else
            {
                mobileScroll();
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
        if (gameState.RollingPhaseActive == false)
        {
            if (platformDragActive == false)
            {
                this.Transfrom_YZ();
            }

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
        Vector3 position = this.transform.position;
        Vector3 dragend = camera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 diffrence = new Vector3(dragend.x - dragOrigin.x, dragend.y - dragOrigin.y, 0);
        Vector3 move = new Vector3(-diffrence.x, -diffrence.y, 0);
        Vector3 outsideGrid = move + position;
        CorrectCamera(outsideGrid);
        transform.Translate(move, Space.World);
    }
    public void CorrectCamera(Vector3 outsideGrid)
    {
        if (outsideGrid.y < gameState.gridManager.heigth * -1)
        {
            this.transform.position = new Vector3(this.transform.position.x, gameState.gridManager.heigth * -1 + .1f, this.transform.position.z);
        }
        if (outsideGrid.y > 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, -.1f, this.transform.position.z);
        }
        if (outsideGrid.x < 0)
        {
            this.transform.position = new Vector3(.1f, this.transform.position.y, this.transform.position.z);
        }
        if (outsideGrid.x > gameState.gridManager.width)
        {
            this.transform.position = new Vector3(gameState.gridManager.width - .1f, this.transform.position.y, this.transform.position.z);
        }
    }

    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;        // The rate of change of the orthographic size in orthographic mode.


    public void mobileScroll()
    {
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }
    }

}



