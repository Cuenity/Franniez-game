using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public Coin coin;
    public Sticker sticker;
    List<Vector3> coinPositions;
    Vector3 stickerPosition;

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
