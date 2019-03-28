using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    PlayerManager PlayerManager;
    PlatformManager platformManager;
    private void Awake()
    {
        PlayerManager = gameObject.AddComponent<PlayerManager>();
        platformManager = gameObject.AddComponent<PlatformManager>();
        platformManager.spawnLevel1();
        platformManager.Init_Platforms();
    }
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
   
}
