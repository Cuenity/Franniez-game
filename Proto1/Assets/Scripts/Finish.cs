using UnityEngine;
using System.Collections;

public class Finish : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction Finished;

    GameState gameState;
    PlayerManager playerManager;
    public Vector3 spawnpoint;

    // Use this for initialization
    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        playerManager = gameState.GetComponent<PlayerManager>();
        gameState.levelManager.finish = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.collectedCoins++;
        Finished();
        this.gameObject.SetActive(false);
    }
}
