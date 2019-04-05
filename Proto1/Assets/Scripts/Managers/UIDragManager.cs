using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler//, IPointerDownHandler
{
    //deze fields verwijderen
    public GameObject platformSquare;
    public GameObject ramp;
    //tot hier


    GameObject draggedPlatform;

    private new Camera camera;
    PlatformManager platformManager;
    PlayerCamera playercamera;


    float zAxis = 0;
    //Vector3 clickOffset = Vector3.zero;

    private void OnPointerDown()
    {
        playercamera.platformDragActive = true;
    }

    private void Start()
    {
        playercamera = GameState.Instance.playerCamera;
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();

        zAxis = transform.position.z;
    }

    private Vector3 ScreenPointToWorldOnPlane(Vector3 screenPosition, float zPosition)
    {
        //float enterDist;
        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition));
        Ray rayCast = camera.ScreenPointToRay(screenPosition);
        plane.Raycast(rayCast, out float enterDist);
        return rayCast.GetPoint(enterDist);
    }

    //public void OnPointerDown(PointerEventData eventData)
    //{
    //    Debug.Log(eventData);
    //    clickOffset = transform.position - ScreenPointToWorldOnPlane(eventData.position, zAxis);
    //}

        
    public void OnBeginDrag(PointerEventData data)
    {
        Debug.Log(data.pointerPressRaycast.gameObject.name);

        playercamera = GameState.Instance.playerCamera;
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        playercamera.platformDragActive = true;

        platformManager = GameState.Instance.platformManager.GetComponent<PlatformManager>();

        GameObject inventoryButton = data.pointerPressRaycast.gameObject.transform.parent.gameObject;

        if (inventoryButton)
        {
            if (inventoryButton.name == "platformSquareButton")
            {
                draggedPlatform = Instantiate(platformSquare);
            }
            else if (inventoryButton.name == "rampInventoryButton")
            {
                draggedPlatform = Instantiate(ramp);
            }
        }

        else
        {
            Debug.Log(data.pointerPressRaycast.gameObject);
            draggedPlatform = data.pointerPressRaycast.gameObject;
        }



        //draggedPlatform = GameState.Instance.levelManager.playerPlatforms.InstantiatePlayerPlatform(inventoryButton);
    }
    


    public void OnDrag(PointerEventData eventData)
    {
        draggedPlatform.transform.position = ScreenPointToWorldOnPlane(eventData.position, zAxis); //+ clickOffset; 
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        GameState.Instance.levelManager.playerPlatforms.placedPlatforms.Add(draggedPlatform);

        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);

        platformManager.spawnPlatformOnGrid(draggedPlatform.transform.position, draggedPlatform);
        //spawnPlatformOnGrid()



        //platform.transform.position = Vector3.zero;
        //transform.localPosition = Vector3.zero;
        playercamera.platformDragActive = false;
    }    
}