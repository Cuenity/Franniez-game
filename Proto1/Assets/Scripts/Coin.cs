using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
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

    private void OnCollisionEnter(Collision collision)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.collectedCoins++;
        Destroy(gameObject);
    }
}
