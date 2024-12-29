using System;
using System.Collections;
using System.Collections.Generic;
using Helpers;
using Interfaces.WeaponStrategy;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Dictionary<WeaponType, IShootingStrategy> strategyByWeaponType =
        new Dictionary<WeaponType, IShootingStrategy>();
    private static GameController _instance;

    public static GameController Instance
    {
        get
        {
            if (_instance == null)
                _instance = new GameObject("GameController").AddComponent<GameController>();
            return _instance;
        }
    }

    private void Awake()
    {
        foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
        {
            string strategyClassName = weaponType + "ShootingStrategy";
            
            Type strategyType = Type.GetType(strategyClassName);

            if (strategyType != null && typeof(IShootingStrategy).IsAssignableFrom(strategyType))
            {
                IShootingStrategy strategyInstance = (IShootingStrategy)Activator.CreateInstance(strategyType);
                strategyByWeaponType.Add(weaponType, strategyInstance);
            }
            else
            {
                Debug.LogError(
                    $"Strategy class {strategyClassName} not found or does not implement IShootingStrategy.");
            }
        }
    }
}
