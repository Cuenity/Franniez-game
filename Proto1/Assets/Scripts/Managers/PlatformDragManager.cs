using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformDragManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler//, IPointerDownHandler
{
    public GameObject platformSquare;
    public GameObject ramp;

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
        playercamera = GameState.Instance.playerCamera;
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        playercamera.platformDragActive = true;

        platformManager = GameState.Instance.platformManager.GetComponent<PlatformManager>();

        GameObject inventoryButton = data.pointerPressRaycast.gameObject.transform.parent.gameObject;

        if (inventoryButton.name == "platformSquareButton")
        {
            draggedPlatform = Instantiate(platformSquare);
        } 
        else if (inventoryButton.name == "rampInventoryButton")
        {
            draggedPlatform = Instantiate(ramp);
        }
    }
    


    public void OnDrag(PointerEventData eventData)
    {
        draggedPlatform.transform.position = ScreenPointToWorldOnPlane(eventData.position, zAxis); //+ clickOffset; 

    }


    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);

        platformManager.spawnPlatformOnGrid(draggedPlatform.transform.position, draggedPlatform);
        //spawnPlatformOnGrid()



        //platform.transform.position = Vector3.zero;
        //transform.localPosition = Vector3.zero;
        playercamera.platformDragActive = false;
    }





}

//public static class extensionMethod
//{
//    public static Vector3 ScreenPointToWoldOnPlane(this Camera cam, Vector3 screenPosition, float zPos)
//    {
//        float enterDist;
//        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPos));
//        Ray rayCast = cam.ScreenPointToRay(screenPosition);
//        plane.Raycast(rayCast, out enterDist);
//        return rayCast.GetPoint(enterDist);
//    }
//}
