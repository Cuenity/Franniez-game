using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerBal : MonoBehaviour
{
    GameState gameState;
    public Vector3 spawnpoint;
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
            this.GetComponent<Rigidbody>().useGravity = true;
        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = false;
        }

    }
}
