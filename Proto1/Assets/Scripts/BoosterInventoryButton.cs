using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class BoosterInventoryButton : InventoryButton
{
    [SerializeField]
    private GameObject booster;
    [SerializeField]
    private Sprite boosterImage;

    public override void SetCorrectTextAndImageForInventoryButton(string platformAmmount)
    {
        gameObject.transform.Find("PlatformImage").GetComponent<Image>().sprite = boosterImage;
        gameObject.name = InventoryButtonName.boostPlatformButton.ToString();
        gameObject.GetComponentInChildren<Text>().text = platformAmmount;
    }

    public override GameObject SpawnPhotonPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = PhotonNetwork.Instantiate("Photon Booster", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        GameState.Instance.levelManager.playerPlatforms.boostPlatformsLeftToPlace--;

        if (GameState.Instance.levelManager.playerPlatforms.boostPlatformsLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }

        GameState.Instance.levelManager.playerPlatforms.UpdateBoostPlatformsLeft(gameObject.GetComponent<InventoryButton>());

        return draggedPlatform;
    }

    public override GameObject SpawnPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = Instantiate(booster);

        GameState.Instance.levelManager.playerPlatforms.boostPlatformsLeftToPlace--;
        if (GameState.Instance.levelManager.playerPlatforms.boostPlatformsLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }
        GameState.Instance.levelManager.playerPlatforms.UpdateBoostPlatformsLeft(gameObject.GetComponent<InventoryButton>());

        return draggedPlatform;
    }

}
