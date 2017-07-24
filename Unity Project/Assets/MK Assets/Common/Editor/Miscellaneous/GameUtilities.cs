/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/unbounded-eagle/ 
*/
using UnityEditor;
using UnityEngine;

namespace MK.Common.Miscellaneous
{
    public static class GameUtilities
    {
        [MenuItem("Tools/MK Assets/Check All Plugins", false, 50)]
        public static void CheckAllPlugins()
        {
            Application.OpenURL("https://assetstore.unity.com/publishers/28971");
        }

        [MenuItem("Tools/MK Assets/OpenScene/Prefab Manager - Demo Scene")]
        public static void OpenPrefabDemoScene()
        {
            OpenScene("Demo");
        }

        static void OpenScene(string name)
        {
            if (UnityEditor.SceneManagement.EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
            {
                UnityEditor.SceneManagement.EditorSceneManager.OpenScene("Assets/MK Assets/Prefab Manager/Scene/" + name + ".unity");
            }
        }
    }
}
