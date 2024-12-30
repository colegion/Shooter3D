using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces.WeaponStrategy;
using Scriptables.Weapons;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private MeshFilter weaponMesh;
    [SerializeField] private MeshRenderer weaponRenderer;

    private WeaponConfig _config;
    private IShootingStrategy _shootingStrategy;
    private float _lastFireTime;

    public void Initialize(WeaponConfig config)
    {
        _config = config;
        _shootingStrategy = GameController.Instance.GetStrategyByType(_config.weaponType);
        SetVisuals();
    }

    private void SetVisuals()
    {
        weaponMesh.mesh = _config.weaponMesh;
        weaponRenderer.material = _config.weaponMaterial;
    }

    public void FireBullet(Vector3 target)
    {
        if (CanFire())
        {
            _lastFireTime = Time.time;
            _shootingStrategy.Shoot(target, _config);
        }
    }
    
    private bool CanFire()
    {
        return Time.time >= _lastFireTime + (1f / _config.fireRate);
    }

    public float GetRange()
    {
        return _config.range;
    }
}
