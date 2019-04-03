using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class Balletje : MonoBehaviour
{
    GameState gameState;
    // Start is called before the first frame update
    private void Awake()
    {
        this.GetComponent<Rigidbody>().useGravity = false;
        gameState = GameState.Instance;
    }
    void Start()
    {
      
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameState.RollingPhaseActive)
        {
            this.GetComponent<Rigidbody>().useGravity= true;
        }
            
    }
}
