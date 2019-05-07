using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Shop.Items
{
    public class ShopCategory : MonoBehaviour
    {
        // Voor UI, dit ziet de speler
        [SerializeField] public string Name;
        [SerializeField] public int cost;
        [SerializeField] public ShopCategoryEnum categoryEnum;
        [SerializeField] public SkinObject[] skins;


        public void Start()
        {
            // Pakket, moet nog implementeren
        }
    }
}
