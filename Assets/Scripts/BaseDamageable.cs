using UnityEngine;

public abstract class BaseDamageable : MonoBehaviour
{
    // Abstract methods to access health and armor from derived classes
    protected abstract float Health { get; set; }
    protected abstract float Armor { get; set; }

    // Common damage calculation logic
    public void TakeDamage(float damage, float armorPenetration)
    {
        float damageToHealth = damage * armorPenetration;
        float damageToArmor = damage - damageToHealth;

        // Apply damage to armor first
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

        // Apply remaining damage to health
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