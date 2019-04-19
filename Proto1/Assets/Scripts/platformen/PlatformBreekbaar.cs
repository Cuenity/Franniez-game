using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreekbaar : Platform
{
    Vector3 spawnPosition;
    bool hasCollided;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.transform.position!=spawnPosition && GameState.Instance.BuildingPhaseActive&&hasCollided)
        {
            gameObject.transform.position = spawnPosition;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {

    }
    
    private void OnCollisionExit(Collision collision)
    {
        gameObject.SetActive(false);
        spawnPosition = gameObject.transform.position;
        gameObject.transform.position = new Vector3(-100, -100, -100);
    }
}
