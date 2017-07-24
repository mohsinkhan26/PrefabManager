/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using MK.Common.Utilities;
using MK.Prefab.API;
using System;

namespace MK.Prefab.Manager
{
    public sealed class PrefabManager : Singleton<PrefabManager>
    {
        [SerializeField]
        List<PrefabData> prefabsData = new List<PrefabData>();

        [SerializeField]
        StartupPoolMode startupPoolMode;
        bool startupPoolsCreated = false;
        Dictionary<PrefabType, PoolData> pooledObjects = new Dictionary<PrefabType, PoolData>();

        #region inherited functions

        protected override void Awake()
        {
            base.Awake();
            if (startupPoolMode == StartupPoolMode.Awake)
                CreateStartupPool();
        }

        protected override void Start()
        {
            base.Start();
            if (startupPoolMode == StartupPoolMode.Start)
                CreateStartupPool();
        }

        #endregion inherited functions

        #region Pooling API

        public void CreateStartupPool()
        {
            if (HasInstance && !startupPoolsCreated)
            {
                startupPoolsCreated = true;
                for (int i = prefabsData.Count - 1; i >= 0; --i)
                {
                    if (prefabsData[i].prefab != null && prefabsData[i].initialPoolSize > 0)
                        CreatePool(prefabsData[i].prefabType, prefabsData[i].initialPoolSize, prefabsData[i].prefab);
                }
            }
        }

        //[Obsolete("Use this function if and only if InitialPoolSize is set to Max from editor. Otherwise, pooling goal would be lost")]

        /// <summary>
        /// Increments the pool. Don't use this function, instead mark auto scale from inspector
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_howMany">How many.</param>
        public void IncrementPool(PrefabType _prefabType, int _howMany)
        {
            CreatePool(_prefabType, _howMany, GetPrefab(_prefabType));
        }

        public T Spawn<T>(PrefabType _prefabType) where T : Component
        {
            return Spawn(_prefabType, null, Vector3.zero, Quaternion.identity).GetComponent<T>();
        }

        public T Spawn<T>(PrefabType _prefabType, Transform _parent) where T : Component
        {
            return Spawn(_prefabType, _parent, Vector3.zero, Quaternion.identity).GetComponent<T>();
        }

        public T Spawn<T>(PrefabType _prefabType, Vector3 _position) where T : Component
        {
            return Spawn(_prefabType, null, _position, Quaternion.identity).GetComponent<T>();
        }

        public T Spawn<T>(PrefabType _prefabType, Transform _parent, Vector3 _position) where T : Component
        {
            return Spawn(_prefabType, _parent, _position, Quaternion.identity).GetComponent<T>();
        }

        public T Spawn<T>(PrefabType _prefabType, Quaternion _rotation) where T : Component
        {
            return Spawn(_prefabType, null, Vector3.zero, _rotation).GetComponent<T>();
        }

        public T Spawn<T>(PrefabType _prefabType, Transform _parent, Quaternion _rotation) where T : Component
        {
            return Spawn(_prefabType, _parent, Vector3.zero, _rotation).GetComponent<T>();
        }

        public T Spawn<T>(PrefabType _prefabType, Transform _parent, Vector3 _position, Quaternion _rotation) where T : Component
        {
            return Spawn(_prefabType, _parent, _position, _rotation).GetComponent<T>();
        }

        /// <summary>
        /// Spawn the specified _prefabType, _parent, _position and _rotation.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_parent">Parent.</param>
        /// <param name="_position">Position.</param>
        /// <param name="_rotation">Rotation.</param>
        public GameObject Spawn(PrefabType _prefabType, Transform _parent, Vector3 _position, Quaternion _rotation)
        {
            if (HasPrefab(_prefabType))
            {
                if (!pooledObjects.ContainsKey(_prefabType))
                { // prefab type not exists, so creating it first
                    // making a heirarchy/structured pooling under the prefab type
                    GameObject gObject = InstantiateGameObject(GetPrefab(_prefabType), transform, Vector3.zero, Quaternion.identity, false, _prefabType.ToString(), true);

                    List<GameObject> pool = new List<GameObject>();
                    pooledObjects.Add(_prefabType, new PoolData(gObject.transform, pool));
                }
                return GetActiveGameObject(_prefabType, _parent, _position, _rotation);
            }
            throw new NullReferenceException("PrefabManager-" + _prefabType + " do not has Prefab reference in the 'prefabsData' list");
        }

