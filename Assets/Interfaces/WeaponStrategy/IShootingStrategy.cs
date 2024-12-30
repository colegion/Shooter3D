using Helpers;
using Scriptables.Weapons;
using UnityEngine;

namespace Interfaces.WeaponStrategy
{
    public interface IShootingStrategy
    {
        public abstract void Shoot(Vector3 target, WeaponConfig config);
    }
}
