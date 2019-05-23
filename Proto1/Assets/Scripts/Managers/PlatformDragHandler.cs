using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformDragHandler : MonoBehaviour
{
    private GameState gameState;
    private Vector3 screenPoint;
    private Vector3 offset;

    private new Camera camera;
    private GameObject draggedPlatformInScene;


    bool draggingAllowed;

    private void Awake()
    {
        gameState = GameState.Instance;
        camera = gameState.playerCamera.GetComponent<Camera>();
    }

    void OnMouseDown()
    {
        screenPoint = camera.WorldToScreenPoint(transform.position);
        if (EventSystem.current.IsPointerOverGameObject())
        {
            draggingAllowed = true;
        }
        else
        {
            draggingAllowed = false;
        }
        gameState.playerCamera.platformDragActive = true;

        if (gameObject.GetComponent<Cannon>())
        {
            draggedPlatformInScene = gameObject.transform.root.gameObject;
        }
        else
        {
            draggedPlatformInScene = gameObject;
        }
    }

    void OnMouseDrag()
    {
        if (!draggingAllowed)
        {
            Vector3 currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = camera.ScreenToWorldPoint(currentScreenPoint);
            draggedPlatformInScene.transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        if (!draggingAllowed)
        {
            if (!gameState.UIManager.GarbageBinHit(draggedPlatformInScene))
            {
                //EventSystem eventSystem = GetComponent<EventSystem>();
                //List<RaycastResult> results = new List<RaycastResult>();
                //bool platformDraggedToButton = false;

                //for (int i = 0; i < gameState.UIManager.instantiatedInventoryButtons.Length; i++)
                //{
                //    GraphicRaycaster ray = gameState.UIManager.instantiatedInventoryButtons[i].GetComponent<GraphicRaycaster>();
                //    PointerEventData pointerEventData = new PointerEventData(eventSystem);
                //    pointerEventData.position = Input.mousePosition;
                //    ray.Raycast(pointerEventData, results);

                //    if (results.Count > 0)
                //    {
                //        platformDraggedToButton = true;
                //    }
                //}

                if (gameState.UIManager.PlatformDraggedToButton())
                {
                    gameState.UIManager.RemoveOldFilledGridSpots(draggedPlatformInScene);
                    draggedPlatformInScene.GetComponent<Platform>().UpdatePlayerPlatforms();
                    // dragactive = false;?
                }
                else
                {
                    gameState.UIManager.RemoveOldFilledGridSpots(draggedPlatformInScene);
                    gameState.platformManager.spawnPlatformOnGrid(draggedPlatformInScene.transform.position, draggedPlatformInScene.GetComponent<Platform>());
                    gameState.playerCamera.platformDragActive = false;
                }
            }
        }
    }

    //private bool GarbageBinHit()
    //{
    //    EventSystem eventSystem = GetComponent<EventSystem>();
    //    List<RaycastResult> results = new List<RaycastResult>();

    //    GraphicRaycaster ray = gameState.UIManager.canvas.gameObject.transform.GetChild(6).GetComponent<GraphicRaycaster>();
    //    PointerEventData pointerEventData = new PointerEventData(eventSystem);
    //    pointerEventData.position = Input.mousePosition;
    //    ray.Raycast(pointerEventData, results);

    //    if (results.Count > 0)
    //    {
    //        RemoveOldFilledGridSpots();
    //        //gameState.buttonManager.UpdatePlayerPlatforms(draggedPlatformInScene);
    //        draggedPlatformInScene.GetComponent<Platform>().UpdatePlayerPlatforms();
    //        return true;
    //    }

    //    return false;
    //}

    //private void RemoveOldFilledGridSpots()
    //{
    //    int filledGridSpotToRemove = draggedPlatformInScene.GetComponent<Platform>().fillsGridSpot;
    //    if (!draggedPlatformInScene.GetComponent<Cannon>())
    //    {
    //        gameState.gridManager.RemoveFilledGridSpots(filledGridSpotToRemove, SizeType.twoByOne);
    //    }
    //    else
    //    {
    //        gameState.gridManager.RemoveFilledGridSpots(filledGridSpotToRemove, SizeType.twoByTwo);
    //    }
    //}
}