using Photon.Pun;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private GameState gameState;

    [SerializeField]
    private GameObject platformSquare;
    [SerializeField]
    private GameObject ramp;
    //public GameObject trampoline;
    //public GameObject boostPlatform;
    //public GameObject cannon;

    public RotateSprite rotateSprite;


    GameObject draggedPlatform;
    bool draggingAllowed;

    private new Camera camera;
    PlatformManager platformManager;
    PlayerCamera playercamera;

    float zAxis = 0;

    private void Start()
    {
        gameState = GameState.Instance;
        playercamera = gameState.playerCamera;
        camera = gameState.playerCamera.GetComponent<Camera>();
        platformManager = gameState.platformManager.GetComponent<PlatformManager>();

        SetWorldMaterials();

        zAxis = transform.position.z;
    }

    private Vector3 ScreenPointToWorldOnPlane(Vector3 screenPosition, float zPosition)
    {
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition));
        Ray rayCast = camera.ScreenPointToRay(screenPosition);
        plane.Raycast(rayCast, out float enterDist);
        return rayCast.GetPoint(enterDist);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        draggingAllowed = true;
        playercamera.platformDragActive = true;

        InventoryButton correctButton = gameObject.GetComponent<InventoryButton>();
        if (correctButton.InventoryButtonAllowed)
        {
            if (!PhotonNetwork.IsConnected)
            {
                draggedPlatform = correctButton.SpawnPlatformFromInventoryButton();
            }
            else
            {
                //zorgen dat de andere speler ook het platform kan zien
                draggedPlatform = correctButton.SpawnPhotonPlatformFromInventoryButton();
            }

            if (!draggedPlatform.GetComponent<Cannon>())
            {
                var outline = draggedPlatform.AddComponent<Outline>();
                outline.OutlineMode = Outline.Mode.OutlineAll;
                outline.OutlineColor = Color.blue;
                outline.OutlineWidth = 10f;
            }

            draggedPlatform.AddComponent<PlatformDragHandler>();
        }
        else
        {
            draggingAllowed = false;
            playercamera.platformDragActive = false;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (draggingAllowed)
        {
            draggedPlatform.transform.position = ScreenPointToWorldOnPlane(eventData.position, zAxis); //+ clickOffset; 
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingAllowed)
        {
            if (!gameState.UIManager.GarbageBinHit(draggedPlatform))
            {
                bool platformDraggedToButton = gameState.UIManager.PlatformDraggedToButton();
                if (platformDraggedToButton)
                {
                    draggedPlatform.GetComponent<Platform>().UpdatePlayerPlatforms();
                }
                else
                {
                    gameState.levelManager.playerPlatforms.placedPlatforms.Add(draggedPlatform);
                    platformManager.spawnPlatformOnGrid(draggedPlatform.transform.position, draggedPlatform.GetComponent<Platform>());
                    StartCoroutine(SetDragActiveFalseAfterEndOfFrame());
                }
            }

        }
    }

    public IEnumerator SetDragActiveFalseAfterEndOfFrame()
    {
        yield return new WaitForEndOfFrame();
        gameState.playerCamera.platformDragActive = false;
    }

    private void SetWorldMaterials()
    {
        WorldManager worldManager = new WorldManager();
        Material[] materials = worldManager.GetMaterials();

        platformSquare.GetComponent<Renderer>().material = materials[0];
        ramp.GetComponent<Renderer>().material = materials[1];
        camera.GetComponent<Skybox>().material = worldManager.GetSkyBox();
    }
}