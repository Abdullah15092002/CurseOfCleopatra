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
        private void Start()
        {
            Debug.Log("SC asset: " + powerUpImageSO);
            Debug.Log("Magnet Sprite: " + (powerUpImageSO != null ? powerUpImageSO.magnetSprite?.name : "NULL"));
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
            }
        }

        private IEnumerator MagnetRoutine(float duration)
        {
            float timer = 0f;
            UIManager.Instance.OnShowPowerUpLayout(powerUpImageSO.magnetSprite);
            while (timer < duration)
            {
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
    }
}
