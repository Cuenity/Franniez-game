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
        Debug.Log("f");

        gameState.RollingPhaseActive = true;
        gameState.BuildingPhaseActive = false;
    }
}
