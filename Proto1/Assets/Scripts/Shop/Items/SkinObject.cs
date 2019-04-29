using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Skin", menuName = "Skin")]
[System.Serializable]
public class SkinObject : ScriptableObject
{
    // Voor UI, dit ziet de speler
    public string skinName = "Skin Name";
    public int cost = 50;
    public string description;
    public Sprite shopImage;
    public bool owned = false;

    // Voor het balletje zelf
    public Material material;
}
