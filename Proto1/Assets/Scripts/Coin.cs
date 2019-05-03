using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public delegate void ClickAction();
    public static event ClickAction PickedCoin;
    public Vector3 spawnpoint;
    public float SpringForce;
    public float SpringDamper;
    public GameObject firework; 

    GameState gameState;
    PlayerManager playerManager;
    
    void Start()
    {
        gameState = GameState.Instance;
        playerManager = gameState.GetComponent<PlayerManager>();
        gameState.levelManager.coinList.Add(this);
    }
    
    void Update()
    {
        
    }

    public void go_toBall(Collider other)
    {
        Rigidbody Body = this.GetComponent<Rigidbody>();

        Vector3 Diff = transform.position - other.transform.position;
        Vector3 Vel = Body.velocity;

        Vector3 force = (Diff * -SpringForce) - (Vel * SpringDamper);

        Body.AddForce(force,ForceMode.Impulse);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger && other.name.Contains("Ball"))
        {
            Debug.Log(other);
            go_toBall(other);
        }
        else if (other.name.Contains("Ball"))
        {
            gameState = GameState.Instance;
            GameState.Instance.playerManager.collectedCoins++;
            PickedCoin();
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.Destroy(this.gameObject);
            }
            this.gameObject.SetActive(false);
            firework = Instantiate(firework);
            firework.transform.position = gameState.playerBallManager.activePlayer.transform.position;
        }
    }
    public void SetSpawnpoint(int i)
    {
        gameState = GameState.Instance;
        Vector3 coinadjustment = new Vector3(2f, 0, 0);
        this.spawnpoint = gameState.gridManager.gridSquares[i] + coinadjustment;
    }
}