        /// <summary>
        /// Spawn the specified _prefabType.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        public GameObject Spawn(PrefabType _prefabType)
        {
            return Spawn(_prefabType, null, Vector3.zero, Quaternion.identity);
        }

        /// <summary>
        /// Spawn the specified _prefabType and _parent.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_parent">Parent.</param>
        public GameObject Spawn(PrefabType _prefabType, Transform _parent)
        {
            return Spawn(_prefabType, _parent, Vector3.zero, Quaternion.identity);
        }

        /// <summary>
        /// Spawn the specified _prefabType and _position.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_position">Position.</param>
        public GameObject Spawn(PrefabType _prefabType, Vector3 _position)
        {
            return Spawn(_prefabType, null, _position, Quaternion.identity);
        }

        /// <summary>
        /// Spawn the specified _prefabType, _parent and _position.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_parent">Parent.</param>
        /// <param name="_position">Position.</param>
        public GameObject Spawn(PrefabType _prefabType, Transform _parent, Vector3 _position)
        {
            return Spawn(_prefabType, _parent, _position, Quaternion.identity);
        }

        /// <summary>
        /// Spawn the specified _prefabType and _rotation.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_rotation">Rotation.</param>
        public GameObject Spawn(PrefabType _prefabType, Quaternion _rotation)
        {
            return Spawn(_prefabType, null, Vector3.zero, _rotation);
        }

        /// <summary>
        /// Spawn the specified _prefabType, _parent and _rotation.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_parent">Parent.</param>
        /// <param name="_rotation">Rotation.</param>
        public GameObject Spawn(PrefabType _prefabType, Transform _parent, Quaternion _rotation)
        {
            return Spawn(_prefabType, _parent, Vector3.zero, _rotation);
        }

        /// <summary>
        /// Recycle the specified _gameObject.
        /// </summary>
        /// <param name="_gameObject">Game object.</param>
        public void Recycle(GameObject _gameObject)
        {
            for (int i = pooledObjects.Keys.Count - 1; i >= 0; --i)
            {
                PrefabType prefabType = pooledObjects.Keys.ToList()[i];
                if (pooledObjects[prefabType].pooledObjects.Contains(_gameObject))
                {
                    int j = pooledObjects[prefabType].pooledObjects.IndexOf(_gameObject);
                    //for (int j = pooledObjects[prefabType].pooledObjects.Count - 1; j >= 0; --j){
                    //if (pooledObjects[prefabType].pooledObjects[j].Equals(_gameObject))
                    if (pooledObjects[prefabType].pooledObjects[j].Equals(_gameObject))
                    {
                        pooledObjects[prefabType].pooledObjects[j].transform.SetParent(pooledObjects[prefabType].poolParent, false);
                        pooledObjects[prefabType].pooledObjects[j].SetActive(false);
                        return;
                    }
                    //}
                }
            }
            // if not found in our pooledObjects list, then destroy
            Debug.LogWarning("Destroying unpooled object with name: " + _gameObject.name);
            Destroy(_gameObject);
        }

        /// <summary>
        /// Recycles all.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        public void RecycleAll(PrefabType _prefabType)
        {
            for (int i = pooledObjects[_prefabType].pooledObjects.Count - 1; i >= 0; --i)
            {
                pooledObjects[_prefabType].pooledObjects[i].transform.SetParent(pooledObjects[_prefabType].poolParent, false);
                pooledObjects[_prefabType].pooledObjects[i].SetActive(false);
            }
        }

        /// <summary>
        /// Recycles all.
        /// </summary>
        public void RecycleAll()
        {
            for (int i = pooledObjects.Keys.Count - 1; i >= 0; --i)
                RecycleAll(pooledObjects.Keys.ElementAt(i));
        }

        /// <summary>
        /// Counts the active spawned pooled objects of specified type in scene.
        /// </summary>
        /// <returns>The active pooled.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public int CountActivePooled(PrefabType _prefabType)
        {
            if (pooledObjects.ContainsKey(_prefabType))
                return pooledObjects[_prefabType].pooledObjects.Count(go => go.activeInHierarchy);
            return 0;
        }

