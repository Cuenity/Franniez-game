using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public delegate void ClickAction();
    public static event ClickAction PickedCoin;
    public Vector3 spawnpoint;

    GameState gameState;
    PlayerManager playerManager;
    
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        playerManager = gameState.GetComponent<PlayerManager>();
        gameState.levelManager.coinList.Add(this);
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        GameState.Instance.playerManager.collectedCoins++;
        PickedCoin();
        this.gameObject.SetActive(false);
    }
    public void SetSpawnpoint(int i)
    {
        gameState = GameState.Instance;
        Vector3 coinadjustment = new Vector3(-.5f, 0, 0);
        this.spawnpoint = gameState.gridManager.gridSquares[i] + coinadjustment;
    }
}
