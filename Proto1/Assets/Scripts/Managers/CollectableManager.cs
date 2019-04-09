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
    GameState gameaState;

    // Start is called before the first frame update
    void Start()
    {
        gameaState = GameState.Instance;
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
        this.coinPositions = coinPositions;
        this.stickerPosition = stickerPosition;
        this.finishPosition = finishPosition;

        InitCoins();
        InitSticker();
        InitFinish();
    }

    public void InitFinish()
    {
        finish = Instantiate(finish);
        Vector3 adjustment = new Vector3(0, -.5f, 0);
        finish.spawnpoint = finishPosition;
        finish.transform.position = finish.spawnpoint + adjustment;
        finish.gameObject.SetActive(true);
    }

    public void InitCoins()
    {
        foreach (Vector3 coinPosition in coinPositions)
        {
            coin = Instantiate(coin);
            coin.spawnpoint = coinPosition;
            coin.transform.position = coin.spawnpoint;
            coin.gameObject.SetActive(true);
        }
    }

    public void InitSticker()
    {
        sticker = Instantiate(sticker);
        sticker.spawnpoint = stickerPosition;
        sticker.transform.position = sticker.spawnpoint;
        sticker.gameObject.SetActive(true);
        // gameaState.levelManager.stickerObject = sticker2;
    }
}
