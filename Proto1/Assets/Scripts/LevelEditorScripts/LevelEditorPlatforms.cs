using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorPlatforms
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

    public GameObject redZone;
    public int redZones;
    public int redZonesLeftToPlace;
    public bool redZoneButtonInstantiated;

    public GameObject ball;
    public int balls;
    public int ballsLeftToPlace;
    public bool ballButtonInstantiated;

    public int inventoryButtonAmmount;

    //private GameObject draggedPlatform;
    public List<GameObject> placedPlatforms = new List<GameObject>();

    public LevelEditorPlatforms(int balls,int ramps, int platformSquares, int trampolines, int boostPlatforms, int cannonPlatforms,int redZones)
    {
        placedPlatforms = new List<GameObject>();

        this.ramps = ramps;
        this.platformSquares = platformSquares;
        this.trampolines = trampolines;
        this.boostPlatforms = boostPlatforms;
        this.cannonPlatforms = cannonPlatforms;
        this.redZones = redZones;
        this.balls = balls;

        rampsLeftToPlace = ramps;
        platformSquaresLeftToPlace = platformSquares;
        trampolinesLeftToPlace = trampolines;
        boostPlatformsLeftToPlace = boostPlatforms;
        cannonPlatformsLeftToPlace = cannonPlatforms;
        redZonesLeftToPlace = redZones;
        ballsLeftToPlace = balls;

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
        if (redZones > 0)
            inventoryButtonAmmount++;
        if (balls > 0)
            inventoryButtonAmmount++;
    }

    //public void UpdateRampsLeft(InventoryButton button)
    //{
    //    Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
    //    buttonText.text = LevelEditorState.Instance.playerPlatforms.rampsLeftToPlace.ToString();
    //}

    //public void UpdatePlatformSquaresLeft(InventoryButton button)
    //{
    //    Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
    //    buttonText.text = LevelEditorState.Instance.playerPlatforms.rampsLeftToPlace.ToString();
    //}

    //public void UpdateTrampolinesLeft(InventoryButton button)
    //{
    //    Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
    //    buttonText.text = LevelEditorState.Instance.playerPlatforms.rampsLeftToPlace.ToString();
    //}

    //public void UpdateBoostPlatformsLeft(InventoryButton button)
    //{
    //    Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
    //    buttonText.text = LevelEditorState.Instance.playerPlatforms.rampsLeftToPlace.ToString();
    //}

    //public void UpdateCannonPlatformsLeft(InventoryButton button)
    //{
    //    Text buttonText = button.transform.GetChild(3).gameObject.GetComponent<Text>();
    //    buttonText.text = LevelEditorState.Instance.playerPlatforms.rampsLeftToPlace.ToString();
    //}

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
