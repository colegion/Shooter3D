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
            string jsonPath = "JSON/DefaultWeaponConfigs";
            TextAsset jsonFile = Resources.Load<TextAsset>(jsonPath);

            if (jsonFile != null)
            {
                WeaponConfigsJson weaponConfigsJson = JsonUtility.FromJson<WeaponConfigsJson>(jsonFile.text);

                foreach (var weaponData in weaponConfigsJson.configs)
                {
                    weaponData.InitializeAttributes();
                    WeaponConfig weaponConfig = _weaponConfigs.Find(config => config.weaponType.ToString() == weaponData.weaponType);

                    if (weaponConfig != null)
                    {
                        weaponConfig.Initialize(weaponData);
                        Debug.Log($"Initialized WeaponConfig: {weaponConfig.name}, Damage: {weaponConfig.Damage}");
                    }
                    else
                    {
                        Debug.LogWarning($"WeaponConfig for {weaponData.weaponType} not found.");
                    }
                }
            }
            else
            {
                Debug.LogError("Failed to load WeaponConfigs JSON.");
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
        foreach (var config in _weaponConfigs)
        {
            Debug.Log("checking config type: " + config.weaponType);
            if (config.weaponType == type)
            {
                Debug.Log("found type :" + type);
                return config;
            }
        }

        return null;
    }
    
    private void OnDestroy()
    {
        /*foreach (var weaponConfig in _weaponConfigs)
        {
            Addressables.Release(weaponConfig);
        }*/
    }
}
