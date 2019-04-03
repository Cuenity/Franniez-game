using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    GameState gameState;
    Scene currentScene;
   
    private void Awake()
    {
        gameState = GameState.Instance;
        //gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }

    public void InitScene()
    {
        currentScene = SceneManager.GetActiveScene();

        //gameState.platformManager.spawnLevel1();
        gameState.platformManager.Init_Platforms();

        if (currentScene.name == "TestLevelCoen")
        {
            List<Vector3> coinPositions = new List<Vector3>();
            coinPositions.Add(new Vector3(0, 1.5f, 0));
            coinPositions.Add(new Vector3(3, 2, 0));
            coinPositions.Add(new Vector3(6, 2, 0));

            Vector3 stickerPosition = new Vector3(0, -2, 0);

            gameState.collectableManager.InitCollectables(coinPositions, stickerPosition);
        }
        if(currentScene.name == "TestLevel1")
        {
            gameState.gridManager.width = 11;
            gameState.gridManager.heigth = 12;
            gameState.gridManager.Build_Grid1_Without_Visuals();
            gameState.platformManager.Build_Level1();
        }
    }
}
