using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Shop.Items
{
    class SkinsShop : MonoBehaviour
    {
        // Materials
        readonly Material mat_World;
        readonly Sprite texture_World;
        readonly Material mat_Basketball;
        readonly Sprite texture_Basketball;

        readonly Material mat_Tennisball;
        readonly Sprite texture_Tennisball;

        readonly Material mat_Blue;
        readonly Sprite texture_Blue;

        GameObject buttons;
        Button skinButton;

        public List<SkinObject> skins;

        public void Awake()
        {
            LoadSkinObjects();
        }

        public void Start()
        {
            
        }

        private void MakeButton()
        {
            Button worldButton = skinButton;
            
        }

        private void LoadSkinObjects()
        {
            // Make the World
            SkinObject world = new SkinObject
            {
                cost = 20,
                skinName = LocalizationManager.instance.GetLocalizedValue("skin_World"),
                texture = texture_World,
                material = mat_World
            };
            skins.Add(world);

            // Make the Basketball
            SkinObject basketBall = new SkinObject
            {
                cost = 20,
                skinName = LocalizationManager.instance.GetLocalizedValue("skin_Basketball"),
                texture = texture_Basketball,
                material = mat_Basketball
            };
            skins.Add(basketBall);

            // Make the Tennisball
            SkinObject tennisball = new SkinObject
            {
                cost = 20,
                skinName = LocalizationManager.instance.GetLocalizedValue("skin_Tennisball"),
                texture = texture_Tennisball,
                material = mat_Tennisball
            };
            skins.Add(tennisball);

            // Make the Blue
            SkinObject blue = new SkinObject
            {
                cost = 20,
                skinName = "Blue",
                texture = texture_Tennisball,
                material = mat_Tennisball
            };
            skins.Add(blue);
        }

    }
}
