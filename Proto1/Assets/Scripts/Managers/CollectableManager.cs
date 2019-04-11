using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public Coin coin;
    public StickerObject sticker;
    public Finish finish;
    List<Vector3> coinPositions;
    Vector3 stickerPosition;
    Vector3 finishPosition;
    GameState gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitCollectables(List<Vector3> coinPositions, Vector3 stickerPosition)
    {
        this.coinPositions = coinPositions;
        this.stickerPosition = stickerPosition;

        InitCoins();
        InitSticker();
    }

    public void InitCollectables(List<Vector3> coinPositions, Vector3 stickerPosition, Vector3 finishPosition)
    {
        gameState = GameState.Instance;
        this.coinPositions = coinPositions;
        this.stickerPosition = stickerPosition;
        this.finishPosition = finishPosition;

        InitCoins();
        InitSticker();
        InitFinish();
    }

    public void InitFinish()
    {
        Finish finish2 = Instantiate(finish);
        Vector3 adjustment = new Vector3(0, -.5f, 0);
        finish2.spawnpoint = finishPosition;
        finish2.transform.position = finish2.spawnpoint + adjustment;
        finish2.gameObject.SetActive(true);
        gameState.levelManager.finish = finish2;
    }

    public void InitCoins()
    {
        foreach (Vector3 coinPosition in coinPositions)
        {
            Coin coin2 = Instantiate(coin);
            coin2.spawnpoint = coinPosition;
            coin2.transform.position = coin2.spawnpoint;
            coin2.gameObject.SetActive(true);
            gameState.levelManager.coinList.Add(coin2);
        }
    }

    public void InitSticker()
    {
        StickerObject sticker2 = Instantiate(sticker);
        sticker2.spawnpoint = stickerPosition;
        sticker2.transform.position = sticker2.spawnpoint;
        sticker2.gameObject.SetActive(true);
        gameState.levelManager.stickerObject = sticker2;
    }
}