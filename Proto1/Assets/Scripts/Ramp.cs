using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ramp : MonoBehaviour
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
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        Ramp ramp = Instantiate(this, spawnPosition + rampAdjustment, new Quaternion(0, 0, 0, 0));
        ramp.transform.localScale = new Vector3(200f, 50f, 50f);
        ramp.transform.Rotate(new Vector3(-90f, -90f, 0));

    }
    internal void SpawnRampReversed(Vector3 spawnPosition)
    {
        Vector3 rampAdjustment = new Vector3(0.5f, 0f, 0f);
        Ramp ramp = Instantiate(this, spawnPosition + rampAdjustment, new Quaternion(0, 0, 0, 0));
        ramp.transform.localScale = new Vector3(200f, 50f, 50f);
        ramp.transform.Rotate(new Vector3(-90f, -90f, -180));
    }
    

}
