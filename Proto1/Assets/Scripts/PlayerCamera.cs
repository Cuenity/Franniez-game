using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Camera))]
public class PlayerCamera : MonoBehaviour
{
    private GameState gameState;
    public bool platformDragActive = false;
    public GameObject Target { get; set; }

    public Vector3 TargetMovementOffset;
    public Vector3 TargetLookAtOffset;
    int index = 0;
    bool animation = false;


    public float SpringForce;
    public float SpringDamper;
    // public float dragSpeed = 1;
    private Vector3 dragOrigin;
    Camera camera;
    Vector2?[] oldTouchPositions = {
        null,
        null
    };

    Vector3 startPos, endPos, direction;
    float touchTimeStart, touchTimeFinish, timeInterval;

    public float throwForce = 3f;


    private void Awake()
    {

        gameState = GameState.Instance;
        camera = this.GetComponent<Camera>();

    }
    private void Start()
    {
        StartCoroutine(PlayAnimation());
        if (Target != null)
        {
            transform.LookAt(Target.transform.position + TargetLookAtOffset);
        }
    }
    private void OnAnimatorIK(int layerIndex)
    {

    }

    internal void InitCamera()
    {
        this.transform.position = gameState.playerManager.playerManager.spawnpoint + TargetMovementOffset;
    }

    internal void ManualInit()
    {
        gameState = GameState.Instance;
        camera = this.GetComponent<Camera>();
        camera.gameObject.SetActive(true);
        this.transform.position = gameState.playerBallManager.spawnpoint + TargetMovementOffset;
        if (Target != null)
        {
            transform.LookAt(Target.transform.position + TargetLookAtOffset);
        }
        this.transform.rotation = new Quaternion(0, 0, 0, 0);

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
            mobileZoom();
        }
        if (gameState.BuildingPhaseActive == true)
        {
            if (this.transform.position.y < gameState.gridManager.heigth * -1)
            {
                this.transform.position = new Vector3(this.transform.position.x, gameState.gridManager.heigth * -1 + .1f, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            if (this.transform.position.y > 0)
            {
                this.transform.position = new Vector3(this.transform.position.x, -.1f, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            if (this.transform.position.x < 0)
            {
                this.transform.position = new Vector3(.1f, this.transform.position.y, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            if (this.transform.position.x > gameState.gridManager.width)
            {
                this.transform.position = new Vector3(gameState.gridManager.width - .1f, this.transform.position.y, this.transform.position.z);
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }

            if (Application.platform != RuntimePlatform.Android)
            {
                Vector3 mousedata = Input.mouseScrollDelta;
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
                if (Application.platform != RuntimePlatform.Android)
                {
                    Transfrom_YZ();
                }
                else
                {
                    mobileScroll();
                }
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

    public void mobileZoom()
    {
        if (Input.touchCount == 2)
        {
            float perspectiveZoomSpeed = 0.05f;        // The rate of change of the field of view in perspective mode.

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

            // Otherwise change the field of view based on the change in distance between the touches.
            camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;

            // Clamp the field of view to make sure it's between 0 and 180.
            camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 40.1f, 89.9f);
        }
    }

    public void mobileScroll()
    {
        Vector2 oldTouchVector = new Vector2();
        float oldTouchDistance = new float();

        if (Input.touchCount == 0)
        {
            oldTouchPositions[0] = null;
            oldTouchPositions[1] = null;
        }
        else if (Input.touchCount == 1)
        {
            if (oldTouchPositions[0] == null || oldTouchPositions[1] != null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = null;
                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            }
            else
            {

                this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                Vector2 newTouchPosition = Input.GetTouch(0).position;
                Vector3 outsideGrid = transform.position + transform.TransformDirection((Vector3)((oldTouchPositions[0] - newTouchPosition) * GetComponent<Camera>().orthographicSize / GetComponent<Camera>().pixelHeight * 2f));

                bool nope = true;
                if (outsideGrid.y < gameState.gridManager.heigth * -1)
                {
                    transform.position = new Vector3(outsideGrid.x, gameState.gridManager.heigth * -1 + .1f, outsideGrid.z);
                    this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    nope = false;
                }
                if (outsideGrid.y > 0)
                {
                    transform.position = new Vector3(outsideGrid.x, -.1f, outsideGrid.z);
                    this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    nope = false;
                }
                if (outsideGrid.x < 0)
                {
                    transform.position = new Vector3(.1f, outsideGrid.y, outsideGrid.z);
                    this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    nope = false;
                }
                if (outsideGrid.x > gameState.gridManager.width)
                {
                    transform.position = new Vector3(gameState.gridManager.width - .1f, outsideGrid.y, outsideGrid.z);
                    this.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                    nope = false;
                }
                if (nope)
                {
                    transform.position = outsideGrid;
                }
                Vector2 diffrence = newTouchPosition - (Vector2)oldTouchPositions[0];
                this.GetComponent<Rigidbody>().AddForce(-diffrence * 3);

                oldTouchPositions[0] = newTouchPosition;
            }
        }
        else
        {
            if (oldTouchPositions[1] == null)
            {
                oldTouchPositions[0] = Input.GetTouch(0).position;
                oldTouchPositions[1] = Input.GetTouch(1).position;
                oldTouchVector = (Vector2)(oldTouchPositions[0] - oldTouchPositions[1]);
                oldTouchDistance = oldTouchVector.magnitude;
            }
            else
            {
                mobileZoom();
            }
        }
    }

    public IEnumerator PlayAnimation()
    {
        yield return new WaitForEndOfFrame();
        gameState.RollingPhaseActive = false;
        gameState.BuildingPhaseActive = false;
        gameState = GameState.Instance;
        gameState.playerCamera.transform.position = gameState.levelManager.finish.transform.position + TargetMovementOffset;
        camera = gameState.playerCamera.GetComponent<Camera>();

        camera.transform.LookAt(gameState.levelManager.finish.transform.position);
        StartCoroutine(zoomout());
        index = 0;


        // StartCoroutine(zoomin());

    }
    public IEnumerator zoomout()
    {
        while (index < 50)
        {
            this.transform.position = this.transform.position + new Vector3(0, 0, -.1f);
            yield return new WaitForEndOfFrame();
            index++;
        }
        StartCoroutine(finishtostart());

    }
    public IEnumerator zoomin()
    {
        this.transform.position = this.Target.transform.position + TargetMovementOffset + new Vector3(0, 0, -5);
        camera.transform.LookAt(this.Target.transform.position);
        while (index < 50)
        {
            this.transform.position = this.transform.position + new Vector3(0, 0, .1f);
            yield return new WaitForEndOfFrame();           
            index++;
        }
        gameState.BuildingPhaseActive = true;
    }
    public IEnumerator finishtostart()
    {
        index = 0;
        Vector3 cameraPos = this.transform.position;
        Vector3 targetPos = this.Target.transform.position + TargetMovementOffset;
        Vector3 difference = (targetPos - cameraPos) / 100;
        difference.z = 0;
        while (index <= 100)
        {
            this.transform.position = this.transform.position + difference;
            yield return new WaitForEndOfFrame();         
            index++;
        }
        index = 0;
        StartCoroutine(zoomin());

    }


}





