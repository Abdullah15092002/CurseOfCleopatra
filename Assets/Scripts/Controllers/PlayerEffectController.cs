

using Assets.Scripts.Enum;
using UnityEngine;

namespace Assets.Scripts.Controllers
{
    public class PlayerEffectController:MonoBehaviour
    {

        [SerializeField] public GameObject shieldEffectPrefab;
        [SerializeField] public GameObject magnetEffectPrefab;

        private GameObject currentshield;
        private GameObject currentmagnet;


        public void ShowShieldEffect(Transform playerTransform)
        {
            if (currentshield == null)
            {
                currentshield = Instantiate(shieldEffectPrefab, playerTransform);
                currentshield.transform.localPosition = new Vector3(0,1,0);
            }

            currentshield.SetActive(true);
        }

        public void HideShieldEffect()
        {
            if (currentshield != null)
            {
                currentshield.SetActive(false);
            }
        }
        public void ShowMagnetEffect(Transform playerTransform)
        {
            if (currentmagnet == null)
            {
                currentmagnet = Instantiate(magnetEffectPrefab, playerTransform);
                currentmagnet.transform.localPosition = new Vector3(0, 1, 0);
            }

            currentmagnet.SetActive(true);
        }

        public void HideMagnetEffect()
        {
            if (currentmagnet != null)
            {
                currentmagnet.SetActive(false);
            }
        }
    }
}
