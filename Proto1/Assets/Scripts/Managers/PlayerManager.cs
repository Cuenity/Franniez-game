using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerBal player;
    public int collectedCoins;
    public bool collectedSticker;
    GameState gameState;
    // Start is called before the first frame update
    private void Awake()
    {
        gameState = GameState.Instance;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    internal void PlayerInit()
    {
        player = Instantiate(player);
        player.transform.position = player.spawnpoint;
        Debug.Log(player.spawnpoint);
        gameState.playerCamera.Target = player;
        Camera camera=gameState.playerCamera.GetComponent<Camera>();
        camera.transform.LookAt(gameState.playerCamera.Target.transform.position);
    }
}
