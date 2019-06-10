using UnityEngine;
using UnityEngine.UI;



[RequireComponent(typeof(GameObject))]
public class LevelEditorInventoryButton : MonoBehaviour
{
    private bool inventoryButtonAllowed;
    public bool InventoryButtonAllowed
    {
        get
        {
            return inventoryButtonAllowed;
        }
        set
        {
            inventoryButtonAllowed = value;
            Image inventoryButtonImage = transform.GetChild(0).gameObject.GetComponent<Image>();
            Color transparentColor = inventoryButtonImage.color;

            if (inventoryButtonAllowed)
            {
                transparentColor.a = 1f;
            }
            else
            {
                transparentColor.a = 0.47f;
            }

            inventoryButtonImage.color = transparentColor;
        }
    }

    public void Awake()
    {
        inventoryButtonAllowed = true;
    }

}
