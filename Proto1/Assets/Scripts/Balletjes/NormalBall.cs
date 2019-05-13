using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : MonoBehaviourPun
{
    // Start is called before the first frame update
    GameState gameState = GameState.Instance;
    private void Awake()
    {
        gameState = GameState.Instance;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
    }
    void Start()
    {
        this.GetComponent<Rigidbody>().maxAngularVelocity = 99;

    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.RollingPhaseActive)
        {
            this.GetComponent<Rigidbody>().useGravity = true;
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<SphereCollider>().isTrigger = true;
        }
    }
    private void FixedUpdate()
    {
        if (this.transform.position.y < gameState.gridManager.height * -1 || this.transform.position.x < 0 || this.transform.position.x > gameState.gridManager.width)
        {
            Handheld.Vibrate();
            gameState.levelManager.SetBuildingPhase();
        }
    }

    [PunRPC]
    void FlagHit(string hitter)
    {
        if(hitter == "masterhit")
        {
            gameState.UIManager.ChangeFlagHitTrue(gameState.UIManager.p1FlagHit);
            gameState.levelManager.p1Finish = true;
        }
        else
        {
            gameState.UIManager.ChangeFlagHitTrue(gameState.UIManager.p2FlagHit);
            gameState.levelManager.p2Finish = true;
        }

        //checken of we gewonnen hebben 

        if(gameState.levelManager.p1Finish ==true && gameState.levelManager.p2Finish == true)
        {
            PhotonView view = this.GetComponent<PhotonView>();
            view.RPC("WinLevel", RpcTarget.All);
            Debug.Log("gewonnen in MP fucker");
        }
    }
    [PunRPC]
    void FlagUnHit(string hitter)
    {
        if (hitter == "masterhit")
        {
            gameState.UIManager.ChangeFlagHitFalse(gameState.UIManager.p1FlagHit);
            gameState.levelManager.p1Finish = false;
        }
        else
        {
            gameState.UIManager.ChangeFlagHitFalse(gameState.UIManager.p2FlagHit);
            gameState.levelManager.p2Finish = false;
        }
    }
    [PunRPC]
    void WinLevel()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel(26);
        }
    }
}
