using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Scriptables.Bullets;
using Scriptables.Weapons;
using UnityEngine;
using UnityEngine.Serialization;

public class Bullet : MonoBehaviour
{
    [SerializeField] private MeshFilter bulletMesh;
    [SerializeField] private MeshRenderer bulletRenderer;
    private WeaponConfig _weaponConfig;

    public void Initialize(WeaponConfig parentWeaponConfig)
    {
        _weaponConfig = parentWeaponConfig;
    }

    public void MoveTowardsTarget(Vector3 target)
    {
        transform.DOMove(target, 2f).OnComplete(ResetSelf);
    }

    private void OnCollisionEnter(Collision other)
    {
        Explode();
    }

    protected virtual void Explode()
    {
        ResetSelf();
    }

    private void ResetSelf()
    {
        bulletRenderer.material = null;
        bulletMesh.mesh = null;
        gameObject.SetActive(false);
    }
}
