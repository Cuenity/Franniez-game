using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformDragManager : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private new Camera camera;

    bool rotateSpriteHit;

    private void Awake()
    {
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
    }

    private void ShowTurnPlatformButton()
    {

    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            // check if rotatespritehit?
        }

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

            //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        }

    }

    void OnMouseDrag()
    {
        if (!rotateSpriteHit)
        {
            Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
            Vector3 curPosition = camera.ScreenToWorldPoint(curScreenPoint);// + offset;
            transform.position = curPosition;
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
               // InventoryButton button = null;
                bool platformDraggedToButton = false;

                for (int i = 0; i < GameState.Instance.UIManager.instantiatedInventoryButtons.Length; i++)
                {
                    GraphicRaycaster ray = GameState.Instance.UIManager.instantiatedInventoryButtons[i].GetComponent<GraphicRaycaster>();
                    PointerEventData pointerEventData = new PointerEventData(eventSystem);
                    pointerEventData.position = Input.mousePosition;
                    ray.Raycast(pointerEventData, results);

                    foreach (RaycastResult result in results)
                    {
                        //Debug.Log(result);
                        //if (result.gameObject.tag == "InventoryButton" || result.gameObject.name == "GarbageBinButton")
                        //{
                        platformDraggedToButton = true;
                        break;
                        //}
                    }

                    //if (tag == "PlatformSquare" && GameState.Instance.UIManager.instantiatedInventoryButtons[i].name == InventoryButtonName.platformSquareButton.ToString())
                    //{
                    //    button = GameState.Instance.UIManager.instantiatedInventoryButtons[i];
                    //}
                    //else if (tag == "Ramp" && GameState.Instance.UIManager.instantiatedInventoryButtons[i].name == InventoryButtonName.rampInventoryButton.ToString())
                    //{
                    //    button = GameState.Instance.UIManager.instantiatedInventoryButtons[i];
                    //}
                    //else if (tag == "Trampoline" && GameState.Instance.UIManager.instantiatedInventoryButtons[i].name == InventoryButtonName.trampolineButton.ToString())
                    //{
                    //    button = GameState.Instance.UIManager.instantiatedInventoryButtons[i];
                    //}
                    //else if (tag == "Booster" && GameState.Instance.UIManager.instantiatedInventoryButtons[i].name == InventoryButtonName.boostPlatformButton.ToString())
                    //{
                    //    button = GameState.Instance.UIManager.instantiatedInventoryButtons[i];
                    //}
                }

                if (platformDraggedToButton)
                {
                    GameState.Instance.buttonManager.UpdatePlayerPlatforms(gameObject);
                    // dragactive = false;?
                }
                else
                {
                    GameState.Instance.gridManager.RemoveFilledGridSpots(this.GetComponent<Platform>().fillsGridSpot);
                    GameState.Instance.platformManager.spawnPlatformOnGrid(transform.position, gameObject);
                    GameState.Instance.playerCamera.platformDragActive = false;
                }
            }
        }
    }

    private bool GarbageBinHit()
    {
        EventSystem eventSystem = GetComponent<EventSystem>();
        List<RaycastResult> results = new List<RaycastResult>();
        //InventoryButton button = null;
        //bool platformDraggedToButton = false;

        GraphicRaycaster ray = GameState.Instance.UIManager.canvas.gameObject.transform.GetChild(6).GetComponent<GraphicRaycaster>();
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        ray.Raycast(pointerEventData, results);

        foreach (RaycastResult result in results)
        {
            GameState.Instance.buttonManager.UpdatePlayerPlatforms(gameObject);
            return true;
        }
        return false;
    }
}