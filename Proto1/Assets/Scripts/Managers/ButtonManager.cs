using UnityEngine;

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
            Vector3 spawnpunt = new Vector3(1, 3, 0);
            PlayerCamera camera = gameState.playerCamera;
            PlayerBal player = gameState.playerManager.player;
            gameState.RollingPhaseActive = false;
            player.transform.position = spawnpunt;
            //gameState.playerCamera.Target = player;
            player.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); ;
            camera.transform.position = new Vector3(spawnpunt.x + camera.TargetMovementOffset.x, spawnpunt.y + camera.TargetMovementOffset.y, spawnpunt.z + camera.TargetMovementOffset.z);
            camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0); ;
            gameState.BuildingPhaseActive = true;
        }
    }

    public void InventoryButton(Platform platform)
    {
        Instantiate(platform);
    }
}
