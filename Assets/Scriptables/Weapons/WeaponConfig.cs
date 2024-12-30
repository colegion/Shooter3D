using System.Collections.Generic;
using Helpers;
using Interfaces.WeaponStrategy;
using Scriptables.Upgradeables;
using UnityEngine;

namespace Scriptables.Weapons
{
    [CreateAssetMenu(fileName = "New Weapon Config", menuName = "ScriptableObjects/Weapon Config")]
    public class WeaponConfig : ScriptableObject
    {
        public List<UpgradeableAttribute> upgradeableAttributes;
        public float fireRate;
        public float areaOfEffect;
        public Mesh weaponMesh;
        public Material weaponMaterial;
        public WeaponType weaponType;
        
        public float Damage => GetAttributeValue(UpgradeableType.Damage);
        public float ArmorPenetration => GetAttributeValue(UpgradeableType.Penetration);
        public float Range => GetAttributeValue(UpgradeableType.Range);
        
        public void ApplyEffect(UpgradeableConfig attribute)
        {
            var field = upgradeableAttributes.Find(x => x.type == attribute.data.type);
            field.value += attribute.data.value;
        }
        
        private float GetAttributeValue(UpgradeableType type)
        {
            var attribute = upgradeableAttributes.Find(x => x.type == type);
            return attribute?.value ?? 0f;
        }
    }
}
