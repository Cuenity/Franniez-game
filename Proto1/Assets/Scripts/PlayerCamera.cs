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

    internal void ManualInit()
    {
        gameState = GameState.Instance;
        camera = this.GetComponent<Camera>();
        camera.gameObject.SetActive(true);
        if (Target != null)
        {
            transform.LookAt(Target.transform.position + TargetLookAtOffset);
        }
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
        Vector3 move;
        Vector3 diffrence = new Vector3(dragend.x - dragOrigin.x, dragend.y - dragOrigin.y, 0);
        if (Application.platform != RuntimePlatform.Android)
        {
            move = new Vector3(diffrence.x, diffrence.y, 0);
        }
        else
        {
            move = new Vector3(-diffrence.x, -diffrence.y, 0);
        }
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
            Vector3 prevTouchDeltaMag = touchZeroPrevPos - touchOnePrevPos;
            Vector3 touchDeltaMag = touchZero.position - touchOne.position;
            Vector3 diffrence = prevTouchDeltaMag - touchDeltaMag;

            if (diffrence.x < 0 && diffrence.y < 0)
            {
                if (camera.transform.position.z >= -30)
                {
                    camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z - 1);
                }
                else
                {
                    camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -29.9f);
                }
            }
            if (diffrence.x > 0 && diffrence.y > 0)
            {
                if (camera.transform.position.z <= -10)
                {
                    camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, camera.transform.position.z + 1);
                }
                else
                {
                    camera.transform.position = new Vector3(camera.transform.position.x, camera.transform.position.y, -10.1f);
                }
            }
        }
    }

}



