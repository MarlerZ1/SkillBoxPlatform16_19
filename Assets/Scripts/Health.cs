using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;


[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
    public event Action<float, float, float> OnHealthChanged;

    [SerializeField] private float MaxHealth;
    private float _curHealth;
    private bool _isAlive = true;

    private void Awake()
    {
        _curHealth = MaxHealth;
    }

    public void TakeDamage(float damage)
    {
        _curHealth -= damage;

        OnHealthChanged?.Invoke(damage, MaxHealth, _curHealth);

        if (_curHealth <= 0)
        {
            _curHealth = 0;
            _isAlive = false;
            Destroy(gameObject);
        }
    }

}
