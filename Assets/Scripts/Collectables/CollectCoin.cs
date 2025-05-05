using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private int CoinPointValue = 1;
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.SetActive(false);
            AudioManager.Instance.PlaySFX(AudioManager.Instance.CoinCollectMusic);
            EventManager.OnCoinCollected?.Invoke(CoinPointValue);
        }
        
    }
}
