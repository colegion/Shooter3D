using System.Collections.Generic;
using Helpers;
using UnityEngine;

public abstract class BaseDamageable : MonoBehaviour
{
    [SerializeField] protected List<BarHelper> bars;
    protected abstract float Health { get; set; }
    protected abstract float Armor { get; set; }
    
    public void TakeDamage(float damage, float armorPenetration)
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
            
            bars[(int)BarType.Armor].UpdateSelf(Armor);
        }
        
        Health -= damageToHealth;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
        
        bars[(int)BarType.Health].UpdateSelf(Health);
    }

    public void ResetBars()
    {
        foreach (var bar in bars)
        {
        //    bar.ResetScale();
        }
    }

    public abstract void Die();
    public abstract void ReSpawn();
}