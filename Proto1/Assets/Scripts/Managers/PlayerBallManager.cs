using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerBallManager : MonoBehaviour
{
    GameState gameState;
    public Vector3 spawnpoint;
    public BlackHoleBall blackHoleBall;
    public NormalBall normalBall;
    public LightBall lightBall;
    public GameObject activePlayer;

    //Multiplayervariablen
    public GameObject tempMultiBall;
    public GameObject MultiActivePlayer1;
    public GameObject MultiActivePlayer2;



    // Start is called before the first frame update

    public void SetSpawnpoint(int i)
    {
        gameState = GameState.Instance;
        Vector3 playeradjustment = new Vector3(.5f, 0, 0);
        gameState.playerBallManager.spawnpoint = gameState.gridManager.gridSquares[i] + playeradjustment;

        List<int> fillsGridSpot = new List<int>();
        fillsGridSpot.Add(i);
        GameState.Instance.gridManager.AddFilledGridSpots(fillsGridSpot, SizeType.oneByOne);
    }

    public void InitTypeBall(Bal type)
    {
        //Photon instantiate
        // instantiate de photon manier en geeft het object een andere naam(zodat deze niet fuckt)
        // alle rare assignemt zooi(tempmultiball en multiball) hoeft wss niet meer maar staat er nog 
        if (PhotonNetwork.IsConnected)
        {
            switch (type)
            {
                case Bal.BlackHole:
                    tempMultiBall = PhotonNetwork.Instantiate("Photon BlackHoleBall", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    
                    gameState.playerCamera.Target = tempMultiBall;
                    break;
                case Bal.Light:
                    tempMultiBall = PhotonNetwork.Instantiate("Photon LightBall", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    
                    gameState.playerCamera.Target = tempMultiBall;
                    break;
                case Bal.Normal:
                    tempMultiBall = PhotonNetwork.Instantiate("Photon Player Ball", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    
                    gameState.playerCamera.Target = tempMultiBall;
                    break;
                default:
                    tempMultiBall = PhotonNetwork.Instantiate("Photon Player Ball", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    
                    gameState.playerCamera.Target = tempMultiBall;
                    break;
            }
            if (PhotonNetwork.IsMasterClient)
            {
                MultiActivePlayer1 = tempMultiBall;
                MultiActivePlayer1.name = "player1ball";
                activePlayer = tempMultiBall;
                tempMultiBall = null;
            }
            else
            {
                MultiActivePlayer2 = tempMultiBall;
                MultiActivePlayer2.name = "player2ball";
                activePlayer = tempMultiBall;
                tempMultiBall = null;
            }
        }
        else
        {
            switch (type)
            {
                case Bal.BlackHole:
                    activePlayer = Instantiate(blackHoleBall, gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    gameState.playerBallManager.activePlayer = activePlayer;
                    gameState.playerCamera.Target = activePlayer;
                    break;
                case Bal.Light:
                    activePlayer = Instantiate(lightBall, gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    gameState.playerBallManager.activePlayer = activePlayer;
                    gameState.playerCamera.Target = activePlayer;
                    break;
                case Bal.Normal:
                    activePlayer = Instantiate(normalBall, gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    gameState.playerBallManager.activePlayer = activePlayer;
                    gameState.playerCamera.Target = activePlayer;
                    break;
                default:
                    activePlayer = Instantiate(normalBall, gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    gameState.playerBallManager.activePlayer = activePlayer;
                    gameState.playerCamera.Target = activePlayer;
                    break;
            }
        }
    }
    private IEnumerator respawnballinternal()
    {
        yield return new WaitForEndOfFrame();
        gameState = GameState.Instance;
        Camera actualcamera = gameState.GetComponent<Camera>();
        PlayerCamera camera = gameState.playerCamera;
        this.activePlayer.transform.position = gameState.playerBallManager.spawnpoint;
        this.activePlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        this.activePlayer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        //this.GetComponent<Rigidbody>().rotation = new Quaternion(0, 0, 0, 0);
        camera.transform.position = new Vector3(gameState.playerBallManager.spawnpoint.x + camera.TargetMovementOffset.x, gameState.playerBallManager.spawnpoint.y + camera.TargetMovementOffset.y, gameState.playerBallManager.spawnpoint.z + camera.TargetMovementOffset.z);
        camera.transform.LookAt(camera.Target.transform.position);
        camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().Sleep();
    }

    public void respawnBal()
    {
        StartCoroutine(respawnballinternal());
    }

}
