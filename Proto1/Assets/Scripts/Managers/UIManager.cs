using UnityEngine;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public InventoryButton inventoryButton;
    //public InventoryButton inventoryButton;

    void Start()
    {
        Instantiate(canvas);
    }

    void Update()
    {

    }

    public void InventoryButtons(int inventoryButtonAmmount)
    {
        //inventoryButton = gameObject.AddComponent<InventoryButton>();

        GameObject uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");

        int buttonDistance = Screen.width / inventoryButtonAmmount;

        for (int i = 0; i < inventoryButtonAmmount; i++)
        {
            inventoryButton = Instantiate(inventoryButton);
            inventoryButton.transform.SetParent(uiCanvas.transform);
            inventoryButton.transform.position = new Vector3(buttonDistance * (i+1), 10, 0);
        }
        

        
    }
}
