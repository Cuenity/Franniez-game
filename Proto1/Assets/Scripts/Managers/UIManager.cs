using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Canvas canvas;
    public Canvas pauseMenuCanvas;

    [SerializeField]
    private InventoryButton inventoryButton;

    public Sprite rampImage;
    public Sprite platformSquareImage;
    public Sprite trampolineImage;
    public Sprite boostPlatformImage;
    public Sprite cannonPlatformImage;
    public Sprite normalBallImage;

    public InventoryButton[] instantiatedInventoryButtons;

    public bool newLevelInventoryisRequired = false;
    public Canvas pauseMenu;

    public void InventoryButtons(PlayerPlatforms playerPlatforms)
    {

        // Dit moet nog veranderd worden naar niet een Tag. Maar dat deed het niet. Dus nu maar 
        // op deze manier gedaan.
        GameObject canvasTest = GameObject.FindGameObjectWithTag("PauseMenu");
        pauseMenu = canvasTest.GetComponent<Canvas>();
        pauseMenu.enabled = false;

        InstantiateInventoryButtonsCheck(playerPlatforms.InventoryButtonAmmount);

        //bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons[0] == null;
        if (instantiatedInventoryButtons[0] == null)
        {
            //GameObject uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");
            // Canvas uiCanvas = canvas;

            int buttonDistance = Screen.width / (playerPlatforms.InventoryButtonAmmount + 1);
            int buttonHeight = Screen.height / 8;

            for (int i = 0; i < playerPlatforms.InventoryButtonAmmount; i++)
            {
                InventoryButton buttonForWidth = Instantiate(inventoryButton);
                //buttonForWidth.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 8, Screen.width / 8);
                instantiatedInventoryButtons[i] = buttonForWidth;
                instantiatedInventoryButtons[i].transform.SetParent(canvas.transform);

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
        //bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons == null;
        bool check = instantiatedInventoryButtons.Length > 0;
        if (instantiatedInventoryButtons == null || !check)
        {
            instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
        }
        if (newLevelInventoryisRequired == true)
        {
            instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
            newLevelInventoryisRequired = false;
        }
        //if (instantiatedInventoryButtons.Length != inventoryButtonAmmount)
        //{
        //    instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
        //}
    }

    public void DeactivateInventoryButtons()
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

    public void ActivateGarbageBinButton()
    {
        canvas.gameObject.transform.GetChild(6).gameObject.SetActive(true);
    }

    public void DeactivateGarbageBinButton()
    {
        canvas.gameObject.transform.GetChild(6).gameObject.SetActive(false);
    }

    public void ChangeInventoryButtonImageAndText(int currentButton, PlayerPlatforms playerPlatforms)
    {
        GameObject buttonImage = instantiatedInventoryButtons[currentButton].transform.GetChild(0).gameObject;
        GameObject buttonText = instantiatedInventoryButtons[currentButton].transform.GetChild(3).gameObject;

        if (playerPlatforms.ramps > 0 && !playerPlatforms.rampButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = rampImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.rampInventoryButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.ramps.ToString();

            playerPlatforms.rampButtonInstantiated = true;
        }
        else if (playerPlatforms.platformSquares > 0 && !playerPlatforms.platformSquaresButtonInstantated)
        {
            buttonImage.GetComponent<Image>().sprite = platformSquareImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.platformSquareButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.platformSquares.ToString();

            playerPlatforms.platformSquaresButtonInstantated = true;
        }
        else if (playerPlatforms.trampolines > 0 && !playerPlatforms.trampolineButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = trampolineImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.trampolineButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.trampolines.ToString();

            playerPlatforms.trampolineButtonInstantiated = true;
        }
        else if (playerPlatforms.boostPlatforms > 0 && !playerPlatforms.boostPlatformButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = boostPlatformImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.boostPlatformButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.boostPlatforms.ToString();

            playerPlatforms.boostPlatformButtonInstantiated = true;
        }
        else if (playerPlatforms.cannonPlatforms > 0 && !playerPlatforms.cannonPlatformButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = cannonPlatformImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.cannonPlatformButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.cannonPlatforms.ToString();

            playerPlatforms.cannonPlatformButtonInstantiated = true;
        }
    }
}
