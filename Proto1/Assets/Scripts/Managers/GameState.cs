using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    PlatformManager PlatformManager = new PlatformManager();
    PlayerManager PlayerManager = new PlayerManager();
    UIManager UIManager = new UIManager();
    InputManager InputManager = new InputManager();
    IOManager IOManager = new IOManager();
    
    void Awake()
    {
        DontDestroyOnLoad(this);
    }



    // Start is called before the first frame update
    void Start()
    {
        PlatformManager.spawnLevel1();
        PlayerManager.spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
