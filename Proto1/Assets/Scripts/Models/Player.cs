using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static Player instance;

    public string Name { get; set; }
    public Level[] Levels { get; set; }
    public int Coins { get; set; }
    public StickerObject[] Stickers { get; set; }
    public LanguageEnum Language { get; set; }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (this != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void LoadPlayer()
    {

    }

    public void SavePlayer()
    {

    }
}

