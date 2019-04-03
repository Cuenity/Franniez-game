using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    private GameState gameState;
    
    void Start()
    {
        gameState = GameState.Instance;
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

    public void InventoryButton(GameObject platformSquare)
    {
        //Instantiate(platformSquare);
        //var pos = Input.mousePosition;

    }
}
