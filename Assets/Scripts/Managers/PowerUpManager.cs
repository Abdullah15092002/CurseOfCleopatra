using Assets.Scripts.Controllers;
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
        [SerializeField] GameObject playerEffectControllerPrefab;      
        private PlayerEffectController effectController;
        public static PowerUpManager Instance;

        private HashSet<PowerUpType> activeBoosts = new();
        private float defaultGameSpeed;
        private float boostSpeed = 18f;
        private float slowSpeed = 5f;

        private Transform _playerTransform;
        private float _radius = 10f;

        private bool _isMagnetActive = false;
        public bool isShieldActive = false;
        public bool isShieldHitCooldown = false;
        public bool isSpeedBoostActive = false;
        public bool isSlowDownActive = false;
        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
                GameObject instance = Instantiate(playerEffectControllerPrefab);
                DontDestroyOnLoad(instance);
                effectController = instance.GetComponent<PlayerEffectController>();
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
                    _playerTransform = player;
                    isShieldActive = true;
                    StartCoroutine(ShieldRoutine(duration));
                    break;
                case PowerUpType.SpeedBoost:
                    _playerTransform= player;
                    isSpeedBoostActive = true;
                    StartCoroutine(SpeedBoostRoutine(duration));
                    break;
                case PowerUpType.SlowDown:
                    _playerTransform = player;
                    isSlowDownActive = true;
                    StartCoroutine(SlowDownRoutine(duration));
                    break;

            }
        }
        private void ApplyBoostEffects()
        {
            if (activeBoosts.Contains(PowerUpType.SpeedBoost))
            {
                LevelGenerator.boostMultiplier =1.5f;
            }
            else if (activeBoosts.Contains(PowerUpType.SlowDown))
            {
                LevelGenerator.boostMultiplier = 0.5f;
            }
            else
            {
                LevelGenerator.boostMultiplier = 1f;
            }
        }
        private IEnumerator SpeedBoostRoutine(float duration)
        {
            float timer = 0f;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.BoostCollectMusic);
            activeBoosts.Add(PowerUpType.SpeedBoost);
            ApplyBoostEffects();
            UIManager.Instance.OnShowPowerUpLayout(PowerUpType.SpeedBoost,powerUpImageSO.speedBoostSprite);
            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            activeBoosts.Remove(PowerUpType.SpeedBoost);
            ApplyBoostEffects();
            UIManager.Instance.OnHidePowerUpLayout(PowerUpType.SpeedBoost);           
            isSpeedBoostActive = false;
        }
        public IEnumerator SlowDownRoutine(float duration) 
        {
            float timer = 0f;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.BoostCollectMusic);
            activeBoosts.Add(PowerUpType.SlowDown);
            ApplyBoostEffects();
            UIManager.Instance.OnShowPowerUpLayout(PowerUpType.SlowDown,powerUpImageSO.slowDownSprite);
            while (timer < duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            activeBoosts.Remove(PowerUpType.SlowDown);
            ApplyBoostEffects();
            UIManager.Instance.OnHidePowerUpLayout(PowerUpType.SlowDown);
            isSlowDownActive = false;
        }      
        private IEnumerator ShieldRoutine(float duration)
        {
            float timer = 0f;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.BoostCollectMusic);
            if (_playerTransform == null)
                yield break;
            effectController.ShowShieldEffect(_playerTransform);
            UIManager.Instance.OnShowPowerUpLayout(PowerUpType.Shield, powerUpImageSO.Shield);
            while (timer<duration)
            {
                timer += Time.deltaTime;
                yield return null;
            }
            effectController.HideShieldEffect();
            UIManager.Instance.OnHidePowerUpLayout(PowerUpType.Shield);
            isShieldActive = false;
        }    
        public IEnumerator ShieldHitCooldown()
        {
            isShieldHitCooldown = true;
            yield return new WaitForSeconds(0.5f);
            isShieldActive = false;
            isShieldHitCooldown = false;
            effectController.HideShieldEffect();
            UIManager.Instance.OnHidePowerUpLayout(PowerUpType.Shield);
        }
        private IEnumerator MagnetRoutine(float duration)
        {
            float timer = 0f;
            AudioManager.Instance.PlaySFX(AudioManager.Instance.BoostCollectMusic);
            if (_playerTransform == null)
                yield break;
            effectController.ShowMagnetEffect(_playerTransform);
            UIManager.Instance.OnShowPowerUpLayout(PowerUpType.Magnet, powerUpImageSO.magnetSprite);
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
            effectController.HideMagnetEffect();
            UIManager.Instance.OnHidePowerUpLayout(PowerUpType.Magnet);        
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
            effectController.HideShieldEffect();
            effectController.HideMagnetEffect();          
        }
     
    }
}
