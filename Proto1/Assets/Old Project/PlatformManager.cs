﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    Platform platform;
    // Start is called before the first frame update
    void Start()
    {
        platform = new Platform();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal void spawnLevel1()
    {
        Instantiate(platform);
    }
}
