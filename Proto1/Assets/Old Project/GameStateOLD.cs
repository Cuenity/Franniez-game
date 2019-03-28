using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameStateOLD : MonoBehaviour
{
    PlatformManager platformManager;
    // Start is called before the first frame update
    void Start()
    {
        
        platformManager = new PlatformManager();
        Instantiate(platformManager);
        platformManager.spawnLevel1();

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
