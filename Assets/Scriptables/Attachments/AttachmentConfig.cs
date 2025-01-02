using Helpers;
using UnityEngine;

namespace Scriptables.Upgradeables
{
    [CreateAssetMenu(fileName = "New Upgradeable Config", menuName = "ScriptableObjects/ Upgradeable Config")]
    public class AttachmentConfig : ScriptableObject
    {
        public AttachmentAttribute data;
        public Mesh mesh;
        public Material material;
    }
}
