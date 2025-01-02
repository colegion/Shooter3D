using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemySystem;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodeableBullet : Bullet
{
    [SerializeField] private ParticleSystem explosionEffect;
    protected override void Explode()
    {
        transform.DOKill(); 
        var radius = WeaponConfig.areaOfEffect;
        int maxColliders = 10;
        Collider[] results = new Collider[maxColliders];
        
        int size = Physics.OverlapSphereNonAlloc(transform.position, radius, results);
        
        for (int i = 0; i < size; i++)
        {
            var hitCollider = results[i];
            if (hitCollider != null && hitCollider.gameObject.TryGetComponent(out Enemy enemy))
            {
                Debug.Log($"Enemy hit: {enemy.gameObject.name}");
                enemy.TakeDamage(WeaponConfig.Damage, WeaponConfig.ArmorPenetration);
            }
        }
        
        explosionEffect.gameObject.SetActive(true);
        explosionEffect.Play();
        visuals.GameObject().SetActive(false);
        DOVirtual.DelayedCall(2f, () =>
        {
            explosionEffect.gameObject.SetActive(false);
            ResetSelf();
        });
    }

    private void OnCollisionEnter(Collision other)
    {
        explosionEffect.transform.position = other.contacts[0].point;
        Explode();
    }
}