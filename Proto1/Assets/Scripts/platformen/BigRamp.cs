using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigRamp : Platform
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void SpawnRamp(Vector3 spawnPosition)
    {
        BigRamp ramp = Instantiate(this, spawnPosition, new Quaternion(0, 0, 0, 0));
        ramp.transform.Rotate(new Vector3(-90f, -90f, 0));

    }
    internal void SpawnRampReversed(Vector3 spawnPosition)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        BigRamp ramp = Instantiate(this, spawnPosition + rampAdjustment, new Quaternion(0, 0, 0, 0));
        ramp.transform.Rotate(new Vector3(-90f, -90f, -180));
    }
    

}
