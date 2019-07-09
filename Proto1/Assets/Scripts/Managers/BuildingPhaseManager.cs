using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingPhaseManager : MonoBehaviour
{
    GameState gameState;

    private void Awake()
    {
        gameState = GameState.Instance;
    }

    public void Init()
    {
        if (SceneManager.GetActiveScene().name != "VictoryScreen" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            PlayerPlatforms platforms = gameState.levelManager.playerPlatforms;
            gameState.UIManager.InitInventoryButtons(platforms);
            foreach (GameObject placedPlatform in platforms.placedPlatforms)
            {
                placedPlatform.AddComponent<PlatformDragHandler>();
                if (!placedPlatform.GetComponent<Cannon>())
                {
                    placedPlatform.GetComponent<Outline>().enabled = true;
                }
                if (placedPlatform.gameObject.transform.childCount > 0)
                {
                    placedPlatform.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
        }
        else
        {
            gameState.playerCamera.GetComponent<Rigidbody>().isKinematic = false;
            gameState.levelManager.coinList.Clear();
        }
    }
}
