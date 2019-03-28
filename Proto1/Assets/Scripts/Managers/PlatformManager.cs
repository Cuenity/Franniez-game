using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    public GameObject platform;
    
    List<Vector3> platformPositions;

    // Start is called before the first frame update
    private void Awake()
    {
        platformPositions = new List<Vector3>();
    }
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void spawnLevel1()
    {
       // platform = Instantiate(platform);

    }
    internal void Init_Platforms()
    {
        
        Vector3 platform1 = new Vector3(1, 1, 1);
        Vector3 platform2 = new Vector3(5, 5, 5);
        platformPositions.Add(platform1);
        platformPositions.Add(platform2);
        foreach (Vector3 item in platformPositions)
        {
            platform= Instantiate(platform, item, Quaternion.identity);
           
        }
    }
}
