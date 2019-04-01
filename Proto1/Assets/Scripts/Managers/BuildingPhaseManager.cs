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

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init() {
        Debug.Log("spawn building UI");
        gameState.UIManager.InventoryButtons(2);
    }
}
