using UnityEngine;

namespace Scriptables.Bullets
{
    [CreateAssetMenu(fileName = "New Bullet Config", menuName = "ScriptableObjects/Bullet Config")]
    public class BulletConfig : ScriptableObject
    {
        public float speed;
        public Mesh bulletMesh;
        public Material bulletMaterial;
    }
}
