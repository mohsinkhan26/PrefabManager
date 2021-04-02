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
    public class RecycleOnSelect : MonoBehaviour
    {
        void OnMouseDown()
        {
            PrefabManager.Instance.Recycle(gameObject);
        }
    }
}
