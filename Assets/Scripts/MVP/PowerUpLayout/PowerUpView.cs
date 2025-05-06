using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.PowerUpLayout
{
    public class PowerUpView:MonoBehaviour
    {
        [SerializeField] private Image powerUpImage;


        public void Show(Sprite powerUpImage) 
        {
            this.powerUpImage.sprite = powerUpImage;
            gameObject.SetActive(true);
        }
        public void Hide() => gameObject.SetActive(false);
    }
}
