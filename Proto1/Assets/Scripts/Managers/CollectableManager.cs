﻿using System;
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

    // Start is called before the first frame update
    void Start()
    {
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
        finish.transform.position = finishPosition;
    }

    public void InitCoins()
    {
        foreach (Vector3 coinPosition in coinPositions)
        {
            coin = Instantiate(coin);
            coin.transform.position = coinPosition;
            coin.transform.Rotate(new Vector3(0, 90, 0));
        }
    }

    public void InitSticker()
    {
        sticker = Instantiate(sticker);
        sticker.transform.position = stickerPosition;
    }
}
