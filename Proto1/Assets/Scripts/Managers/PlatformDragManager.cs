using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformDragManager : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private Camera camera;

    void OnMouseDown()
    {
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        screenPoint = camera.WorldToScreenPoint(transform.position);

        GameState.Instance.playerCamera.platformDragActive = true;

        //offset = transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
    }

    void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z);
        Vector3 curPosition = camera.ScreenToWorldPoint(curScreenPoint);// + offset;
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        EventSystem eventSystem = GetComponent<EventSystem>();
        List<RaycastResult> results = new List<RaycastResult>();
        InventoryButton button = null;
        bool platformDraggedToButton = false;
        bool foundButton = false;
        for (int i = 0; i < GameState.Instance.UIManager.instantiatedInventoryButtons.Length; i++)
        {
            if (!foundButton)
            {
                GraphicRaycaster ray = GameState.Instance.UIManager.instantiatedInventoryButtons[i].GetComponent<GraphicRaycaster>();
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;
                ray.Raycast(pointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    Debug.Log(result);
                    if (result.gameObject.tag == "InventoryButton")
                    {
                        button = GameState.Instance.UIManager.instantiatedInventoryButtons[i];
                        platformDraggedToButton = true;

                        foundButton = true;
                    }
                }
            }
        }
        //ray = button.GetComponent<GraphicRaycaster>(); 
        ////GraphicRaycaster ray2 = GameState.Instance.UIManager.instantiatedInventoryButtons[1].GetComponent<GraphicRaycaster>();

        //PointerEventData pointerEventData = new PointerEventData(eventSystem);
        //pointerEventData.position = Input.mousePosition;
        //List<RaycastResult> results = new List<RaycastResult>();

        //ray.Raycast(pointerEventData, results);
        //foreach (RaycastResult result in results)
        //{
        //    if (result.gameObject.tag == "InventoryButton")
        //    {
        //foreach (InventoryButton button in GameState.Instance.UIManager.instantiatedInventoryButtons)
        //{
        if (platformDraggedToButton)
        {
            if (tag == "PlatformSquare")
            {
                GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace++;
                GameState.Instance.levelManager.playerPlatforms.UpdatePlatformSquaresLeft(button);
                button.InventoryButtonAllowed = true;
            }
            else if (tag == "Ramp")
            {
                GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace++;
                GameState.Instance.levelManager.playerPlatforms.UpdateRampsLeft(button);
                button.InventoryButtonAllowed = true;
            }
            GameState.Instance.levelManager.playerPlatforms.placedPlatforms.Remove(gameObject);
            Destroy(gameObject);
        }
                //}


        //    }
        //}
        //}

        GameState.Instance.platformManager.spawnPlatformOnGrid(transform.position, gameObject);
        GameState.Instance.playerCamera.platformDragActive = false;
    }
}