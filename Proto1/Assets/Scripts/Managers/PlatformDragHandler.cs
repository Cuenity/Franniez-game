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

                if (gameState.UIManager.PlatformDraggedToButton())
                {
                    gameState.UIManager.RemoveOldFilledGridSpots(draggedPlatformInScene);
                    draggedPlatformInScene.GetComponent<Platform>().UpdatePlayerPlatforms();
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
}