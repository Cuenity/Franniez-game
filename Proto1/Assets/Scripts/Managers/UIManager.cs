using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public InventoryButton inventoryButton;
    private InventoryButton[] instantiatedInventoryButtons;

    void Start()
    {
        //Instantiate(Resources.Load<TextAsset>("Text/textFile01");
    }

    void Update()
    {

    }

    public void InventoryButtons(int inventoryButtonAmmount)
    {
        bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons == null;
        if (instantiatedInventoryButtonsArrayNotInstantiated)        
            instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];

        bool instantiatedInventoryButtonsAlreadyInstantiated = instantiatedInventoryButtons[0] != null;
        if (instantiatedInventoryButtonsAlreadyInstantiated)
        {
            foreach (InventoryButton buttonToActivate in instantiatedInventoryButtons)
            {
                buttonToActivate.gameObject.SetActive(true);
            }
        }

        else
        {
            GameObject uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");

            int buttonDistance = Screen.width / (inventoryButtonAmmount + 1);
            int buttonHeight = Screen.height / 8;

            for (int i = 0; i < inventoryButtonAmmount; i++)
            {
                inventoryButton = Instantiate(inventoryButton);
                inventoryButton.transform.SetParent(uiCanvas.transform);
                inventoryButton.transform.position = new Vector3(buttonDistance * (i + 1), buttonHeight, 0);
                
                instantiatedInventoryButtons[i] = inventoryButton;
            }
        }
    }

    public void RemoveInventoryButtons()
    {
        //instantiatedInventoryButtons = GameObject.FindGameObjectsWithTag("InventoryButton");
        if (instantiatedInventoryButtons != null)
        {
            foreach (InventoryButton buttonToDeactivate in instantiatedInventoryButtons)
            {
                buttonToDeactivate.gameObject.SetActive(false);
            }
        }
    }
}
