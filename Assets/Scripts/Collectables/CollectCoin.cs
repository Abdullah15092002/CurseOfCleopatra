using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    private int CoinPointValue = 1;

    private Transform _magnetTarget;
    private bool _isBeingAttracted = false;
    private float _moveSpeed = 10f;

    void Update()
    {
        if (_isBeingAttracted && _magnetTarget != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, _magnetTarget.position, _moveSpeed * Time.deltaTime);
        }
    }

    public void AttractTo(Transform target)
    {
        _magnetTarget = target;
        _isBeingAttracted = true;
    }

    public void StopAttracting()
    {
        _isBeingAttracted = false;
    }
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
