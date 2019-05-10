using UnityEngine;
using UnityEngine.UI;

public class SquareInventoryButton : InventoryButton
{
    [SerializeField]
    private GameObject platformSquare;
    [SerializeField]
    private Sprite squareImage;

    public override void SetCorrectTextAndImageForInventoryButton(string platformAmmount)
    {
        gameObject.transform.Find("PlatformImage").GetComponent<Image>().sprite = squareImage;
        gameObject.name = InventoryButtonName.platformSquareButton.ToString();
        gameObject.GetComponentInChildren<Text>().text = platformAmmount;
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
