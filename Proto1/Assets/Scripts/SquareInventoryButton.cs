using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SquareInventoryButton : InventoryButton
{
    [SerializeField] private GameObject platformSquare;

    public override void SetCorrectTextAndImageForInventoryButton(string platformAmmount)
    {
        WorldManager worldManager = new WorldManager();
        gameObject.transform.Find("PlatformImage").GetComponent<Image>().sprite = worldManager.GetRechthoekIcon();
        gameObject.name = InventoryButtonName.platformSquareButton.ToString();
        gameObject.GetComponentInChildren<Text>().text = platformAmmount;
    }

    public override GameObject SpawnPhotonPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = PhotonNetwork.Instantiate("Photon PlatformSquare", new Vector3(0, 0, 0), new Quaternion(0, 0, 0, 0));

        draggedPlatform.transform.Rotate(new Vector3(-90, 0, 0));
        GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace--;
        if (GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }
        GameState.Instance.levelManager.playerPlatforms.UpdatePlatformSquaresLeft(gameObject.GetComponent<InventoryButton>());

        return draggedPlatform;
    }

    public override GameObject SpawnPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = Instantiate(platformSquare);

        GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace--;
        if (GameState.Instance.levelManager.playerPlatforms.platformSquaresLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }
        GameState.Instance.levelManager.playerPlatforms.UpdatePlatformSquaresLeft(gameObject.GetComponent<InventoryButton>());

        return draggedPlatform;
    }
}
