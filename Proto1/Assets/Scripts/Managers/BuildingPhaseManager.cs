using System.Collections;
using System.Collections.Generic;
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

    public void Init() {
        //LET OP ELKE KEER ALS DIE SHIT VERANDERT GAAN WE HIERDOORHEEN IS DAT NIET CHILL
        if (SceneManager.GetActiveScene().name != "VictoryScreen")
        {
            PlayerPlatforms platforms = gameState.levelManager.playerPlatforms;
            gameState.UIManager.InventoryButtons(platforms); // InventoryButtons moet meegeven welke platformen en hoeveel van elk, elk verschillend type krijgt één knop met daarin een platform (als afbeelding of wat dan ook) met het aantal weergegeven.
            foreach (var placedPlatform in GameState.Instance.levelManager.playerPlatforms.placedPlatforms)
            {
                placedPlatform.AddComponent<PlatformDragManager>();
                placedPlatform.GetComponent<Outline>().enabled = true;
                if (placedPlatform.gameObject.transform.childCount > 0)
                {
                    placedPlatform.gameObject.transform.GetChild(0).gameObject.SetActive(true);
                }
                //placedPlatform.GetComponent<PlatformDragManager>().enabled = true;
            }
        }
        else
        {
            gameState.playerCamera.GetComponent<Rigidbody>().isKinematic = false;
            gameState.levelManager.coinList.Clear();
        }
    }
}
