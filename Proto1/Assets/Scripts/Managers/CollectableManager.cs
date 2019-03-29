using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableManager : MonoBehaviour
{
    public Coin coin;
    List<Vector3> coinPositions;

    // Start is called before the first frame update
    void Start()
    {
        InitCoins();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void InitCoins()
    {
        /*
        Vector3 platform1 = new Vector3(1, 1, 1);
        Vector3 platform2 = new Vector3(3, 5, 1);

        platformPositions.Add(platform1);
        platformPositions.Add(platform2);
        foreach (Vector3 item in platformPositions)
        {
            platform = Instantiate(platform);
            platform.transform.position = item;
            platform.transform.Rotate(new Vector3(0, 0, 2));


        }
        */

        coin = Instantiate(coin);
        coin.transform.position = new Vector3(1, 1.5f, 1);
    }
}
