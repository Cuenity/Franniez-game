using Photon.Pun;
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

    public bool newCollectablesAreRequired = false;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameState.Instance;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void InitCollectables(List<Vector3> coinPositions, Vector3 finishPosition)
    {
        gameState = GameState.Instance;
        this.coinPositions = coinPositions;
        this.finishPosition = finishPosition;

        InitCoins();
        InitFinish();

        newCollectablesAreRequired = false;
    }

    public void RespawnCoins(List<Vector3> coinPositions)
    {
        this.coinPositions = coinPositions;
        InitCoins();
        newCollectablesAreRequired = false;
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
        if (PhotonNetwork.InRoom)
        {
            //zorgt ervoor dat we maar 1 setje sterren spawnen in MP
            if (PhotonNetwork.IsMasterClient)
            {
                foreach (Vector3 coinPosition in coinPositions)
                {
                    Coin coin2 = PhotonNetwork.Instantiate("Photon Star", coinPosition, new Quaternion(0, 0, 0, 0)).GetComponent<Coin>();
                    coin2.gameObject.SetActive(true);
                    gameState.levelManager.coinList.Add(coin2);
                }
            }
        }
        else
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
    }

}