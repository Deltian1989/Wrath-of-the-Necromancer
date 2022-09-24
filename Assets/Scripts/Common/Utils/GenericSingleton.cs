using UnityEngine;

namespace WotN.Common.Utils
{
    public class GenericSingleton<T> : MonoBehaviour where T : Component
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = FindObjectOfType<T>();
                }

                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (instance == null)
            {
                instance = this as T;
                DontDestroyOnLoad(this.gameObject);
            }
            else
            {
                Debug.LogError($"Instance of {typeof(T).Name} already exists!");
            }
        }
    }
}
