/* 
 * Author : Mohsin Khan
 * Portfolio : http://mohsinkhan26.github.io/ 
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
*/
using UnityEngine;
using MK.Prefab.Manager;

namespace MK.Prefab.Miscellaneous
{
    public class RecycleSelf : MonoBehaviour
    {
        [SerializeField] float life = 5.0f;

        void OnEnable()
        {
            Invoke("Recycle", life);
        }

        void OnDisable()
        {
            //CancelInvoke("Recycle");
        }

        void Recycle()
        {
            PrefabManager.Instance.Recycle(gameObject);
        }
    }
}
