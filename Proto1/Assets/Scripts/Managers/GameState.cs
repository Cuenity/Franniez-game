using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
   
    IOManager IOManager;
    InputManager InputManager;
    UIManager UIManager;
    LevelManager levelManager;
    
    

    void Awake()
    {
        DontDestroyOnLoad(this);
       
        IOManager = gameObject.AddComponent<IOManager>();
        InputManager = gameObject.AddComponent<InputManager>();
        UIManager = gameObject.AddComponent<UIManager>();
        levelManager = gameObject.AddComponent<LevelManager>();
    }



    // Start is called before the first frame update
    void Start()
    {
        
        //PlayerManager.spawnPlayer();
    }

    // Update is called once per frame
    void Update()
    {

    }
   
}
