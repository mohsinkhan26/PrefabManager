/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using UnityEngine;
using MK.Prefab.Manager;
using MK.Prefab.API;

public class Demo : MonoBehaviour
{
    [SerializeField]
    Vector3 positionToSpawn;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
            PrefabManager.Instance.RecycleAll();
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            PrefabManager.Instance.Spawn(PrefabType.Cube, positionToSpawn);
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            PrefabManager.Instance.Spawn(PrefabType.Sphere, positionToSpawn);
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            PrefabManager.Instance.Spawn(PrefabType.Capsule, positionToSpawn);
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            PrefabManager.Instance.Spawn(PrefabType.Cylinder, positionToSpawn);
    }
}
