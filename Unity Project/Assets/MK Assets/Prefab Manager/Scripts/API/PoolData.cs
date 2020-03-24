/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
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
