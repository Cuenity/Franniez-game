﻿using UnityEngine;
using UnityEngine.EventSystems;

public class PlatformDragManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    
    public GameObject platform;
    private Camera camera;
    PlatformManager platformManager;
    PlayerCamera playercamera;
    private int index = 0;
    
    

    public void OnBeginDrag(PointerEventData data)
    {

        playercamera = GameState.Instance.playerCamera;
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        playercamera.platformDragActive = true;

        platformManager = GameState.Instance.platformManager.GetComponent<PlatformManager>();

        platform = Instantiate(platform);



    //    playercamera.platformDragActive = true;
    //    Debug.Log(data);
    //    platform = Instantiate(platform); // GameObject.CreatePrimitive(PrimitiveType.Sphere);
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

        //Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);
        //platform.transform.position = new Vector3(pos.x, pos.y, 0);

        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        Vector3 pos = ray.origin + (ray.direction);
        Debug.Log(index);
        //Vector3 pos = Input.mousePosition;
        pos.z = 0;
        platform.transform.position = pos;
        //index++;

        


    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);

        platformManager.spawnPlatformOnGrid(platform.transform.position, platform);
        //spawnPlatformOnGrid()



        //platform.transform.position = Vector3.zero;
        //transform.localPosition = Vector3.zero;
        playercamera.platformDragActive = false;
    }

    void Awake()
    {
       
    }
    private void Start()
    {
        playercamera = GameState.Instance.playerCamera;
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        
    }

    void Update()
    {

    }
}


