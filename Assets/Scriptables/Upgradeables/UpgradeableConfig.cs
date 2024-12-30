using Helpers;
using UnityEngine;

namespace Scriptables.Upgradeables
{
    [CreateAssetMenu(fileName = "New Upgradeable Config", menuName = "ScriptableObjects/ Upgradeable Config")]
    public class UpgradeableConfig : ScriptableObject
    {
        public UpgradeableAttribute data;
    }
}
