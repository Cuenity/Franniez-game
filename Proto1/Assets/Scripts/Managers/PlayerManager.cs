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
        player = Instantiate(player);
        player.transform.position = new Vector3(1, 3, 0);
        gameState.playerCamera.Target = player;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
