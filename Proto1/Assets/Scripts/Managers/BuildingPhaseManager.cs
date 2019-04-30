using UnityEngine;
using UnityEngine.SceneManagement;

public class BuildingPhaseManager : MonoBehaviour
{
    GameState gameState;

    private void Awake()
    {
        gameState = GameState.Instance;
    }

    void Start()
    {

    }

    void Update()
    {

    }

    public void Init()
    {
        //LET OP ELKE KEER ALS DIE SHIT VERANDERT GAAN WE HIERDOORHEEN IS DAT NIET CHILL 
        if (SceneManager.GetActiveScene().name != "VictoryScreen" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            PlayerPlatforms platforms = gameState.levelManager.playerPlatforms;
            gameState.UIManager.InitInventoryButtons(platforms);
            foreach (GameObject placedPlatform in platforms.placedPlatforms)
            {
                placedPlatform.AddComponent<PlatformDragManager>();
                if (!placedPlatform.GetComponent<Cannon>())
                {
                    placedPlatform.GetComponent<Outline>().enabled = true;
                }
                // voor welk platform wordt onderstaande code uitgevoerd?
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
