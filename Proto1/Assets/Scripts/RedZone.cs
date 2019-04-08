using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : MonoBehaviour
{
    GameState gameState;
    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        gameState.playerManager.player.respawnBal();
    }
}
