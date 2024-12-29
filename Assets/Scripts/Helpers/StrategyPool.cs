using System;
using System.Collections.Generic;
using Interfaces.WeaponStrategy;
using UnityEngine;

namespace Helpers
{
    public class StrategyPool
    {
        private readonly Dictionary<WeaponType, IShootingStrategy> _strategyByWeaponType =
            new Dictionary<WeaponType, IShootingStrategy>();

        public void PoolStrategies()
        {
            foreach (WeaponType weaponType in Enum.GetValues(typeof(WeaponType)))
            {
                CreateStrategy(weaponType);
            }
        }
        
        public IShootingStrategy GetStrategyByType(WeaponType type)
        {
            if (!_strategyByWeaponType.TryGetValue(type, out IShootingStrategy strategy))
            {
                strategy = CreateStrategy(type);
            }

            return strategy;
        }

        private IShootingStrategy CreateStrategy(WeaponType weaponType)
        {
            string strategyClassName = "Interfaces.WeaponStrategy." + weaponType + "ShootingStrategy";
            IShootingStrategy strategyInstance = null;
            Type strategyType = Type.GetType(strategyClassName);

            if (strategyType != null && typeof(IShootingStrategy).IsAssignableFrom(strategyType))
            {
                strategyInstance = (IShootingStrategy)Activator.CreateInstance(strategyType);
                _strategyByWeaponType.Add(weaponType, strategyInstance);
            }
            else
            {
                Debug.LogError(
                    $"Strategy class {strategyClassName} not found or does not implement IShootingStrategy.");
            }

            return strategyInstance;
        }
    }
}
