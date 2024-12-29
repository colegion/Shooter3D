using System.Collections;
using System.Collections.Generic;
using Interfaces.WeaponStrategy;
using Scriptables.Weapons;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private MeshFilter weaponMesh;
    [SerializeField] private Material weaponMaterial;

    private WeaponConfig _config;
    private IShootingStrategy _shootingStrategy;

    public void Initialize(WeaponConfig config)
    {
        _config = config;
        _shootingStrategy = GameController.Instance.GetStrategyByType(_config.weaponType);
    }
}
