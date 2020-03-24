/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using System;
using UnityEngine;

namespace MK.Prefab.API
{
    [Serializable]
    public class PrefabDetail
    {
        public PrefabType prefabType;

        public GameObject prefab;

        [Range(0, 50)]
        public int initialPoolSize;

        [Tooltip("Make the pool auto-scale")]
        public bool autoScale;
    }
}
