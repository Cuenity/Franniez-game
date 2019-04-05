using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatforms
{
    public bool rampButtonInstantiated;
    public bool platformSquaresButtonInstantated;

    public int ramps;
    public int platformSquares;
    public int inventoryButtonAmmount;

    public PlayerPlatforms(int ramps, int platformSquares)
    {
        this.ramps = ramps;
        this.platformSquares = platformSquares;

        inventoryButtonAmmount = 0;

        if (ramps > 0)
            inventoryButtonAmmount = inventoryButtonAmmount +1;
        if (platformSquares > 0)
            inventoryButtonAmmount = inventoryButtonAmmount + 1;

    }
}
