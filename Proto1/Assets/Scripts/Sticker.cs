using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sticker : MonoBehaviour
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

    private void OnTriggerEnter(Collider other)
    {
        playerManager.collectedSticker = true;
        Destroy(gameObject);
    }
}
