using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameState gameState;

    void Start()
    {
        gameState = GameState.Instance;
    }

    void Update()
    {

    }

    public void StartButton()
    {
        if (gameState.BuildingPhaseActive == true)
        {
            gameState.BuildingPhaseActive = false;
            gameState.RollingPhaseActive = true;
        }
        else if (gameState.RollingPhaseActive == true)
        {
            Vector3 spawnpunt = gameState.playerManager.player.spawnpoint;
            PlayerCamera camera = gameState.playerCamera;
            PlayerBal player = gameState.playerManager.player;
            Camera actualcamera = gameState.GetComponent<Camera>();
            gameState.RollingPhaseActive = false;
            player.transform.position = spawnpunt;
            player.GetComponent<Rigidbody>().angularVelocity = new Vector3(0,0,0);
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); ;
            camera.transform.position = new Vector3(spawnpunt.x + camera.TargetMovementOffset.x, spawnpunt.y + camera.TargetMovementOffset.y, spawnpunt.z + camera.TargetMovementOffset.z);
            camera.transform.LookAt(camera.Target.transform.position);
            camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); ;
            gameState.BuildingPhaseActive = true;
        }
    }

    public void InventoryButton(GameObject platformSquare)
    {
        //Instantiate(platformSquare);
        //var pos = Input.mousePosition;

    }

    public void TestLang(string pathFile)
    {
        LocalizationManager.instance.LoadLocalizedText(pathFile);

        Player player = new Player();
        player.name = "";
        player.language = (int)LocalizationManager.instance.LanguageChoice;
        PlayerDataController.instance.player = player;
        PlayerDataController.instance.Save();
        SceneManager.LoadScene("StartMenu");
    }
}
