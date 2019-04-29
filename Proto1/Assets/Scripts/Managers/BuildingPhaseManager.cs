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
        //wat bedoel je elke keer als die shit veranderd? elke keer dat er op de start knop gedrukt wordt moet dit uitgevoerd worden om dingen wel / niet weer te geven
        if (SceneManager.GetActiveScene().name != "VictoryScreen" && SceneManager.GetActiveScene().name != "MainMenu")
        {
            PlayerPlatforms platforms = gameState.levelManager.playerPlatforms;
            gameState.UIManager.InitInventoryButtons(platforms); // InventoryButtons moet meegeven welke platformen en hoeveel van elk, elk verschillend type krijgt één knop met daarin een platform (als afbeelding of wat dan ook) met het aantal weergegeven.
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
