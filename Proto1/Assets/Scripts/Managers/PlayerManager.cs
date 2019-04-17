using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerBallManager player;
    public int collectedCoins;
    public bool collectedSticker;
    GameState gameState;
    bool newPlayerBallIsRequired = true;
    // Start is called before the first frame update
    private void Awake()
    {
        gameState = GameState.Instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void PlayerInit()
    {
        if (newPlayerBallIsRequired)
        {
            gameState.playerBallManager.InitTypeBall("lightball");

            gameState.playerBallManager.activePlayer.transform.position = gameState.playerBallManager.spawnpoint;
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().maxAngularVelocity = 99;
            gameState.playerCamera.Target = gameState.playerBallManager.activePlayer;
            Camera camera = gameState.playerCamera.GetComponent<Camera>();
            camera.transform.LookAt(gameState.playerCamera.Target.transform.position);
            newPlayerBallIsRequired = false;
        }
        else
        {
            gameState.playerBallManager.activePlayer.transform.position = gameState.playerBallManager.spawnpoint;
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = true;
            gameState.playerCamera.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
