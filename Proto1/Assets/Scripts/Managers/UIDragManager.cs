using UnityEngine;
using UnityEngine.EventSystems;

public class UIDragManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler//, IPointerDownHandler
{
    //deze fields later verwijderen
    public GameObject platformSquare;
    public GameObject ramp;
    public GameObject trampoline;
    //tot hier
    public RotateSprite rotateSprite;


    GameObject draggedPlatform;
    bool draggingAllowed;


    private new Camera camera;
    PlatformManager platformManager;
    PlayerCamera playercamera;


    float zAxis = 0;
    //Vector3 clickOffset = Vector3.zero;

    PlatformType type;

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
        draggingAllowed = true;

        playercamera = GameState.Instance.playerCamera;
        camera = GameState.Instance.playerCamera.GetComponent<Camera>();
        playercamera.platformDragActive = true;

        platformManager = GameState.Instance.platformManager.GetComponent<PlatformManager>();

        GameObject inventoryButton = data.pointerPressRaycast.gameObject.transform.parent.gameObject;

        if (inventoryButton)
        {
            if (inventoryButton.name == "platformSquareButton")
            {
                type = PlatformType.platformSquare;
                foreach (InventoryButton button in GameState.Instance.UIManager.instantiatedInventoryButtons)
                {
                    if (button.name == inventoryButton.name)
                    {
                        if (button.InventoryButtonAllowed)
                        {
                            draggedPlatform = Instantiate(platformSquare);
                            GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace--;

                            if (GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace == 0)
                            {
                                button.InventoryButtonAllowed = false;
                            }

                            GameState.Instance.levelManager.playerPlatforms.UpdatePlatformSquaresLeft(button);

                            var outline = draggedPlatform.AddComponent<Outline>();
                            outline.OutlineMode = Outline.Mode.OutlineAll;
                            outline.OutlineColor = Color.blue;
                            outline.OutlineWidth = 10f;

                            draggedPlatform.AddComponent<PlatformDragManager>();
                        }
                        else
                        {
                            draggingAllowed = false;
                            playercamera.platformDragActive = false;
                        }
                    }
                }
            }
            else if (inventoryButton.name == "rampInventoryButton")
            {
                type = PlatformType.ramp;
                foreach (InventoryButton button in GameState.Instance.UIManager.instantiatedInventoryButtons)
                {
                    if (button.name == inventoryButton.name)
                    {
                        if (button.InventoryButtonAllowed)
                        {
                            draggedPlatform = Instantiate(ramp);
                            GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace--;

                            if (GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace == 0)
                            {
                                button.InventoryButtonAllowed = false;
                            }

                            GameState.Instance.levelManager.playerPlatforms.UpdateRampsLeft(button);

                            var outline = draggedPlatform.AddComponent<Outline>();
                            outline.OutlineMode = Outline.Mode.OutlineAll;
                            outline.OutlineColor = Color.blue;
                            outline.OutlineWidth = 10f;

                            draggedPlatform.AddComponent<PlatformDragManager>();
                        }
                        else
                        {
                            draggingAllowed = false;
                            playercamera.platformDragActive = false;
                        }
                    }
                }
            }
            else if (inventoryButton.name == "trampolineButton")
            {
                type = PlatformType.trampoline;
                foreach (InventoryButton button in GameState.Instance.UIManager.instantiatedInventoryButtons)
                {
                    if (button.name == inventoryButton.name)
                    {
                        if (button.InventoryButtonAllowed)
                        {
                            draggedPlatform = Instantiate(trampoline);
                            GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace--;

                            if (GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace == 0)
                            {
                                button.InventoryButtonAllowed = false;
                            }

                            GameState.Instance.levelManager.playerPlatforms.UpdateTrampolinesLeft(button);

                            var outline = draggedPlatform.AddComponent<Outline>();
                            outline.OutlineMode = Outline.Mode.OutlineAll;
                            outline.OutlineColor = Color.blue;
                            outline.OutlineWidth = 10f;

                            draggedPlatform.AddComponent<PlatformDragManager>();
                        }
                        else
                        {
                            draggingAllowed = false;
                            playercamera.platformDragActive = false;
                        }
                    }
                }
            }
        }

        //else
        //{
        //    Debug.Log(data.pointerPressRaycast.gameObject);
        //    draggedPlatform = data.pointerPressRaycast.gameObject;
        //}



        //draggedPlatform = GameState.Instance.levelManager.playerPlatforms.InstantiatePlayerPlatform(inventoryButton);
    }



    public void OnDrag(PointerEventData eventData)
    {
        if (draggingAllowed)
        {
            draggedPlatform.transform.position = ScreenPointToWorldOnPlane(eventData.position, zAxis); //+ clickOffset; 
        }
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        if (draggingAllowed)
        {
            if (type == PlatformType.ramp)
            {
                RotateSprite sprite = Instantiate(rotateSprite);
                sprite.type = PlatformType.ramp;

                sprite.transform.SetParent(draggedPlatform.transform); //x positie: 0,0200 (202) scale: 0.001 bij 0.0005
                sprite.transform.localScale = new Vector3(0.001f, 0.0005f, 0);
                //rotateSprite.transform.Rotate(new Vector3(0, 90, 0));
                sprite.transform.position = draggedPlatform.transform.position + new Vector3(0, 0, -1.01f); //new Vector3(1, 0, -2);

                sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                //rotateSprite.AddComponent<MeshCollider>();
            }

            GameState.Instance.levelManager.playerPlatforms.placedPlatforms.Add(draggedPlatform);

            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);

            platformManager.spawnPlatformOnGrid(draggedPlatform.transform.position, draggedPlatform);

            GameState.Instance.playerCamera.platformDragActive = false;
        }
    }
}