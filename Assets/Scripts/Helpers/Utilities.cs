using System.Collections.Generic;
using UnityEngine;

namespace Helpers
{
    public class Utilities : MonoBehaviour
    {
        public static string BulletPath = "Prefabs/Bullet";

        public static Dictionary<Direction, Vector3> DirectionVectors = new Dictionary<Direction, Vector3>()
        {
            { Direction.Forward , Vector3.forward},
            { Direction.Right , Vector3.right},
            { Direction.Back , Vector3.back},
            { Direction.Left , Vector3.left}
        };
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
