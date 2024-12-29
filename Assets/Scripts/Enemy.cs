using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamageable
{
    private float _health = Utilities.BaseHealth;
    
    public void TakeDamage(float amount)
    {
        _health -= amount;
        if(_health <= 0)
            Die();
    }

    public void Die()
    {
        gameObject.SetActive(false);
    }

    public void ReSpawn()
    {
        _health = Utilities.BaseHealth;
    }
}
