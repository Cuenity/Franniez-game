using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas UICanvas;
    public Canvas canvas;
    public Canvas pauseMenuCanvas;

    [SerializeField]
    private InventoryButton inventoryButton;
    [SerializeField]
    private SquareInventoryButton squareInventoryButton;
    [SerializeField]
    private RampInventoryButton rampInventoryButton;
    [SerializeField]
    private TrampolineInventoryButton trampolineInventoryButton;
    [SerializeField]
    private BoosterInventoryButton boosterInventoryButton;
    [SerializeField]
    private CannonInventoryButton cannonInventoryButton;

    public Sprite rampImage;
    public Sprite platformSquareImage;
    public Sprite trampolineImage;
    public Sprite boostPlatformImage;
    public Sprite cannonPlatformImage;
    public Sprite normalBallImage;
    public GameObject tutorialArrow;

    public InventoryButton[] instantiatedInventoryButtons;

    public bool newLevelInventoryisRequired = false;
    public Canvas pauseMenu;

    //multiPrefabs
    public Image p1FlagHit, p2FlagHit;
    [SerializeField]
    Canvas multiplayerCanvas;
    [SerializeField]
    Sprite flagIsHit, flagIsNotHit;

    public void InitInventoryButtons(PlayerPlatforms playerPlatforms)
    {
        pauseMenu = Instantiate(pauseMenuCanvas);
        pauseMenu.enabled = false;
        InstantiateInventoryButtonsCheck(playerPlatforms.InventoryButtonAmmount);

        if (instantiatedInventoryButtons[0] == null)
        {
            int buttonDistance = Screen.width / (playerPlatforms.InventoryButtonAmmount + 1);
            int buttonHeight = Screen.height / 8;

            int index = 0;
            if (playerPlatforms.platformSquares > 0)
            {
                instantiatedInventoryButtons[index] = Instantiate(squareInventoryButton);
                instantiatedInventoryButtons[index].transform.SetParent(canvas.transform);
                instantiatedInventoryButtons[index].transform.position = new Vector3(buttonDistance * (index + 1), buttonHeight, 0);
                instantiatedInventoryButtons[index].SetCorrectTextAndImageForInventoryButton(playerPlatforms.platformSquares.ToString());

                index++;
            }

            if (playerPlatforms.ramps > 0)
            {
                instantiatedInventoryButtons[index] = Instantiate(rampInventoryButton);
                instantiatedInventoryButtons[index].transform.SetParent(canvas.transform);
                instantiatedInventoryButtons[index].transform.position = new Vector3(buttonDistance * (index + 1), buttonHeight, 0);
                instantiatedInventoryButtons[index].SetCorrectTextAndImageForInventoryButton(playerPlatforms.ramps.ToString());

                index++;
            }

            if (playerPlatforms.trampolines > 0)
            {
                instantiatedInventoryButtons[index] = Instantiate(trampolineInventoryButton);
                instantiatedInventoryButtons[index].transform.SetParent(canvas.transform);
                instantiatedInventoryButtons[index].transform.position = new Vector3(buttonDistance * (index + 1), buttonHeight, 0);
                instantiatedInventoryButtons[index].SetCorrectTextAndImageForInventoryButton(playerPlatforms.trampolines.ToString());

                index++;
            }

            if (playerPlatforms.boostPlatforms > 0)
            {
                instantiatedInventoryButtons[index] = Instantiate(boosterInventoryButton);
                instantiatedInventoryButtons[index].transform.SetParent(canvas.transform);
                instantiatedInventoryButtons[index].transform.position = new Vector3(buttonDistance * (index + 1), buttonHeight, 0);
                instantiatedInventoryButtons[index].SetCorrectTextAndImageForInventoryButton(playerPlatforms.boostPlatforms.ToString());

                index++;
            }

            if (playerPlatforms.cannonPlatforms > 0)
            {
                instantiatedInventoryButtons[index] = Instantiate(cannonInventoryButton);
                instantiatedInventoryButtons[index].transform.SetParent(canvas.transform);
                instantiatedInventoryButtons[index].transform.position = new Vector3(buttonDistance * (index + 1), buttonHeight, 0);
                instantiatedInventoryButtons[index].SetCorrectTextAndImageForInventoryButton(playerPlatforms.cannonPlatforms.ToString());

                index++;
            }
        }

        else
        {
            foreach (InventoryButton buttonToActivate in instantiatedInventoryButtons)
            {
                buttonToActivate.gameObject.SetActive(true);
            }
        }
    }

    private void InstantiateInventoryButtonsCheck(int inventoryButtonAmmount)
    {
        bool check = instantiatedInventoryButtons.Length > 0;
        if (instantiatedInventoryButtons == null || !check)
        {
            instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
        }
        if (newLevelInventoryisRequired == true)
        {
            instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
            newLevelInventoryisRequired = false;
        }
    }

    public void DeactivateInventoryButtons()
    {
        if (instantiatedInventoryButtons != null)
        {
            foreach (InventoryButton buttonToDeactivate in instantiatedInventoryButtons)
            {
                buttonToDeactivate.gameObject.SetActive(false);
            }
        }
    }

    public void ActivateGarbageBinButton()
    {
        canvas.gameObject.transform.GetChild(6).gameObject.SetActive(true);
    }

    public void DeactivateGarbageBinButton()
    {
        canvas.gameObject.transform.GetChild(6).gameObject.SetActive(false);
    }

    public void AddMultiplayerUI()
    {
        multiplayerCanvas.gameObject.SetActive(true);
        foreach (Button button in GameState.Instance.UIManager.canvas.GetComponentsInChildren<Button>())
        {
            if (button.name == "MenuButton")
            {
                button.gameObject.SetActive(true);
            }
        }
        Image[] flaghitImages = multiplayerCanvas.GetComponentsInChildren<Image>();
        p1FlagHit = flaghitImages[0];
        p2FlagHit = flaghitImages[1];
    }
    public void ChangeFlagHitTrue(Image flagtoChange)
    {
        //flagtoChange.sprite = flagIsHit;
    }
    public void ChangeFlagHitFalse(Image flagtoChange)
    {
        //flagtoChange.sprite = flagIsNotHit;
    }

    public bool GarbageBinHit(GameObject draggedPlatformInScene)
    {
        EventSystem eventSystem = GetComponent<EventSystem>();
        List<RaycastResult> results = new List<RaycastResult>();

        GraphicRaycaster ray = canvas.gameObject.transform.GetChild(6).GetComponent<GraphicRaycaster>();
        PointerEventData pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        ray.Raycast(pointerEventData, results);

        if (results.Count > 0)
        {
            RemoveOldFilledGridSpots(draggedPlatformInScene);
            //gameState.buttonManager.UpdatePlayerPlatforms(draggedPlatformInScene);
            draggedPlatformInScene.GetComponent<Platform>().UpdatePlayerPlatforms();
            return true;
        }

        return false;
    }

    public void RemoveOldFilledGridSpots(GameObject draggedPlatformInScene)
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

    public bool PlatformDraggedToButton()
    {
        EventSystem eventSystem = GetComponent<EventSystem>();
        List<RaycastResult> results = new List<RaycastResult>();
        bool platformDraggedToButton = false;

        for (int i = 0; i < instantiatedInventoryButtons.Length; i++)
        {
            GraphicRaycaster ray = instantiatedInventoryButtons[i].GetComponent<GraphicRaycaster>();
            PointerEventData pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            ray.Raycast(pointerEventData, results);

            if (results.Count > 0)
            {
                platformDraggedToButton = true;
            }
        }

        return platformDraggedToButton;
    }
}
