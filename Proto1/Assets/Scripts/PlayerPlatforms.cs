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

    public GameObject boostPlatform;
    public int boostPlatforms;
    public int boostPlatformsLeftToPlace;
    public bool boostPlatformButtonInstantiated;

    public GameObject cannonPlatform;
    public int cannonPlatforms;
    public int cannonPlatformsLeftToPlace;
    public bool cannonPlatformButtonInstantiated;

    public int inventoryButtonAmmount;

    //private GameObject draggedPlatform;
    public List<GameObject> placedPlatforms = new List<GameObject>();

    public PlayerPlatforms(int ramps, int platformSquares, int trampolines, int boostPlatforms, int cannonPlatforms)
    {
        placedPlatforms = new List<GameObject>();

        this.ramps = ramps;
        this.platformSquares = platformSquares;
        this.trampolines = trampolines;
        this.boostPlatforms = boostPlatforms;
        this.cannonPlatforms = cannonPlatforms;
        rampsLeftToPlace = ramps;
        platformSquaresLeftToPlace = platformSquares;
        trampolinesLeftToPlace = trampolines;
        boostPlatformsLeftToPlace = boostPlatforms;
        cannonPlatformsLeftToPlace = cannonPlatforms;

        inventoryButtonAmmount = 0;
        if (ramps > 0)
            inventoryButtonAmmount++;
        if (platformSquares > 0)
            inventoryButtonAmmount++;
        if (trampolines > 0)
            inventoryButtonAmmount++;
        if (boostPlatforms > 0)
            inventoryButtonAmmount++;
        if (cannonPlatforms > 0)
            inventoryButtonAmmount++;
    }

    public void UpdateRampsLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace.ToString();
    }

    public void UpdatePlatformSquaresLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace.ToString();
    }

    public void UpdateTrampolinesLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.trampolinesLeftToPlace.ToString();
    }

    public void UpdateBoostPlatformsLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.boostPlatformsLeftToPlace.ToString();
    }

    public void UpdateCannonPlatformsLeft(InventoryButton button)
    {
        Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
        buttonText.text = GameState.Instance.levelManager.playerPlatforms.cannonPlatformsLeftToPlace.ToString();
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
