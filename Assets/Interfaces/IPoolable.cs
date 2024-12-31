using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        public void OnCreatedForPool();
        public void OnAssignPool();
        public void OnReleasePool();
        public void OnDeletePool();
        public GameObject GameObject();
        

    }
}
