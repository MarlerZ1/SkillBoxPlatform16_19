using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[DisallowMultipleComponent]
public class Health : MonoBehaviour
{
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
        if (_curHealth <= 0)
        {
            _curHealth = 0;
            _isAlive = false;
            Destroy(gameObject);
        }
    }

}
