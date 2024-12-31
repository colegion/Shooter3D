using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        public void OnCreatedForPool();
        public void OnReleasePool();
        public GameObject GameObject();
        

    }
}
