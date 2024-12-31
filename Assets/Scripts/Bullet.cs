using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using EnemySystem;
using Interfaces;
using Scriptables.Bullets;
using Scriptables.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour, IPoolable
{
    [SerializeField] private GameObject visuals;
    [SerializeField] private MeshFilter bulletMesh;
    [SerializeField] private MeshRenderer bulletRenderer;
    private WeaponConfig _weaponConfig;

    public void Initialize(WeaponConfig parentWeaponConfig)
    {
        _weaponConfig = parentWeaponConfig;
    }

    public void MoveTowardsTarget(Vector3 target)
    {
        visuals.gameObject.SetActive(true);
        Vector3 directionToTarget = (target - transform.position).normalized;
        if (directionToTarget != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(directionToTarget);
        }
        transform.DOMove(target, 2f).OnComplete(ResetSelf);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            Explode();
        }
    }

    protected virtual void Explode()
    {
        ResetSelf();
    }

    private void ResetSelf()
    {
        bulletRenderer.material = null;
        bulletMesh.mesh = null;
        visuals.gameObject.SetActive(false);
    }

    public void OnCreatedForPool()
    {
    }

    public void OnAssignPool()
    {
    }

    public void OnReleasePool()
    {
    }

    public void OnDeletePool()
    {
    }

    public GameObject GameObject() => gameObject;
}
