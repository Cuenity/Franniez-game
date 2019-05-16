using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedZone : Platform
{
    private GameState gameState;

    private void Start()
    {
        gameState = GameState.Instance;
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.isTrigger && other.name.Contains("BlackHole"))
        //{

        //}
        //else
        //{
        //    //Handheld.Vibrate();
        //    gameState.levelManager.SetBuildingPhase();
        //}
    }

    // alleen nodig voor de playerplatforms (dat zijn de enige met knoppen die geupdate kunnen worden)
    public override void UpdatePlayerPlatforms() { }
}
