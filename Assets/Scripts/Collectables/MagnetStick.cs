using Assets.Scripts.Enum;
using Assets.Scripts.Managers;
using UnityEngine;

namespace Assets.Scripts.Collectables
{
    public class MagnetStick:MonoBehaviour
    {
        public float magnetDuration = 5f;
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                PowerUpManager.Instance.ActivatePowerUp(PowerUpType.Magnet,other.transform, magnetDuration);
                Destroy(gameObject);
            }
        }
    }
    
}
