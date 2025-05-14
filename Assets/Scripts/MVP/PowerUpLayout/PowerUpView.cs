using Assets.Scripts.Enum;
using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.MVP.PowerUpLayout
{
    public class PowerUpView : MonoBehaviour
    {
        [SerializeField] private Image powerUpImage1;
        [SerializeField] private Image powerUpImage2;
        [SerializeField] private Image powerUpImage3;

        private Dictionary<PowerUpType, Image> activePowerUps = new();

        private List<Image> imageSlots;

        private void Start()
        {
            imageSlots = new List<Image> { powerUpImage1, powerUpImage2, powerUpImage3 };

            foreach (var image in imageSlots)
            {
                image.enabled = false; // Başta kapalı
            }
            gameObject.SetActive(true);
        }

        public void Show(PowerUpType type, Sprite sprite)
        {
            if (activePowerUps.ContainsKey(type))
                return; // Aynı power-up zaten gösteriliyorsa tekrar gösterme

            foreach (var slot in imageSlots)
            {
                if (!slot.enabled)
                {
                    slot.sprite = sprite;
                    slot.enabled = true;
                    activePowerUps[type] = slot;
                    return;
                }
            }
        }

        public void Hide(PowerUpType type)
        {
            if (activePowerUps.TryGetValue(type, out var image))
            {
                image.enabled = false;
                activePowerUps.Remove(type);
            }
        }

        public void HideAll()
        {
            foreach (var slot in imageSlots)
            {
                slot.enabled = false;
            }
            activePowerUps.Clear();
        }
    }
}
