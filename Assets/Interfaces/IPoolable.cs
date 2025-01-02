using UnityEngine;

namespace Interfaces
{
    public interface IPoolable
    {
        public void OnCreatedForPool();
        public void OnReleaseFromPool();
        public GameObject GameObject();
        

    }
}
