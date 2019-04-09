using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickerObject : MonoBehaviour
{
    public delegate void ClickAction();
    public static event ClickAction PickedSticker;

    GameState gameState;
    PlayerManager playerManager;
    public Vector3 spawnpoint;

    void Start()
    {
        gameState = GameObject.Find("GameState").GetComponent<GameState>();
        playerManager = gameState.GetComponent<PlayerManager>();
        gameState.levelManager.stickerObject = this;
    }

    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        playerManager.collectedSticker = true;
        this.gameObject.SetActive(false);
        PickedSticker();
    }
}
