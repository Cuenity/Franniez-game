using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelEditorPlatformDragManager : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;

    private new Camera camera;
    private GameObject draggedPlatformInScene;

    bool rotateSpriteHit;

    private void Awake()
    {
        camera = GameObject.Find("LevelEditorCamera").GetComponent<Camera>();
    }

    
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
            //Dit doet vast coole shit maar ik heb het nodig om tilelist op een cristelijke manier bij te houden
            // huidige trukje werkt alleen voor platformen die geen adjustments hebben
            //int tileListIndex = LevelEditorState.Instance.gridManager.gridSquares.FindIndex(x => x == draggedPlatformInScene.transform.position);
            // DUS aparte methodes voor dingen die adjustments hebben
            // eerst:redzones
            // TWEEDS:CANNON
            //3rde start
            //4de finish
            screenPoint = camera.WorldToScreenPoint(transform.position);

            //GameState.Instance.playerCamera.platformDragActive = true;

            if (tag == "Cannon")
            {
                draggedPlatformInScene = gameObject.transform.root.gameObject;
                int tileListIndex = LevelEditorState.Instance.gridManager.gridSquares.FindIndex(x => x == (draggedPlatformInScene.transform.position + new Vector3(0.5f, 0, 0)));
                LevelEditorState.Instance.levelPlatformen.tileList[tileListIndex - 1] = 0;
            }
            else if (gameObject.name.Contains("RedZone") || gameObject.name.Contains("Star"))
            {
                draggedPlatformInScene = gameObject;
                int tileListIndex = LevelEditorState.Instance.gridManager.gridSquares.FindIndex(x => x == (draggedPlatformInScene.transform.position+new Vector3(0.5f,0,0)));
                LevelEditorState.Instance.levelPlatformen.tileList[tileListIndex - 1] = 0;
                
            }
            else if (gameObject.name.Contains("Vlaggetje"))
            {
                draggedPlatformInScene = gameObject;
                int tileListIndex = LevelEditorState.Instance.gridManager.gridSquares.FindIndex(x => x == (draggedPlatformInScene.transform.position + new Vector3(0.5f, 0.5f, 0)));
                LevelEditorState.Instance.levelPlatformen.tileList[tileListIndex - 1] = 0;
            }
            else
            {
                draggedPlatformInScene = gameObject;
                int tileListIndex = LevelEditorState.Instance.gridManager.gridSquares.FindIndex(x => x == draggedPlatformInScene.transform.position);
                LevelEditorState.Instance.levelPlatformen.tileList[tileListIndex-1] = 0;
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
            
                EventSystem eventSystem = GetComponent<EventSystem>();
                List<RaycastResult> results = new List<RaycastResult>();
                bool platformDraggedToButton = false;

                for (int i = 0; i < LevelEditorState.Instance.UIManager.instantiatedInventoryButtons.Length; i++)
                {
                    GraphicRaycaster ray = LevelEditorState.Instance.UIManager.instantiatedInventoryButtons[i].GetComponent<GraphicRaycaster>();
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
                    GameObject.Destroy(draggedPlatformInScene);
                }
                else
                {
                    //RemoveFilledGridSpots();
                    LevelEditorState.Instance.platformManager.spawnPlatformOnGrid(draggedPlatformInScene.transform.position, draggedPlatformInScene);
                    //GameState.Instance.playerCamera.platformDragActive = false;
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
            RemoveFilledGridSpots();
            //GameState.Instance.buttonManager.UpdatePlayerPlatforms(draggedPlatformInScene);
            draggedPlatformInScene.GetComponent<Platform>().UpdatePlayerPlatforms();
            return true;
        }

        //foreach (RaycastResult result in results)
        //{
        //    GameState.Instance.buttonManager.UpdatePlayerPlatforms(gameObject);
        //    return true;
        //}
        return false;
    }

    private void RemoveFilledGridSpots()
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