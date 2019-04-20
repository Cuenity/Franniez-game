using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelEditorDragManager : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler//, IPointerDownHandler
{
    ////deze fields later verwijderen
    //public GameObject platformSquare;
    //public GameObject ramp;
    //public GameObject trampoline;
    //public GameObject boostPlatform;
    //public GameObject cannon;
    ////tot hier
    //public RotateSprite rotateSprite;



    //bool draggingAllowed;

    //private new Camera camera;

    //PlayerCamera playercamera;


    ////Vector3 clickOffset = Vector3.zero;

    //PlatformType type;

    //private void OnPointerDown()
    //{
    //    playercamera.platformDragActive = true;
    //}
    [SerializeField]
    RotateSprite rotateSprite;

    Camera camera;
    float zAxis = 0;
    PlatformType type;
    GameObject draggedPlatform;


    private void Start()
    {
        camera = GameObject.Find("LevelEditorCamera").GetComponent<Camera>();

        zAxis = transform.position.z;
    }
    //FUCK it helemaal anders it is
    private void Update()
    {
        
    }
    public void klikInventoryButton()
    {
        Debug.Log("ButtonKLICKED");
        //GameObject platformSquare = Instantiate(objectToSpawn);
    }

    private Vector3 ScreenPointToWorldOnPlane(Vector3 screenPosition, float zPosition)
    {

        Plane plane = new Plane(Vector3.forward, new Vector3(0, 0, zPosition));
        Ray rayCast = camera.ScreenPointToRay(screenPosition);
        plane.Raycast(rayCast, out float enterDist);
        return rayCast.GetPoint(enterDist);
    }

    public void OnBeginDrag(PointerEventData data)
    {
        //draggingAllowed = true;
        //playercamera.platformDragActive = true;

        InventoryButton correctButton = FindInventoryButton(data);
        if (correctButton != null)
        {
            if (correctButton.InventoryButtonAllowed)
            {
                if (correctButton.name == InventoryButtonName.platformSquareButton.ToString())
                {
                    type = PlatformType.platformSquare;
                    draggedPlatform = Instantiate(LevelEditorState.Instance.platformManager.platformSquareClass.gameObject);

                    //LevelEditorState.Instance.playerPlatforms.platformSquaresLeftToPlace--;
                    if (LevelEditorState.Instance.playerPlatforms.platformSquaresLeftToPlace == 0)
                    {
                        correctButton.InventoryButtonAllowed = false;
                    }
                    //LevelEditorState.Instance.playerPlatforms.UpdatePlatformSquaresLeft(correctButton);
                }
                else if (correctButton.name == InventoryButtonName.rampInventoryButton.ToString())
                {
                    type = PlatformType.ramp;
                    draggedPlatform = Instantiate(LevelEditorState.Instance.platformManager.rampClass.gameObject);
                    //LevelEditorState.Instance.playerPlatforms.rampsLeftToPlace--;

                    if (LevelEditorState.Instance.playerPlatforms.rampsLeftToPlace == 0)
                    {
                        correctButton.InventoryButtonAllowed = false;
                    }

                    //LevelEditorState.Instance.playerPlatforms.UpdateRampsLeft(correctButton);
                }
                else if (correctButton.name == InventoryButtonName.trampolineButton.ToString())
                {
                    type = PlatformType.trampoline;

                    draggedPlatform = Instantiate(LevelEditorState.Instance.platformManager.trampolineClass.gameObject);
                    //LevelEditorState.Instance.playerPlatforms.trampolinesLeftToPlace--;

                    if (LevelEditorState.Instance.playerPlatforms.trampolinesLeftToPlace == 0)
                    {
                        correctButton.InventoryButtonAllowed = false;
                    }

                    //LevelEditorState.Instance.playerPlatforms.UpdateTrampolinesLeft(correctButton);
                }
                else if (correctButton.name == InventoryButtonName.boostPlatformButton.ToString())
                {
                    type = PlatformType.boostPlatform;

                    draggedPlatform = Instantiate(LevelEditorState.Instance.platformManager.boostPlatformClass.gameObject);
                    //LevelEditorState.Instance.playerPlatforms.boostPlatformsLeftToPlace--;

                    if (LevelEditorState.Instance.playerPlatforms.boostPlatformsLeftToPlace == 0)
                    {
                        correctButton.InventoryButtonAllowed = false;
                    }

                    //LevelEditorState.Instance.playerPlatforms.UpdateBoostPlatformsLeft(correctButton);
                }
                else if (correctButton.name == InventoryButtonName.cannonPlatformButton.ToString())
                {
                    type = PlatformType.cannon;

                    draggedPlatform = Instantiate(LevelEditorState.Instance.platformManager.cannonClass.gameObject);
                    //LevelEditorState.Instance.playerPlatforms.cannonPlatformsLeftToPlace--;

                    if (LevelEditorState.Instance.playerPlatforms.cannonPlatformsLeftToPlace == 0)
                    {
                        correctButton.InventoryButtonAllowed = false;
                    }

                    //LevelEditorState.Instance.playerPlatforms.UpdateCannonPlatformsLeft(correctButton);

                    draggedPlatform.transform.GetChild(0).transform.GetChild(0).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                    draggedPlatform.transform.GetChild(1).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                    draggedPlatform.transform.GetChild(2).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                    draggedPlatform.transform.GetChild(3).gameObject.AddComponent<LevelEditorPlatformDragManager>();
                }
                else if (correctButton.name == InventoryButtonName.redZoneButton.ToString())
                {
                    type = PlatformType.cannon;

                    draggedPlatform = Instantiate(LevelEditorState.Instance.platformManager.redZoneClass.gameObject);
                    //LevelEditorState.Instance.playerPlatforms.cannonPlatformsLeftToPlace--;

                    if (LevelEditorState.Instance.playerPlatforms.redZonesLeftToPlace == 0)
                    {
                        correctButton.InventoryButtonAllowed = false;
                    }

                    //LevelEditorState.Instance.playerPlatforms.UpdateCannonPlatformsLeft(correctButton);

                    draggedPlatform.transform.gameObject.AddComponent<LevelEditorPlatformDragManager>();
                }
                

                if (!draggedPlatform.GetComponent<Cannon>())
                {
                    var outline = draggedPlatform.AddComponent<Outline>();
                    outline.OutlineMode = Outline.Mode.OutlineAll;
                    outline.OutlineColor = Color.blue;
                    outline.OutlineWidth = 10f;

                    draggedPlatform.AddComponent<LevelEditorPlatformDragManager>();
                }
            }
            else
            {
                //draggingAllowed = false;
                //playercamera.platformDragActive = false;
            }
        }
    }

    private InventoryButton FindInventoryButton(PointerEventData data)
    {
        GameObject buttonFromRaycast = null;
        if (data.pointerPressRaycast.gameObject.tag == "InventoryButton")
        {
            buttonFromRaycast = data.pointerPressRaycast.gameObject;
        }
        else
        {
            buttonFromRaycast = data.pointerPressRaycast.gameObject.transform.parent.gameObject;
        }

        InventoryButton correctButton = null;
        foreach (InventoryButton button in LevelEditorState.Instance.UIManager.instantiatedInventoryButtons)
        {
            if (button.name == buttonFromRaycast.name)
            {
                correctButton = button;
                return correctButton;
            }
        }
        return correctButton;
    }


    public void OnDrag(PointerEventData eventData)
    {
        //if (draggingAllowed)
        //{
            draggedPlatform.transform.position = ScreenPointToWorldOnPlane(eventData.position, zAxis); //+ clickOffset; 
        //}
    }


    public void OnEndDrag(PointerEventData eventData)
    {
        //if (draggingAllowed)
        //{
        //    draggedPlatform.AddRotateSpriteIfNeeded();
        //}

        //if (draggingAllowed) // replace this with the if above when there is an abstract parent class for platforms
        //{
            if (type == PlatformType.ramp)
            {
                RotateSprite sprite = Instantiate(rotateSprite);
                sprite.type = PlatformType.ramp;

                sprite.transform.SetParent(draggedPlatform.transform); //x positie: 0,0200 (202) scale: 0.001 bij 0.0005
                sprite.transform.localScale = new Vector3(0.0015f, 0.00075f, 0);
                //rotateSprite.transform.Rotate(new Vector3(0, 90, 0));
                sprite.transform.position = draggedPlatform.transform.position + new Vector3(0, -0.9f, -0.51f); //new Vector3(1, 0, -2);

                sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                //rotateSprite.AddComponent<MeshCollider>();
            }

            LevelEditorState.Instance.playerPlatforms.placedPlatforms.Add(draggedPlatform);

            Vector3 pos = camera.ScreenToWorldPoint(Input.mousePosition);

            LevelEditorState.Instance.platformManager.spawnPlatformOnGrid(draggedPlatform.transform.position, draggedPlatform);
            //pointless in leveleditor????
            //StartCoroutine(CorutineDragActive());
            //coroutine wacht een frame
        //}
    }

    //public IEnumerator CorutineDragActive()
    //{
    //    yield return new WaitForEndOfFrame();
    //    GameState.Instance.playerCamera.platformDragActive = false;
    //}
}