using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


public class ShopCategory : MonoBehaviour
{
    // Voor UI, dit ziet de speler
    [SerializeField] public string Name;
    [SerializeField] public int cost;
    [SerializeField] public ShopCategoryEnum categoryEnum;
    [SerializeField] public SkinObject[] skins;
    [SerializeField] public Sprite Image;
    [SerializeField] public bool Owned;


    public void Start()
    {
        // Pakket, moet nog implementeren
    }
}
