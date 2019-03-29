using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    public GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        //gameState = GameObject.Find(GameState);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartButton()
    {
        gameState.RollingPhaseActive = true;
        gameState.BuildingPhaseActive = false;
    }
}
