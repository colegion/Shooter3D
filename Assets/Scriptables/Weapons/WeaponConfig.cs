using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Interfaces.WeaponStrategy;
using Scriptables.Upgradeables;
using UnityEngine;

namespace Scriptables.Weapons
{
    [CreateAssetMenu(fileName = "New Weapon Config", menuName = "ScriptableObjects/Weapon Config")]
    public class WeaponConfig : ScriptableObject
    {
        public List<AttachmentAttribute> upgradeableAttributes;
        public float fireRate;
        public float areaOfEffect;
        public Mesh weaponMesh;
        public Material weaponMaterial;
        public WeaponType weaponType;
        
        public float Damage => GetAttributeValue(UpgradeableType.Damage);
        public float ArmorPenetration => GetAttributeValue(UpgradeableType.Penetration);
        public float Range => GetAttributeValue(UpgradeableType.Range);

        public void Initialize(WeaponData data)
        {
            upgradeableAttributes = data.attributes.Select(attr => new AttachmentAttribute
            {
                type = attr.type,
                value = attr.value
            }).ToList();
        
            fireRate = data.fireRate;
            areaOfEffect = data.areaOfEffect;
            
            if (Enum.TryParse(data.weaponType, out WeaponType weaponTypeEnum))
                weaponType = weaponTypeEnum;
        }

        public void ApplyEffect(AttachmentConfig attribute)
        {
            var field = upgradeableAttributes.Find(x => x.type == attribute.data.type);
            if (field != null)
            {
                field.value += attribute.data.value;
            }
        }
        
        private float GetAttributeValue(UpgradeableType type)
        {
            var attribute = upgradeableAttributes.Find(x => x.type == (int)type);
            return attribute?.value ?? 0f;
        }

    }
}
