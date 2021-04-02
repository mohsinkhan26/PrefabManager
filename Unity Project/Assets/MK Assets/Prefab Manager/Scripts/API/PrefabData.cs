/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace MK.Prefab.API
{
    [CreateAssetMenu(fileName = "PrefabData", menuName = "Game/Prefab Data", order = 1)]
    public class PrefabData : ScriptableObject
    {
        [SerializeField] List<PrefabDetail> prefabsDetail;

        [SerializeField] StartupPoolMode startupPoolMode;

        public StartupPoolMode StartupPoolMode
        {
            get
            {
                return startupPoolMode;
            }
        }

        public int PrefabsCount
        {
            get
            {
                return prefabsDetail.Count;
            }
        }

        /// <summary>
        /// Gets the type of the prefab.
        /// </summary>
        /// <returns>The prefab type.</returns>
        /// <param name="_index">Index.</param>
        public PrefabType GetPrefabType(int _index)
        {
            if (prefabsDetail[_index].prefab == null)
                return prefabsDetail[_index].prefabType;
            throw new IndexOutOfRangeException("Index is out of range");
        }

        /// <summary>
        /// Determines whether this instance has prefab the specified _prefabType.
        /// </summary>
        /// <returns><c>true</c> if this instance has prefab the specified _prefabType; otherwise, <c>false</c>.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public bool HasPrefab(PrefabType _prefabType)
        {
            if (prefabsDetail.FirstOrDefault(item => item.prefabType == _prefabType).prefab == null)
                return false;
            return true;
        }

        public bool HasPrefab(int _index)
        {
            if (prefabsDetail[_index].prefab == null)
                return false;
            return true;
        }

        /// <summary>
        /// Gets the prefab details.
        /// </summary>
        /// <returns>The prefab details.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public PrefabDetail GetPrefabDetails(PrefabType _prefabType)
        {
            if (HasPrefab(_prefabType))
                return prefabsDetail.FirstOrDefault(item => item.prefabType == _prefabType);
            return null;
        }

        public PrefabDetail GetPrefabDetails(int _index)
        {
            if (HasPrefab(_index))
                return prefabsDetail[_index];
            return null;
        }

        /// <summary>
        /// Gets the initial size of the pool.
        /// </summary>
        /// <returns>The initial pool size.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public int GetInitialPoolSize(PrefabType _prefabType)
        {
            if (HasPrefab(_prefabType))
                return GetPrefabDetails(_prefabType).initialPoolSize;
            return 0;
        }

        public int GetInitialPoolSize(int _index)
        {
            if (HasPrefab(_index))
                return GetPrefabDetails(_index).initialPoolSize;
            return 0;
        }

        /// <summary>
        /// Determines whether this instance can auto scale the specified _prefabType.
        /// </summary>
        /// <returns><c>true</c> if this instance can auto scale the specified _prefabType; otherwise, <c>false</c>.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public bool CanAutoScale(PrefabType _prefabType)
        {
            if (HasPrefab(_prefabType))
                return GetPrefabDetails(_prefabType).autoScale;
            return false;
        }

        public bool CanAutoScale(int _index)
        {
            if (HasPrefab(_index))
                return GetPrefabDetails(_index).autoScale;
            return false;
        }

        /// <summary>
        /// Gets the prefab.
        /// </summary>
        /// <returns>The prefab.</returns>
        /// <param name="_prefabType">Prefab type.</param>
        public GameObject GetPrefab(PrefabType _prefabType)
        {
            if (HasPrefab(_prefabType))
                return GetPrefabDetails(_prefabType).prefab;
            return null;
        }

        public GameObject GetPrefab(int _index)
        {
            if (HasPrefab(_index))
                return GetPrefabDetails(_index).prefab;
            return null;
        }

#if UNITY_EDITOR
        [ContextMenu("Remove Entries which don't have prefab reference")]
        void RemoveNullEntries()
        {
            for (int i = PrefabsCount - 1; i >= 0; --i)
            {
                if (HasPrefab(i)) { }
                else
                {
                    prefabsDetail.RemoveAt(i);
                }
            }
        }

        [ContextMenu("Remove NONE Types")]
        void RemoveNoneEntries()
        {
            if (prefabsDetail.Count(p => p.prefabType == PrefabType.None) > 0)
            {
                prefabsDetail.RemoveAll(p => p.prefabType == PrefabType.None);
            }
        }

        [ContextMenu("Remove Dublicates")]
        void RemoveDublicates()
        {
            for (int i = ParseEnum<PrefabType>().Count - 1; i >= 0; --i)
            {
                if (prefabsDetail.Count(p => p.prefabType == ParseEnum<PrefabType>(i)) > 1)
                {
                    RemoveEntry(ParseEnum<PrefabType>(i), prefabsDetail.Count(p => p.prefabType == ParseEnum<PrefabType>(i)));
                }
            }
        }

        void RemoveEntry(PrefabType _prefabType, int _count)
        {
            for (int i = _count - 2; i >= 0; --i)
            {
                prefabsDetail.Remove(prefabsDetail.LastOrDefault(p => p.prefabType == _prefabType));
            }
        }

        List<string> ParseEnum<T>()
        {
            return Enum.GetNames(typeof(T)).ToList();
        }

        T ParseEnum<T>(int _value)
        {
            return ParseEnum<T>(_value.ToString());
        }

        T ParseEnum<T>(string _value)
        {
            return (T)Enum.Parse(typeof(T), _value, true);
        }
#endif
    }
}
