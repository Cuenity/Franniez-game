﻿using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RampInventoryButton : InventoryButton
{
    [SerializeField]
    private GameObject ramp;
    [SerializeField]
    private Sprite rampImage;
    [SerializeField]
    private RotateSprite rotateSprite;

    public override void SetCorrectTextAndImageForInventoryButton(string platformAmmount)
    {
        WorldManager worldManager = new WorldManager();
        gameObject.transform.Find("PlatformImage").GetComponent<Image>().sprite = worldManager.GetRampIcon();
        gameObject.name = InventoryButtonName.rampInventoryButton.ToString();
        gameObject.GetComponentInChildren<Text>().text = platformAmmount;
    }

    public override GameObject SpawnPhotonPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = PhotonNetwork.Instantiate("Photon RampSmall2", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        draggedPlatform.transform.Rotate(new Vector3(270, 270, 0));
        GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace--;

        if (GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }

        GameState.Instance.levelManager.playerPlatforms.UpdateRampsLeft(gameObject.GetComponent<InventoryButton>());

        RotateSprite sprite = Instantiate(rotateSprite);
        sprite.type = PlatformType.ramp;

        sprite.transform.SetParent(draggedPlatform.transform);
        sprite.transform.localScale = new Vector3(0.0015f, 0.00075f, 0);
        sprite.transform.position = draggedPlatform.transform.position + new Vector3(0, -0.9f, -0.51f);

        sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

        return draggedPlatform;
    }

    public override GameObject SpawnPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = Instantiate(ramp);

        GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace--;
        if (GameState.Instance.levelManager.playerPlatforms.rampsLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }
        GameState.Instance.levelManager.playerPlatforms.UpdateRampsLeft(gameObject.GetComponent<InventoryButton>());

        RotateSprite sprite = Instantiate(rotateSprite);
        sprite.type = PlatformType.ramp;

        sprite.transform.SetParent(draggedPlatform.transform);
        sprite.transform.localScale = new Vector3(0.0015f, 0.00075f, 0);
        sprite.transform.position = draggedPlatform.transform.position + new Vector3(0, -0.9f, -0.51f);

        sprite.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));

        return draggedPlatform;
    }
}
