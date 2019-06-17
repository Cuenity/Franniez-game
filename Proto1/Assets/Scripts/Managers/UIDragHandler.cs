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

    // vertaalt de positie van de muis naar een positie op een plane die op dezelfde positie zit als het grid met alle platformen
    private Vector3 ScreenPointToWorldOnPlane(Vector3 screenPosition, float zPosition)
    {
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition));
        Ray rayCast = camera.ScreenPointToRay(screenPosition);
        plane.Raycast(rayCast, out float enterDist);
        return rayCast.GetPoint(enterDist);
    }

    // deze methode wordt aangeroepen nadat de muis of vinger vanaf een van de inventorybuttons gedragged wordt
    public void OnBeginDrag(PointerEventData data)
    {
        draggingAllowed = true;
        playercamera.platformDragActive = true; // zodat de camera niet ook mag draggen als er een platform gedragged wordt

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

            // outline om alle platformen behalve kanon omdat deze altijd door de speler geplaatst wordt (nooit al staande in een level) en te veel detail heeft voor de outline
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

    // zolang de speler het platform aan het draggen is beweegt deze mee met de positie van de camera
    public void OnDrag(PointerEventData eventData)
    {
        if (draggingAllowed)
        {
            draggedPlatform.transform.position = ScreenPointToWorldOnPlane(eventData.position, zAxis);
        }
    }

    // wanneer het platform losgelaten wordt na draggen wordt deze in het grid vastgezet geplaatst en op de dichtstbijzijnde plek gesnapped
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

    // zodat de camera weer gedragged kan worden nadat het platform is losgelaten, 
    // dit moet aan het eind van de frame omdat de camera anders bewogen wordt in dezelfde frame dat het platform los gelaten wordt
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