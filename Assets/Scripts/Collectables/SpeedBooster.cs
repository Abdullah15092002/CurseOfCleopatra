using Assets.Scripts.Enum;
using Assets.Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Collectables
{
    public class SpeedBooster:MonoBehaviour
    {
        public float BoostDuration = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PowerUpManager.Instance.ActivatePowerUp(PowerUpType.SpeedBoost, other.transform, BoostDuration);
                Destroy(gameObject);
            }
        }
    }
}
