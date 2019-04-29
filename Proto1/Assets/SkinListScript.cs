using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinListScript : MonoBehaviour
{
    [Serializable]
    public struct Skins
    {
        public string name;
        public SkinObject skinobject;
    }

    public Skins[] skins;

    public static SkinListScript instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != null && instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
    }
}
