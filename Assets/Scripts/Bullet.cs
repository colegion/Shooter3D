using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using DG.Tweening;
using EnemySystem;
using Helpers;
using Interfaces;
using Scriptables.Bullets;
using Scriptables.Weapons;
using UnityEngine;
using UnityEngine.Serialization;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private Collider bulletCollider;
    [SerializeField] protected GameObject visuals;
    [SerializeField] private MeshFilter bulletMesh;
    [SerializeField] private MeshRenderer bulletRenderer;
    protected WeaponConfig WeaponConfig;

    public void Initialize(WeaponConfig parentWeaponConfig)
    {
        WeaponConfig = parentWeaponConfig;
    }

    public void MoveTowardsTarget(Vector3 target)
    {
        visuals.gameObject.SetActive(true);
        transform.DOMove(target, 2f).OnComplete(ResetSelf);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out BaseDamageable damageable))
        {
            Debug.Log($"Hit: {other.gameObject.name}" , other.gameObject);
            damageable.TakeDamage(WeaponConfig.Damage, WeaponConfig.ArmorPenetration);
            Explode();
        }
    }

    protected virtual void Explode()
    {
        transform.DOKill(); 
        ResetSelf();
    }

    protected void ResetSelf()
    {
        bulletCollider.enabled = false;
        bulletRenderer.material = null;
        bulletMesh.mesh = null;
        visuals.gameObject.SetActive(false);
        PoolController.Instance.EnqueueItemToPool(Utilities.BulletTypesByWeapons[WeaponConfig.weaponType], this);
    }

    public void OnCreatedForPool()
    {
        visuals.gameObject.SetActive(false);
        bulletCollider.enabled = false;
    }

    public void OnReleaseFromPool()
    {
        visuals.gameObject.SetActive(true);
        bulletCollider.enabled = true;
    }
    public GameObject GameObject() => gameObject;
}
