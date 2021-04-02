/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using UnityEngine;

/* Reference: https://blogs.unity3d.com/2016/07/26/il2cpp-optimizations-devirtualization/
 * write 'sealed' keyword with each singleton inheriting classes, if they are leaf nodes, 
 * so by 'sealed' keyword the overriding function calls become optimized, Devirtualization
*/

namespace MK.Common.Utilities
{
    public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        // use it for testing
        public string InstanceID;

        private static object _lock = new object();

        protected virtual void Start()
        {
        }

        protected virtual void Awake()
        {
            if (ReferenceEquals(_instance, null))
            {
                //If I am the first instance, make me the Singleton
                bool has = ReferenceEquals(Instance, null); // to access Instance, so it won't create new instance of same class. Important point: Don't comment this line
                DontDestroyOnLoad(this);
                InstanceID = this.GetInstanceID().ToString();
                //Debug.LogWarning(typeof(T) + " - " + has + "   ID: " + InstanceID);
            }
            else
            {
                //If a Singleton already exists and you find
                //another reference in scene, destroy it!
                //if (!ReferenceEquals(_instance, null))
                //    Debug.LogError("[Singelton]Destroying: " + this.gameObject.name + "   New InstanceID: " + this.GetInstanceID() + "   ->Old: " + _instance.GetInstanceID());
                Destroy(this.gameObject);
            }
        }

        public static bool HasInstance
        {
            get
            {
                return (!ReferenceEquals(_instance, null));
            }
        }

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    if (ReferenceEquals(_instance, null))
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " + typeof(T) +
                                " - there should never be more than 1 singleton!" +
                                " Reopenning the scene might fix it.");
                            return _instance;
                        }

                        if (ReferenceEquals(_instance, null))
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(Singleton) " + typeof(T).ToString();

                            Debug.LogWarning("[Singleton] An instance of " + typeof(T) +
                                " is needed in the scene, so '" + singleton.name +
                                "' was created.");
                        }
                        else
                        {
                            Debug.LogWarning("[Singleton] " + typeof(T) + " - Using instance already created: " +
                                _instance.gameObject.name);
                        }
                    }
                    return _instance;
                }
            }
        }

        protected virtual void OnApplicationQuit()
        {
            if (!ReferenceEquals(_instance, null))
                _instance = null;
        }

        protected virtual void OnDestroy()
        {
            if (!ReferenceEquals(_instance, null))
                _instance = null;
        }
    }
}
