using UnityEngine;

namespace Patterns
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        
        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    return null;
                }
                
                if (_instance == null)
                {
                    T[] instances = FindObjectsOfType(typeof(T)) as T[];
                    if (instances.Length != 0)
                    {
                        if (instances.Length == 1)
                        {
                            _instance = instances[0];
                            _instance.gameObject.name = $"[{typeof(T).Name}]";
                            return _instance;
                        }
                        else
                        {
                            Debug.LogError($"Multiple singleton instances of {typeof(T).Name} detected. Destroying...");
                            foreach (T instance in instances)
                            {
                                Destroy(instance.gameObject);
                            }
                        }
                    }
                    var obj = new GameObject($"[{typeof(T).Name}]", typeof(T));
                    _instance = obj.GetComponent<T>();
                    DontDestroyOnLoad(obj);
                }
                return _instance;
            }
            set => _instance = value as T;
        }
        
        private static bool _applicationIsQuitting = false;
        
        [RuntimeInitializeOnLoadMethod]
        public static void RunOnStart()
        {
            Application.quitting += () => _applicationIsQuitting = true;
        }
    }
}


