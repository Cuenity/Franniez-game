using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatforms: MonoBehaviour
{
    public GameObject platformSquare;
    public GameObject ramp;

    public bool rampButtonInstantiated;
    public bool platformSquaresButtonInstantated;

    public int ramps;
    public int platformSquares;
    public int inventoryButtonAmmount;

    //private GameObject draggedPlatform;
    public List<GameObject> placedPlatforms;

    public PlayerPlatforms(int ramps, int platformSquares)
    {
        placedPlatforms = new List<GameObject>();

        this.ramps = ramps;
        this.platformSquares = platformSquares;

        inventoryButtonAmmount = 0;

        if (ramps > 0)
            inventoryButtonAmmount = inventoryButtonAmmount + 1;
        if (platformSquares > 0)
            inventoryButtonAmmount = inventoryButtonAmmount + 1;

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
