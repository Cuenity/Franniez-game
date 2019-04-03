using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPhaseManager : MonoBehaviour
{
    GameState gameState;

    private void Awake()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
    }

    void Start()
    {
         
    }
    
    void Update()
    {
        
    }

    public void Init() {
        Debug.Log("spawn building UI");
        gameState.UIManager.InventoryButtons(1); // InventoryButtons moet meegeven welke platformen en hoeveel van elk, elk verschillend type krijgt één knop met daarin een platform (als afbeelding of wat dan ook) met het aantal weergegeven.
    }
}
