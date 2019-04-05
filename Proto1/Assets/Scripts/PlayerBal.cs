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
        this.GetComponent<SphereCollider>().isTrigger = true; // verander later ofzo
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
            this.GetComponent<SphereCollider>().isTrigger = false;
        }
        else
        {
            this.GetComponent<Rigidbody>().useGravity = false;
            this.GetComponent<SphereCollider>().isTrigger = true;
        }

    }
}
