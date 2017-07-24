/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using System;
using UnityEngine;

namespace MK.Prefab.API
{
    [Serializable]
    public class PrefabData
    {
        public PrefabType prefabType;

        public GameObject prefab;

        [Range(0, 50)]
        public int initialPoolSize;

        [Tooltip("Make the pool auto-scale")]
        public bool autoScale;
    }
}
