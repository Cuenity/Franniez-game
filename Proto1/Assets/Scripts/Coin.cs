using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{

    public delegate void ClickAction();
    public static event ClickAction PickedCoin;

    GameState gameState;
    PlayerManager playerManager;
    
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        playerManager = gameState.GetComponent<PlayerManager>();
    }
    
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.collectedCoins++;
        Destroy(gameObject);
        PickedCoin();
    }
}
