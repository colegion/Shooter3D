using System.Collections;
using UnityEngine;

namespace Helpers
{
    public class CoroutineHandler : MonoBehaviour
    {
        private static CoroutineHandler _instance;

        public static CoroutineHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    var obj = new GameObject("CoroutineHandler");
                    _instance = obj.AddComponent<CoroutineHandler>();
                    DontDestroyOnLoad(obj);
                }
                return _instance;
            }
        }

        public void StartManagedCoroutine(IEnumerator coroutine)
        {
            StartCoroutine(coroutine);
        }
    }
}