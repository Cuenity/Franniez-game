using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorUIManager : MonoBehaviour
{

    private Canvas canvas;
    [SerializeField]
    private InventoryButton inventoryButton;
    [SerializeField]
    public InventoryButton[] instantiatedInventoryButtons;

    //plaatjes voor buttons
    [SerializeField]
    Sprite rampImage;
    [SerializeField]
    Sprite platformSquareImage;
    [SerializeField]
    Sprite trampolineImage;
    [SerializeField]
    Sprite boostPlatformImage;
    [SerializeField]
    Sprite cannonPlatformImage;

    

    public void InventoryButtons(LevelEditorPlatforms playerPlatforms)
    {
        canvas = GameObject.Find("InventoryButtonCanvas").GetComponent<Canvas>();
        InstantiateInventoryButtonsCheck(playerPlatforms.inventoryButtonAmmount);

        //bool instantiatedInventoryButtonsArrayNotInstantiated = instantiatedInventoryButtons[0] == null;
        if (instantiatedInventoryButtons[0] == null)
        {
            //GameObject uiCanvas = GameObject.FindGameObjectWithTag("UICanvas");
            // Canvas uiCanvas = canvas;

            int buttonDistance = Screen.width / (playerPlatforms.inventoryButtonAmmount + 1);
            int buttonHeight = Screen.height / 8;

            for (int i = 0; i < playerPlatforms.inventoryButtonAmmount; i++)
            {
                InventoryButton buttonForWidth = Instantiate(inventoryButton,canvas.transform);
                //buttonForWidth.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width / 8, Screen.width / 8);
                instantiatedInventoryButtons[i] = buttonForWidth;

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

    private void ChangeInventoryButtonImageAndText(int currentButton, LevelEditorPlatforms playerPlatforms)
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

        else if (playerPlatforms.redZones > 0 && !playerPlatforms.redZoneButtonInstantiated)
        {
            buttonImage.GetComponent<Image>().sprite = cannonPlatformImage;
            instantiatedInventoryButtons[currentButton].name = InventoryButtonName.redZoneButton.ToString();
            buttonText.GetComponent<Text>().text = playerPlatforms.redZones.ToString();

            playerPlatforms.redZoneButtonInstantiated = true;
        }
    }

    private void InstantiateInventoryButtonsCheck(int inventoryButtonAmmount)
    {
        bool check = instantiatedInventoryButtons.Length > 0;
        
        instantiatedInventoryButtons = new InventoryButton[inventoryButtonAmmount];
    }
    
}
