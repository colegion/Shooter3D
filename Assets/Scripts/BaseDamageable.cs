using UnityEngine;

public abstract class BaseDamageable : MonoBehaviour
{
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
        }
        
        Health -= damageToHealth;

        if (Health <= 0)
        {
            Health = 0;
            Die();
        }
    }

    public abstract void Die();
    public abstract void ReSpawn();
}