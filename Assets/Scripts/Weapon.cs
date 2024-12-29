using System.Collections;
using System.Collections.Generic;
using Interfaces.WeaponStrategy;
using Scriptables.Weapons;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private MeshFilter weaponMesh;
    [SerializeField] private MeshRenderer weaponRenderer;

    private WeaponConfig _config;
    private IShootingStrategy _shootingStrategy;

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

    private void FireBullet()
    {
        
    }
}
