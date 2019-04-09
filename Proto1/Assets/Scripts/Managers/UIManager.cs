using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public InventoryButton inventoryButton;
    public InventoryButton[] instantiatedInventoryButtons;

    public Sprite rampImage;
    public Sprite platformSquareImage;
    public Sprite trampolineImage;
    public Sprite boostPlatformImage;

    void Start()
    {
        //Instantiate(canvas);
    }

    void Update()
    {

    }

    public void InventoryButtons(PlayerPlatforms playerPlatforms)
    {
        InstantiateInventoryButtonsCheck(playerPlatforms.inventoryButtonAmmount);

        bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons[0] == null;
        if (instantiatedInventoryButtonsArrayNotInstantiated)
        {
            GameObject uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");

            int buttonDistance = Screen.width / (playerPlatforms.inventoryButtonAmmount + 1);
            int buttonHeight = Screen.height / 8;

            for (int i = 0; i < playerPlatforms.inventoryButtonAmmount; i++)
            {
                instantiatedInventoryButtons[i] = Instantiate(inventoryButton);
                instantiatedInventoryButtons[i].transform.SetParent(uiCanvas.transform);

                ChangeInventoryButtonImageAndText(i, playerPlatforms);

                instantiatedInventoryButtons[i].transform.position = new Vector3(buttonDistance * (i + 1), buttonHeight, 0);
            }
        }

        else
        {
            foreach (InventoryButton buttonToActivate in instantiatedInventoryButtons)
            {
                buttonToActivate.gameObject.SetActive(true);
            }
        }
    }

    private void InstantiateInventoryButtonsCheck(int inventoryButtonAmmount)
    {
        bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons == null;
        if (instantiatedInventoryButtonsArrayNotInstantiated)
        {
            instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
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

    public void ChangeInventoryButtonImageAndText(int currentButton, PlayerPlatforms playerPlatforms)
    {
        GameObject buttonImage = instantiatedInventoryButtons[currentButton].transform.GetChild(0).gameObject;
        GameObject buttonText = instantiatedInventoryButtons[currentButton].transform.GetChild(1).gameObject;

        if (playerPlatforms.ramps > 0 && !playerPlatforms.rampButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = rampImage;
            instantiatedInventoryButtons[currentButton].name = "rampInventoryButton";
            buttonText.GetComponent<Text>().text = playerPlatforms.ramps + "/" + playerPlatforms.ramps;

            playerPlatforms.rampButtonInstantiated = true;
        }
        else if (playerPlatforms.platformSquares > 0 && !playerPlatforms.platformSquaresButtonInstantated)
        {
            buttonImage.GetComponent<Image>().sprite = platformSquareImage;
            instantiatedInventoryButtons[currentButton].name = "platformSquareButton";
            buttonText.GetComponent<Text>().text = playerPlatforms.platformSquares + "/" + playerPlatforms.platformSquares;

            playerPlatforms.platformSquaresButtonInstantated = true;
        }
        else if (playerPlatforms.trampolines > 0 && !playerPlatforms.trampolineButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = trampolineImage;
            instantiatedInventoryButtons[currentButton].name = "trampolineButton";
            buttonText.GetComponent<Text>().text = playerPlatforms.trampolines + "/" + playerPlatforms.trampolines;

            playerPlatforms.trampolineButtonInstantiated = true;
        }
        else if (playerPlatforms.boostPlatforms > 0 && !playerPlatforms.boostPlatformButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = boostPlatformImage;
            instantiatedInventoryButtons[currentButton].name = "boostPlatformButton";
            buttonText.GetComponent<Text>().text = playerPlatforms.boostPlatforms + "/" + playerPlatforms.boostPlatforms;

            playerPlatforms.trampolineButtonInstantiated = true;
        }
    }
}
