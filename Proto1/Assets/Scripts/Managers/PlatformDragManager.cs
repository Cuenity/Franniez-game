using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformDragManager : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private new Camera camera;
    private GameObject draggedPlatformInScene;

    bool rotateSpriteHit;

    private void Awake()
    {
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
    }

    //private void ShowTurnPlatformButton()
    //{

    //}

    // rotatesprite code weg hier? dit zou moeten helpen als er op een sprite geklikt wordt en dan een ander platform onder zit dat die niet gedragged wordt maar werkt nu geloof ik niet
    void OnMouseDown()
    {
        rotateSpriteHit = false;
        RaycastHit hit;
        Ray ray = camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider != null)
            {
                GameObject potentialRotateSprite = hit.collider.gameObject;
                if (potentialRotateSprite.gameObject.tag == "RotateSprite")
                {
                    rotateSpriteHit = true;
                    //gameObject.transform.rotation = Quaternion.Euler(new Vector3(0, 90, 0));
                }
            }
        }

        if (!rotateSpriteHit)
        {
            screenPoint = camera.WorldToScreenPoint(transform.position);

            GameState.Instance.playerCamera.platformDragActive = true;

            if (tag == "Cannon")
            {
                draggedPlatformInScene = gameObject.transform.root.gameObject;
                Debug.Log(draggedPlatformInScene.name);
            }
            else
            {
                draggedPlatformInScene = gameObject;
            }

            //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

    }

    void OnMouseDrag()
    {
        if (!rotateSpriteHit)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = camera.ScreenToWorldPoint(curScreenPoint);// + offset;
            draggedPlatformInScene.transform.position = curPosition;
        }
    }

    private void OnMouseUp()
    {
        if (!rotateSpriteHit)
        {
            if (!GarbageBinHit())
            {
                EventSystem eventSystem = GetComponent<EventSystem>();
                List<RaycastResult> results = new List<RaycastResult>();
                bool platformDraggedToButton = false;

                for (int i = 0; i < GameState.Instance.UIManager.instantiatedInventoryButtons.Length; i++)
                {
                    GraphicRaycaster ray = GameState.Instance.UIManager.instantiatedInventoryButtons[i].GetComponent<GraphicRaycaster>();
                    PointerEventData pointerEventData = new PointerEventData(eventSystem);
                    pointerEventData.position = Input.mousePosition;
                    ray.Raycast(pointerEventData, results);

                    if (results.Count > 0)
                    {
                        platformDraggedToButton = true;
                    }
                }

                if (platformDraggedToButton)
                {
                    RemoveOldFilledGridSpots();
                    GameState.Instance.buttonManager.UpdatePlayerPlatforms(draggedPlatformInScene);
                    // dragactive = false;?
                }
                else
                {
                    RemoveOldFilledGridSpots();
                    GameState.Instance.platformManager.spawnPlatformOnGrid(draggedPlatformInScene.transform.position, draggedPlatformInScene);
                    GameState.Instance.playerCamera.platformDragActive = false;
                }
            }
        }
    }

    private bool GarbageBinHit()
    {
        EventSystem eventSystem = GetComponent<EventSystem>();
        List<RaycastResult> results = new List<RaycastResult>();

        GraphicRaycaster ray = GameState.Instance.UIManager.canvas.gameObject.transform.GetChild(6).GetComponent<GraphicRaycaster>();
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        ray.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            RemoveOldFilledGridSpots();
            GameState.Instance.buttonManager.UpdatePlayerPlatforms(draggedPlatformInScene.gameObject);
            return true;
        }

        //foreach (RaycastResult result in results)
        //{
        //    GameState.Instance.buttonManager.UpdatePlayerPlatforms(gameObject);
        //    return true;
        //}
        return false;
    }

    private void RemoveOldFilledGridSpots()
    {
        int filledGridSpotToRemove = draggedPlatformInScene.GetComponent<Platform>().fillsGridSpot;
        if (!draggedPlatformInScene.GetComponent<Cannon>())
        {
            GameState.Instance.gridManager.RemoveFilledGridSpots(filledGridSpotToRemove, SizeType.twoByOne);
        }
        else
        {
            GameState.Instance.gridManager.RemoveFilledGridSpots(filledGridSpotToRemove, SizeType.twoByTwo);
        }
    }
}