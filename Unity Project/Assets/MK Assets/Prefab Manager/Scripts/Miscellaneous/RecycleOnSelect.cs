/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
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
