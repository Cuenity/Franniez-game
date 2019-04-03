using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public GameObject inventoryButton;
    private GameObject[] instantiatedInventoryButtons;

    void Start()
    {
        Instantiate(canvas);
    }

    void Update()
    {

    }

    public void InventoryButtons(int inventoryButtonAmmount)
    {
        bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons == null;
        if (instantiatedInventoryButtonsArrayNotInstantiated)        
            instantiatedInventoryButtons = new GameObject[inventoryButtonAmmount];

        bool instantiatedInventoryButtonsAlreadyInstantiated = instantiatedInventoryButtons[0] != null;
        if (instantiatedInventoryButtonsAlreadyInstantiated)
        {
            foreach (GameObject buttonToActivate in instantiatedInventoryButtons)
            {
                buttonToActivate.SetActive(true);
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
            foreach (GameObject buttonToDeactivate in instantiatedInventoryButtons)
            {
                buttonToDeactivate.SetActive(false);
            }
        }
    }
}
