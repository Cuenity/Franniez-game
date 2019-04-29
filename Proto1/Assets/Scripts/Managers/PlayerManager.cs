using Photon.Pun;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerBallManager playerManager;
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
            gameState.playerBallManager.InitTypeBall(Bal.Normal);

            gameState.playerBallManager.activePlayer.transform.position = gameState.playerBallManager.spawnpoint;

            if (gameState.levelManager.bigLevel)
            {
                gameState.playerCamera.Target = gameState.playerBallManager.activePlayer;
            }
            else
            {
                GameObject targetObject = new GameObject("CenterCameraTarget");
                targetObject.transform.position = new Vector3(gameState.gridManager.width / 2, -gameState.gridManager.height / 2);
                gameState.playerCamera.Target = targetObject;
            }

            gameState.playerCamera.transform.position = gameState.playerCamera.Target.transform.position + gameState.playerCamera.TargetMovementOffset;
            gameState.playerCamera.transform.LookAt(gameState.playerCamera.Target.transform.position);

            newPlayerBallIsRequired = false;
        }
        else
        {
            gameState.playerBallManager.activePlayer.transform.position = gameState.playerBallManager.spawnpoint;
            gameState.playerBallManager.activePlayer.GetComponent<Rigidbody>().isKinematic = true;
            gameState.playerCamera.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    internal void MultiPlayerBallInit(int spawn1, int spawn2)
    {
        //geeft de mogelijkheid 2 spawnpoints mee te geven 
        ///eeeh dis ook wel beetje vies als het werkt
        if (PhotonNetwork.IsMasterClient)
        {
            gameState.playerBallManager.SetSpawnpoint(spawn1);
            gameState.playerBallManager.InitTypeBall(Bal.Normal);
        }
        else
        {
            gameState.playerBallManager.SetSpawnpoint(spawn2);
            gameState.playerBallManager.InitTypeBall(Bal.Normal);
        }
        gameState.playerBallManager.activePlayer.transform.position = gameState.playerBallManager.spawnpoint;
        gameState.playerCamera.Target = gameState.playerBallManager.activePlayer;
        gameState.playerCamera.transform.position = gameState.playerCamera.Target.transform.position + gameState.playerCamera.TargetMovementOffset;
        gameState.playerCamera.transform.LookAt(gameState.playerCamera.Target.transform.position);

        newPlayerBallIsRequired = false;
    }
}
