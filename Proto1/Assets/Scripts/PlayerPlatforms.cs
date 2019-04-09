using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatforms
{
    public GameObject platformSquare;
    public int platformSquares;
    public int platformSquaresLeftToPlace;
    public bool platformSquaresButtonInstantated;

    public GameObject ramp;
    public int ramps;
    public int rampsLeftToPlace;
    public bool rampButtonInstantiated;

    public GameObject trampoline;
    public int trampolines;
    public int trampolinesLeftToPlace;
    public bool trampolineButtonInstantiated;

    public int inventoryButtonAmmount;

    //private GameObject draggedPlatform;
    public List<GameObject> placedPlatforms;

    public PlayerPlatforms(int ramps, int platformSquares, int trampolines)
    {
        placedPlatforms = new List<GameObject>();

        this.ramps = ramps;
        this.platformSquares = platformSquares;
        this.trampolines = trampolines;
        rampsLeftToPlace = ramps;
        platformSquaresLeftToPlace = platformSquares;
        trampolinesLeftToPlace = trampolines;

        inventoryButtonAmmount = 0;

        if (ramps > 0)
            inventoryButtonAmmount++;
        if (platformSquares > 0)
            inventoryButtonAmmount++;
        if (trampolines > 0)
            inventoryButtonAmmount++;
    }

    public void UpdateRampsLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(1).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace + "/" + GameState.Instance.levelManager.playerPlatforms.ramps;
    }

    public void UpdatePlatformSquaresLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(1).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace + "/" + GameState.Instance.levelManager.playerPlatforms.platformSquares;
    }

    public void UpdateTrampolinesLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(1).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace + "/" + GameState.Instance.levelManager.playerPlatforms.trampolines;
    }

    //public GameObject InstantiatePlayerPlatform(GameObject inventoryButton)
    //{
    //    if (inventoryButton.name == "platformSquareButton")
    //    {
    //        draggedPlatform = Instantiate(platformSquare);
    //    }
    //    else if (inventoryButton.name == "rampInventoryButton")
    //    {
    //        Instantiate(ramp);
    //    }

    //    return draggedPlatform;
    //}
}
