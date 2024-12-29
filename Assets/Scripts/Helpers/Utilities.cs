using UnityEngine;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
        public static string BulletPath = "Prefabs/Bullet";
    }

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
}
