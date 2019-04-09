using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlatformDragManager : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private Camera camera;

    bool rotateSpriteHit;

    //bool timerOn;
    //float time;


    //private void Update()
    //{
    //    if (timerOn)
    //    {
    //        Timer timer = new Timer(1000);
    //        time = Time.deltaTime;
    //        if (time > 100)
    //        {
    //            ShowTurnPlatformButton();
    //        }
    //    }
    //}

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
            EventSystem eventSystem = GetComponent<EventSystem>();
            List<RaycastResult> results = new List<RaycastResult>();
            InventoryButton button = null;
            bool platformDraggedToButton = false;
            for (int i = 0; i < GameState.Instance.UIManager.instantiatedInventoryButtons.Length; i++)
            {
                GraphicRaycaster ray = GameState.Instance.UIManager.instantiatedInventoryButtons[i].GetComponent<GraphicRaycaster>();
                PointerEventData pointerEventData = new PointerEventData(eventSystem);
                pointerEventData.position = Input.mousePosition;
                ray.Raycast(pointerEventData, results);

                foreach (RaycastResult result in results)
                {
                    if (result.gameObject.tag == "InventoryButton")
                    {
                        platformDraggedToButton = true;
                    }
                }
                if (tag == "PlatformSquare" && GameState.Instance.UIManager.instantiatedInventoryButtons[i].name == "platformSquareButton")
                {
                    button = GameState.Instance.UIManager.instantiatedInventoryButtons[i];

                }
                else if (tag == "Ramp" && GameState.Instance.UIManager.instantiatedInventoryButtons[i].name == "rampInventoryButton")
                {
                    button = GameState.Instance.UIManager.instantiatedInventoryButtons[i];

                }
            }

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
            GameState.Instance.platformManager.spawnPlatformOnGrid(transform.position, gameObject);
            GameState.Instance.playerCamera.platformDragActive = false;
        }
    }
}