        /// <summary>
        /// Counts the inactive pooled objects of specified type in scene which can be spawned.
        /// </summary>
        /// <returns>The inactive pooled.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public int CountInactivePooled(PrefabType _prefabType)
        {
            if (pooledObjects.ContainsKey(_prefabType))
                return (CountPooled(_prefabType) - CountActivePooled(_prefabType));
            return 0;
        }

        /// <summary>
        /// Counts the total pooled objects of specified type.
        /// </summary>
        /// <returns>The pooled.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public int CountPooled(PrefabType _prefabType)
        {
            if (pooledObjects.ContainsKey(_prefabType))
                return pooledObjects[_prefabType].pooledObjects.Count;
            return 0;
        }

        /// <summary>
        /// Counts all pooled objects of all types.
        /// </summary>
        /// <returns>The all pooled.</returns>
        public int CountAllPooled()
        {
            int count = 0;
            for (int i = pooledObjects.Keys.Count - 1; i >= 0; --i)
                count += pooledObjects[pooledObjects.Keys.ElementAt(i)].pooledObjects.Count;
            return count;
        }

        /// <summary>
        /// Destroies all.
        /// </summary>
        public void DestroyAll()
        {
            for (int i = pooledObjects.Keys.Count - 1; i >= 0; --i)
                DestroyAll(pooledObjects.Keys.ElementAt(i));
        }

        /// <summary>
        /// Destroies all.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        public void DestroyAll(PrefabType _prefabType)
        {
            DestroyPooled(_prefabType, pooledObjects[_prefabType].pooledObjects.Count);
            pooledObjects[_prefabType].pooledObjects.Clear();
        }

        /// <summary>
        /// Destroies the pooled.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_howMany">How many.</param>
        public void DestroyPooled(PrefabType _prefabType, int _howMany)
        {
            _howMany = (pooledObjects[_prefabType].pooledObjects.Count >= _howMany) ? _howMany : pooledObjects[_prefabType].pooledObjects.Count;
            for (int i = _howMany - 1; i >= 0; --i)
            {
                Destroy(pooledObjects[_prefabType].pooledObjects[i]);
            }
        }

        #endregion Pooling API

        #region Helper functions

        /// <summary>
        /// Creates the pool.
        /// </summary>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_howMany">How many.</param>
        /// <param name="_prefab">Prefab.</param>
        void CreatePool(PrefabType _prefabType, int _howMany, GameObject _prefab)
        {
            if (pooledObjects.ContainsKey(_prefabType))
            {
                for (int i = _howMany - 1; i >= 0; --i)
                    pooledObjects[_prefabType].pooledObjects.Add(InstantiateGameObject(_prefab, pooledObjects[_prefabType].poolParent));
            }
            else
            {
                // making a heirarchy/structured pooling under the prefab type
                GameObject gObject = InstantiateGameObject(_prefab, transform, Vector3.zero, Quaternion.identity, false, _prefabType.ToString(), true);

                List<GameObject> pool = new List<GameObject>();
                for (int i = _howMany - 1; i >= 0; --i)
                    pool.Add(InstantiateGameObject(_prefab, gObject.transform));
                pooledObjects.Add(_prefabType, new PoolData(gObject.transform, pool));
            }
        }

        /// <summary>
        /// Instantiates the game object.
        /// </summary>
        /// <returns>The game object.</returns>
        /// <param name="_prefab">Prefab.</param>
        /// <param name="_parent">Parent.</param>
        /// <param name="_position">Position.</param>
        /// <param name="_rotation">Rotation.</param>
        /// <param name="_setActive">If set to <c>true</c> set active.</param>
        /// <param name="_gameObjectName">Game object name.</param>
        /// <param name="_emptyGameObject">If set to <c>true</c> empty game object. Used to make the hierarchy of pooled objects</param>
        GameObject InstantiateGameObject(GameObject _prefab, Transform _parent, 
                                         Vector3 _position = default(Vector3), Quaternion _rotation = default(Quaternion),
                                         bool _setActive = false, string _gameObjectName = "", bool _emptyGameObject = false)
        {
            GameObject gObject = _emptyGameObject ? new GameObject() : GameObject.Instantiate(_prefab);
            if (_parent != null)
                gObject.transform.SetParent(_parent, false);
            gObject.transform.localPosition = _position;
            gObject.transform.localRotation = _rotation;
            gObject.SetActive(_setActive);
            if (string.IsNullOrEmpty(_gameObjectName))
            { // default name of instantiated GameObject would be same as the prefab name with postfix "(Clone)"
            }
            else
            {
                gObject.name = _gameObjectName;
            }
            return gObject;
        }

