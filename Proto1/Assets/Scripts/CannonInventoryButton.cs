using UnityEngine;
using UnityEngine.UI;

public class CannonInventoryButton : InventoryButton
{
    [SerializeField]
    private GameObject cannon;
    [SerializeField]
    private Sprite cannonImage;

    public override void SetCorrectTextAndImageForInventoryButton(string platformAmmount)
    {
        gameObject.transform.Find("PlatformImage").GetComponent<Image>().sprite = cannonImage;
        gameObject.name = InventoryButtonName.cannonPlatformButton.ToString();
        gameObject.GetComponentInChildren<Text>().text = platformAmmount;
    }

    public override GameObject SpawnPhotonPlatformFromInventoryButton()
    {
        throw new System.NotImplementedException();
    }

    public override GameObject SpawnPlatformFromInventoryButton()
    {
        GameObject draggedPlatform = Instantiate(cannon);

        GameState.Instance.levelManager.playerPlatforms.cannonPlatformsLeftToPlace--;
        if (GameState.Instance.levelManager.playerPlatforms.cannonPlatformsLeftToPlace == 0)
        {
            InventoryButtonAllowed = false;
        }
        GameState.Instance.levelManager.playerPlatforms.UpdateCannonPlatformsLeft(gameObject.GetComponent<InventoryButton>());

        return draggedPlatform;
    }
}
