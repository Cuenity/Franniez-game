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
        [SerializeField]
        private Text EnglishText, DutchText, SpanishText;

        private Color32 selectedColor = new Color32(47, 140, 242, 255);
        private Color32 whiteColor = new Color32(255, 255, 255, 255);

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
            
            switch (PlayerDataController.instance.Player.language)
            {
                case 0:
                    DutchText.color = selectedColor;
                    break;
                case 1:
                    EnglishText.color = selectedColor;
                    break;
                case 2:
                    SpanishText.color = selectedColor;
                    break;
                default:
                    break;
            }
        }

        public void ChangeTextColor()
        {
            DutchText.color = whiteColor;
            EnglishText.color = whiteColor;
            SpanishText.color = whiteColor;
            SetColor();
        }

    }
}
