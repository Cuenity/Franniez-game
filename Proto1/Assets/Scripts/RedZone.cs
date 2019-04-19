using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : Platform
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
        Handheld.Vibrate();
        if (other.isTrigger && other.name.Contains("BlackHole"))
        {

        }
        else
        {
            Handheld.Vibrate();
            gameState.levelManager.SetBuildingPhase();
        }

    }
}
