using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
        public static int BaseHealth = 100;
        public static readonly string BulletPath = "Prefabs/Bullets/Bullet";
        public static readonly string WeaponConfigLabel = "WeaponConfigs";

        public static Dictionary<Direction, Vector3> DirectionVectors = new Dictionary<Direction, Vector3>()
        {
            { Direction.Forward , Vector3.forward},
            { Direction.Right , Vector3.right},
            { Direction.Back , Vector3.back},
            { Direction.Left , Vector3.left}
        };
    }
    
    [Serializable]
    public class WeaponConfigsJson
    {
        public List<WeaponData> configs;
    }

    [Serializable]
    public class WeaponData
    {
        public List<UpgradeableAttribute> attributes;
        public float fireRate;
        public float areaOfEffect;
        public string weaponType;
        
        public void InitializeAttributes()
        {
            foreach (var attribute in attributes)
            {
                attribute.type = (int)attribute.GetUpgradeableType();
            }
        }
    }

    [Serializable]
    public class UpgradeableAttribute
    {
        public int type; 
        public float value;

        public UpgradeableType GetUpgradeableType()
        {
            return (UpgradeableType)type;
        }
    }

    
    [Serializable]
    public enum UpgradeableType
    {
        Range = 0,
        Penetration,
        Damage
    }

    [Serializable]
    public enum WeaponType
    {
        Pistol = 0,
        Rifle,
        RocketLauncher,
    }

    public enum BulletType
    {
        Pistol = 0,
        Rifle,
        RocketLauncher
    }

    public enum Direction
    {
        Forward = 0,
        Right,
        Back,
        Left,
    }

    public class OnDirectionChanged
    {
        public Direction Direction;

        public OnDirectionChanged(Direction direction)
        {
            Direction = direction;
        }
    }
}
