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
    public List<Bal> ballList = new List<Bal>();
    public int activeplayerIndex;



    // Start is called before the first frame update

    public void SetSpawnpoint(int i)
    {
        gameState = GameState.Instance;
        Vector3 playeradjustment = new Vector3(.5f, 0, 0);
        gameState.playerBallManager.spawnpoint = gameState.gridManager.gridSquares[i] + playeradjustment;
        GameState.Instance.gridManager.AddFilledGridSpots(new List<int> { i } , SizeType.oneByOne);
    }

    public void InitTypeBall(Bal type)
    {
        //Photon instantiate
        // instantiate de photon manier en geeft het object een andere naam
        // alle rare assignemt zooi(tempmultiball en multiball) hoeft wss niet meer maar staat er nog 
        if (PhotonNetwork.InRoom)
        {
            switch (type)
            {
                case Bal.BlackHole:
                    tempMultiBall = PhotonNetwork.Instantiate("Photon BlackHoleBall", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    tempMultiBall.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    break;
                case Bal.Light:
                    
                    tempMultiBall = PhotonNetwork.Instantiate("Photon LightBall", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    tempMultiBall.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    break;
                case Bal.Normal:
                    
                    tempMultiBall = PhotonNetwork.Instantiate("Photon Player Ball", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    tempMultiBall.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    break;
                default:
                    
                    tempMultiBall = PhotonNetwork.Instantiate("Photon Player Ball", gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    tempMultiBall.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    break;
            }
            //geeft de spelers eigen bal een unieke naam(niet met clone erachter)
            if (PhotonNetwork.IsMasterClient)
            {
                MultiActivePlayer1 = tempMultiBall;
                //MultiActivePlayer1.name = "player1Ball";
                activePlayer = tempMultiBall;
                gameState.playerCamera.Target = activePlayer;
                PhotonView view = tempMultiBall.GetComponent<PhotonView>();
                view.RPC("setActivePlayers", RpcTarget.Others, "player1");
                tempMultiBall = null;
            }
            else
            {
                MultiActivePlayer1 = tempMultiBall;
                //MultiActivePlayer2.name = "player2Ball";
                activePlayer = tempMultiBall;
                gameState.playerCamera.Target = activePlayer;
                PhotonView view = tempMultiBall.GetComponent<PhotonView>();
                view.RPC("setActivePlayers", RpcTarget.Others, "player2");
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
                    activePlayer.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    //gameState.playerCamera.Target = activePlayer;
                    break;
                case Bal.Light:
                    activePlayer = Instantiate(lightBall, gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    gameState.playerBallManager.activePlayer = activePlayer;
                    activePlayer.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    //gameState.playerCamera.Target = activePlayer;
                    break;
                case Bal.Normal:
                    activePlayer = Instantiate(normalBall, gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    gameState.playerBallManager.activePlayer = activePlayer;
                    activePlayer.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    //gameState.playerCamera.Target = activePlayer;
                    break;
                default:
                    activePlayer = Instantiate(normalBall, gameState.playerBallManager.spawnpoint, new Quaternion(0, 0, 0, 0)).gameObject;
                    gameState.playerBallManager.activePlayer = activePlayer;
                    activePlayer.GetComponent<Renderer>().material = PlayerDataController.instance.ballMaterial;
                    //gameState.playerCamera.Target = activePlayer;
                    break;
            }
        }
    }

    public void WhatBalls(bool normalball, bool blackholeball, bool lightball)
    {
        //kijken welke ballen er in het level mogen en dan de knop disablen als dit er maar 1 is
        ballList.Clear();
        if (normalball)
        {
            ballList.Add(Bal.Normal);
        }
        if (blackholeball)
        {
            ballList.Add(Bal.BlackHole);
        }
        if (lightball)
        {
            ballList.Add(Bal.Light);
        }
        BallKnop ballknop = gameState.UIManager.canvas.GetComponentInChildren<BallKnop>(true);
        if (ballList.Count > 1)
        {
            ballknop.gameObject.SetActive(true);
        }

    }
    private IEnumerator respawnballinternal()
    {
        yield return new WaitForEndOfFrame();
        gameState = GameState.Instance;
        Camera actualcamera = gameState.GetComponent<Camera>();
        PlayerCamera camera = gameState.playerCamera;
        activePlayer.transform.position = gameState.playerBallManager.spawnpoint;
        activePlayer.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
        activePlayer.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        if (gameState.levelManager.bigLevel)
        {            
            camera.transform.position = gameState.playerBallManager.spawnpoint + camera.TargetMovementOffset;
            camera.transform.LookAt(activePlayer.transform.position);
        }
        else
        {
            camera.transform.position = camera.Target.transform.position + camera.TargetMovementOffset;
            camera.transform.LookAt(camera.Target.transform.position);
        }
        
        camera.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        this.GetComponent<Rigidbody>().Sleep();
    }

    public void respawnBal()
    {
        StartCoroutine(respawnballinternal());
    }

}
