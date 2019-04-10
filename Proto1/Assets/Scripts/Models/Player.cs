using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player
{
    public string name;
    public Level[] levels = new Level[50];
    public int coins;
    public Sticker[] stickers;
    public int language;
}