        /// <summary>
        /// Determines whether this instance has prefab the specified _prefabType.
        /// </summary>
        /// <returns><c>true</c> if this instance has prefab the specified _prefabType; otherwise, <c>false</c>.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        bool HasPrefab(PrefabType _prefabType)
        {
            if (prefabsData.FirstOrDefault(item => item.prefabType == _prefabType).prefab != null)
                return true;
            return false;
        }

        /// <summary>
        /// Determines whether this instance can auto scale the specified _prefabType.
        /// </summary>
        /// <returns><c>true</c> if this instance can auto scale the specified _prefabType; otherwise, <c>false</c>.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        bool CanAutoScale(PrefabType _prefabType)
        {
            if (HasPrefab(_prefabType))
                return prefabsData.FirstOrDefault(item => item.prefabType == _prefabType).autoScale;
            return false;
        }

        /// <summary>
        /// Gets the prefab.
        /// </summary>
        /// <returns>The prefab.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        GameObject GetPrefab(PrefabType _prefabType)
        {
            if (!prefabsData.Exists(item => item.prefabType == _prefabType))
                return null;
            return prefabsData.FirstOrDefault(item => item.prefabType == _prefabType).prefab;
        }

        /// <summary>
        /// Gets the active game object.
        /// </summary>
        /// <returns>The active game object.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        /// <param name="_parent">Parent.</param>
        /// <param name="_position">Position.</param>
        /// <param name="_rotation">Rotation.</param>
        GameObject GetActiveGameObject(PrefabType _prefabType, Transform _parent, Vector3 _position, Quaternion _rotation)
        {
            if (pooledObjects[_prefabType].pooledObjects.Count == 0 ||
                pooledObjects[_prefabType].pooledObjects.Where(pObject => !pObject.gameObject.activeInHierarchy).ToList().Count == 0 ||
                pooledObjects[_prefabType].pooledObjects.Where(poolObject => !poolObject.gameObject.activeInHierarchy && poolObject.name.Equals(GetPrefab(_prefabType).name + "(Clone)")).ToList().Count == 0)
            {
                if (prefabsData.FirstOrDefault(pData => pData.prefabType == _prefabType).autoScale)
                { // allowed to auto-scale this specific pool
                    GameObject gObject = InstantiateGameObject(GetPrefab(_prefabType), _parent, _position, _rotation, true);
                    gObject.transform.localPosition = _position;
                    gObject.transform.localRotation = _rotation;
                    pooledObjects[_prefabType].pooledObjects.Add(gObject);
                    return gObject;
                }
                throw new ArgumentOutOfRangeException("PrefabManager-You are not allowning to auto-scale the pool of " + _prefabType + " in the 'prefabsData' list. \nJust tick auto-scale the pool of '" + _prefabType + "' in the 'prefabsData' list, it will work fine\n");
            }
            else
            {
                //GameObject gObject = pooledObjects[_prefabType].pooledObjects.FirstOrDefault(item => !item.gameObject.activeInHierarchy && item.name.Equals(_prefabType.ToString() + "(Clone)"));
                GameObject gObject = pooledObjects[_prefabType].pooledObjects.FirstOrDefault(item => !item.gameObject.activeInHierarchy);
                gObject.transform.localPosition = _position;
                gObject.transform.localRotation = _rotation;
                gObject.gameObject.SetActive(true);
                if (_parent == null)
                    gObject.transform.parent = null;
                else
                    gObject.transform.SetParent(_parent, false);
                return gObject;
            }
        }

        #endregion Helper functions
    }
}
