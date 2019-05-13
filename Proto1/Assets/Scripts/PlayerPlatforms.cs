using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerPlatforms
{
    private GameState gameState;

    public GameObject platformSquare;
    public int platformSquares;
    public int platformSquaresLeftToPlace;

    public GameObject ramp;
    public int ramps;
    public int rampsLeftToPlace;

    public GameObject trampoline;
    public int trampolines;
    public int trampolinesLeftToPlace;

    public GameObject boostPlatform;
    public int boostPlatforms;
    public int boostPlatformsLeftToPlace;

    public GameObject cannonPlatform;
    public int cannonPlatforms;
    public int cannonPlatformsLeftToPlace;

    private int inventoryButtonAmmount;
    public int InventoryButtonAmmount
    {
        get { return inventoryButtonAmmount; }
    }
    
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

        CalculateAndSetInventoryButtonAmmount();

        gameState = GameState.Instance;
    }

    private void CalculateAndSetInventoryButtonAmmount()
    {
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
        Text buttonText = button.transform.Find("PlatformAmmount").GetComponent<Text>();
        buttonText.text = gameState.levelManager.playerPlatforms.rampsLeftToPlace.ToString();
    }

    public void UpdatePlatformSquaresLeft(InventoryButton button)
    {
        Text buttonText = button.transform.Find("PlatformAmmount").GetComponent<Text>();
        buttonText.text = gameState.levelManager.playerPlatforms.platformSquaresLeftToPlace.ToString();
    }

    public void UpdateTrampolinesLeft(InventoryButton button)
    {
        Text buttonText = button.transform.Find("PlatformAmmount").GetComponent<Text>();
        buttonText.text = gameState.levelManager.playerPlatforms.trampolinesLeftToPlace.ToString();
    }

    public void UpdateBoostPlatformsLeft(InventoryButton button)
    {
        Text buttonText = button.transform.Find("PlatformAmmount").GetComponent<Text>();
        buttonText.text = gameState.levelManager.playerPlatforms.boostPlatformsLeftToPlace.ToString();
    }

    public void UpdateCannonPlatformsLeft(InventoryButton button)
    {
        Text buttonText = button.transform.Find("PlatformAmmount").GetComponent<Text>();
        buttonText.text = gameState.levelManager.playerPlatforms.cannonPlatformsLeftToPlace.ToString();
    }
}
