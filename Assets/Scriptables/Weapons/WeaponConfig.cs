using Helpers;
using Interfaces.WeaponStrategy;
using UnityEngine;

namespace Scriptables.Weapons
{
    [CreateAssetMenu(fileName = "New Weapon Config", menuName = "ScriptableObjects/Weapon Config")]
    public class WeaponConfig : ScriptableObject
    {
        public float damage;
        public float armorPenetration;
        public float range;
        public float fireRate;
        public float areaOfEffect;
        public Mesh weaponMesh;
        public Material weaponMaterial;
        public WeaponType weaponType;
    }
}
