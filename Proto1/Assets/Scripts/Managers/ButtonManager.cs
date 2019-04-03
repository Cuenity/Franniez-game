using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private GameState gameState;
    
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        PlayerManager test= gameState.GetComponent<PlayerManager>();
    }
    
    void Update()
    {

    }

    public void StartButton()
    {
        if (gameState.BuildingPhaseActive == true)
        {
            gameState.RollingPhaseActive = true;
            gameState.BuildingPhaseActive = false;
        } 
        else if (gameState.rollingPhaseManager == true)
        {
            gameState.RollingPhaseActive = false;
            gameState.BuildingPhaseActive = true;
        } 
    }
}
