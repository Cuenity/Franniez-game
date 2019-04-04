using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformDragManager : MonoBehaviour, IDragHandler, IBeginDragHandler
{

    public GameObject platform;
    private Camera camera;
    private int index = 0;

    public void OnBeginDrag(PointerEventData data)
    {
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        Debug.Log(data);

        platform = Instantiate(platform); // GameObject.CreatePrimitive(PrimitiveType.Sphere);
    }

    //public void OnDrag(PointerEventData data)
    //{
    //    // Dragging logic goes here

    //    sphere.transform.localScale = new Vector3(3.0f, 3.0f, 1.0f);
    //    //transform.Translate(0, 0, Time.deltaTime);
    //    sphere.transform.position = new Vector3(4, 0, 0);
    //}


    public void OnDrag(PointerEventData eventData)
    {
        //Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        //platform.transform.position = new Vector3(pos.x, pos.y, 0);

        //Debug.Log(index);
        //    Vector3 pos = Input.mousePosition;
        //platform.transform.position = camera.ScreenToWorldPoint(new Vector3(pos.x, pos.y, 0));
        //index++;


    }

    public void OnEndDrag()
    {

        //spawnPlatformOnGrid()



        //platform.transform.position = Vector3.zero;
        //transform.localPosition = Vector3.zero;
    }

    void Awake()
    {
    }

    void Update()
    {

    }
}
