using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces.WeaponStrategy;
using Scriptables.Weapons;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameController : MonoBehaviour
{
    [SerializeField] private Player player;
    private List<WeaponConfig> _weaponConfigs = new List<WeaponConfig>();
    private StrategyPool _strategyPool;
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = FindObjectOfType<GameController>();
            return _instance;
        }
    }

    private void Awake()
    {
        _instance = this;
        _strategyPool = new StrategyPool();
        _strategyPool.PoolStrategies();
        LoadWeaponConfigs();
    }
    
    private void LoadWeaponConfigs()
    {
        Addressables.LoadAssetsAsync<WeaponConfig>(Utilities.WeaponConfigLabel, null).Completed += OnWeaponConfigsLoaded;
    }
    
    private void OnWeaponConfigsLoaded(AsyncOperationHandle<IList<WeaponConfig>> handle)
    {
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            _weaponConfigs = new List<WeaponConfig>(handle.Result);
            
            foreach (var weaponConfig in _weaponConfigs)
            {
                Debug.Log($"Loaded Weapon Config: {weaponConfig.name}, Damage: {weaponConfig.damage}");
            }
            player.Initialize();
        }
        else
        {
            Debug.LogError("Failed to load WeaponConfigs.");
        }
    }

    public IShootingStrategy GetStrategyByType(WeaponType type)
    {
        return _strategyPool.GetStrategyByType(type);
    }

    public WeaponConfig GetWeaponConfigByType(WeaponType type)
    {
        return _weaponConfigs.Find(w => w.weaponType == type);
    }
    
    private void OnDestroy()
    {
        /*foreach (var weaponConfig in _weaponConfigs)
        {
            Addressables.Release(weaponConfig);
        }*/
    }
}
