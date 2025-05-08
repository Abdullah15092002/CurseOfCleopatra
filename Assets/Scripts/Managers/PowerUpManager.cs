using Assets.Scripts.Enum;
using Assets.Scripts.SCObjects;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class PowerUpManager:MonoBehaviour
    {
        [SerializeField] PowerUpImageSO powerUpImageSO;
        public static PowerUpManager Instance;

        private bool _isMagnetActive = false;
        private Transform _playerTransform;
        private float _radius = 10f;

        public bool isShieldActive = false;
        public bool isShieldHitCooldown = false;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        public void ActivatePowerUp(PowerUpType type,Transform player, float duration)
        {
            switch (type)
            {
                case PowerUpType.Magnet:
                    _playerTransform = player;
                    _isMagnetActive = true;
                    StartCoroutine(MagnetRoutine(duration));
                    break;
                case PowerUpType.Shield:
                    isShieldActive = true;
                    StartCoroutine(ShieldRoutine(duration));
                    break;
            }
        }
        private IEnumerator ShieldRoutine(float duration)
        {
            float timer = 0f;
            UIManager.Instance.OnShowPowerUpLayout(powerUpImageSO.Shield);
            while (timer<duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            UIManager.Instance.OnHidePowerUpLayout();
            isShieldActive = false;
        }    
        public IEnumerator ShieldHitCooldown()
        {
            isShieldHitCooldown = true;
            yield return new WaitForSeconds(0.5f);
            isShieldActive = false;
            isShieldHitCooldown = false;
            UIManager.Instance.OnHidePowerUpLayout();
        }
        private IEnumerator MagnetRoutine(float duration)
        {
            float timer = 0f;
            if (_playerTransform == null)
                yield break;

            UIManager.Instance.OnShowPowerUpLayout(powerUpImageSO.magnetSprite);
            while (timer < duration)
            {
                if (_playerTransform == null)
                    break;
                var coins = FindObjectsOfType<CollectCoin>();
                foreach (var coin in coins)
                {
                    float distance = Vector3.Distance(coin.transform.position, _playerTransform.position);
                    if (distance <= _radius)
                    {
                        coin.AttractTo(_playerTransform);
                    }
                }

                timer += Time.deltaTime;
                yield return null;
            }
            UIManager.Instance.OnHidePowerUpLayout();
            _isMagnetActive = false;

            foreach (var coin in FindObjectsOfType<CollectCoin>())
            {
                coin.StopAttracting();
            }
        }
        public void GameOverRegulations()
        {
            _isMagnetActive = false;
            isShieldActive = false;
            UIManager.Instance.OnHidePowerUpLayout();
        }
    }
}
