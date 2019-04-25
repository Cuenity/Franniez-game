using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class ChangeColorTextLanguage : MonoBehaviour
    {
        public Text EnglishText;
        public Text DutchText;
        public Text SpanishText;

        private Color32 SelectedColor = new Color32(47, 140, 242, 255);

        public void Awake()
        {

            SetColor();
        }

        void OnEnable()
        {
            GetComponentInParent<MainMenu>().ChangeLanguage += ChangeTextColor;
        }

        private void SetColor()
        {
            
            switch (PlayerDataController.instance.player.language)
            {
                case 0:
                    DutchText.color = SelectedColor;
                    break;
                case 1:
                    EnglishText.color = SelectedColor;
                    break;
                case 2:
                    SpanishText.color = SelectedColor;
                    break;
                default:
                    break;
            }
        }

        public void ChangeTextColor()
        {
            DutchText.color = new Color(255, 255, 255, 255);
            EnglishText.color = new Color(255, 255, 255, 255);
            SpanishText.color = new Color(255, 255, 255, 255);
            SetColor();
        }

    }
}
