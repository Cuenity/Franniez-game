using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SkinObject : ScriptableObject
{
    // Voor UI, dit ziet de speler
    public string skinName = "Skin Name";
    public int cost = 50;
    public string description;

    // Voor het balletje zelf
    public Sprite texture;
    public Material material;

}
