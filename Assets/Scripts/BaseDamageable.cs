using System.Collections;
using System.Collections.Generic;
using Helpers;
using UnityEngine;

public abstract class BaseDamageable : MonoBehaviour
{
    public float Health { get; protected set; } = Utilities.BaseHealth;
    public float Armor { get; protected set; } = Utilities.BaseArmor;

    public virtual void TakeDamage(float damage, float armorPenetration)
    {
        float damageToHealth = damage * armorPenetration;
        float damageToArmor = damage - damageToHealth;
        if (Armor > 0)
        {
            if (damageToArmor > Armor)
            {
                
                damageToHealth += (damageToArmor - Armor);
                Armor = 0;
            }
            else
            {
                Armor -= damageToArmor;
            }
        }
        Health -= damageToHealth;
        
        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    protected void Reset()
    {
        Health = Utilities.BaseHealth;
        Armor = Utilities.BaseArmor;
    }


    public abstract void Die();
    public abstract void ReSpawn();
}