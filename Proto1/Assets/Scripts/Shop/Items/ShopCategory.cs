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
        [SerializeField]
        public string Name;
        [SerializeField]
        public int cost;
        [SerializeField]
        public ShopCategoryEnum categoryEnum;
        [SerializeField]
        public SkinObject[] skins;


        public void Start()
        {
            // Voor localication zet alle teksten goed, key komt vanuit de inspector

            //foreach(SkinObject skin in skins)
            //{
            //    skin.skinName = LocalizationManager.instance.GetLocalizedValue(skin.skinName);
            //}
        }
    }
}
