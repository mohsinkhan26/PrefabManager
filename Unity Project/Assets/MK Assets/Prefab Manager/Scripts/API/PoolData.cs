/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MK.Prefab.API
{
    [Serializable]
    public class PoolData
    {
        public Transform poolParent;
        public List<GameObject> pooledObjects;

        public PoolData(Transform _poolParent, List<GameObject> _pooledObjects)
        {
            poolParent = _poolParent;
            pooledObjects = new List<GameObject>();
            pooledObjects = _pooledObjects;
        }
    }
}
