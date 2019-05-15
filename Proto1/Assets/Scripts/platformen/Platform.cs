using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Platform : MonoBehaviour
{
    public int fillsGridSpot;

    public abstract void UpdatePlayerPlatforms();

    protected InventoryButton FindCorrectInventoryButton(InventoryButtonName inventoryButtonName)
    {
        foreach (InventoryButton inventoryButton in GameState.Instance.UIManager.instantiatedInventoryButtons)
        {
            if (inventoryButton.name == inventoryButtonName.ToString())
            {
                return inventoryButton;
            }
        }
        return null;
    }
}
