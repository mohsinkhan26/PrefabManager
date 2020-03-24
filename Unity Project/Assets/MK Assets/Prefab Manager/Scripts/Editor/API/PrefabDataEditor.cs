/* 
 * Author : Mohsin Khan
 * LinkedIn : http://pk.linkedin.com/in/mohsinkhan26/
 * Github : https://github.com/mohsinkhan26/
 * BitBucket : https://bitbucket.org/mohsinkhan26/ 
*/
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;

namespace MK.Prefab.API
{
    [CustomEditor(typeof(PrefabData))]
    public class PrefabDataEditor : Editor
    {
        SerializedProperty m_StartupPoolMode;

        private ReorderableList prefabsDataList;
        const float LINE_MARGIN = 5f;

        private void OnEnable()
        {
            m_StartupPoolMode = serializedObject.FindProperty("startupPoolMode");

            prefabsDataList = new ReorderableList(serializedObject, serializedObject.FindProperty("prefabsDetail"), true, true, true, true);
            prefabsDataList.elementHeight = 70f;

            prefabsDataList.drawHeaderCallback = (Rect rect) =>
            {
                EditorGUI.LabelField(rect, "Prefabs Data");
            };

            prefabsDataList.drawElementCallback =
                (Rect rect, int index, bool isActive, bool isFocused) =>
            { // called to draw the elements

                var element = prefabsDataList.serializedProperty.GetArrayElementAtIndex(index);
                rect.y += 2;
                float posX = rect.x;
                float rowWidth = rect.width / 2.5f;

                // prefab type
                EditorGUI.PropertyField(
                    new Rect(posX, rect.y, rowWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("prefabType"), GUIContent.none);

                // pool count
                posX += (rowWidth * 1.08f);
                //EditorGUI.LabelField(new Rect(posX, rect.y, rowWidth, EditorGUIUtility.singleLineHeight), "(100)" + pooledObjects[((MK.Prefab.API.PrefabType)element.FindPropertyRelative("prefabType").enumValueIndex)].pooledObjects.Count.ToString());

                // auto scale
                posX += rowWidth * 0.53f;
                rowWidth = rect.width / 2.5f;
                element.FindPropertyRelative("autoScale").boolValue = EditorGUI.ToggleLeft(new Rect(posX, rect.y, rowWidth, EditorGUIUtility.singleLineHeight), "Auto Scale",
                    element.FindPropertyRelative("autoScale").boolValue);

                // initial pool size
                rowWidth = rect.width / 2.5f;
                rect.y += EditorGUIUtility.singleLineHeight + LINE_MARGIN;
                EditorGUI.LabelField(new Rect(rect.x, rect.y, rowWidth, EditorGUIUtility.singleLineHeight), "Initial Pool Size");
                posX = rect.x + rowWidth;
                rowWidth = rect.width / 1.69f;
                EditorGUI.PropertyField(new Rect(posX, rect.y, rowWidth, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("initialPoolSize"), GUIContent.none);

                // prefab reference
                rect.y += EditorGUIUtility.singleLineHeight + LINE_MARGIN;
                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight),
                    element.FindPropertyRelative("prefab"), GUIContent.none);
            };

            prefabsDataList.onSelectCallback = (ReorderableList l) =>
            { // called when an entry is selected
                var gObject = l.serializedProperty.GetArrayElementAtIndex(l.index).FindPropertyRelative("prefab").objectReferenceValue as GameObject;
                if (gObject)
                    EditorGUIUtility.PingObject(gObject);
            };

            prefabsDataList.onCanRemoveCallback = (ReorderableList l) =>
            {
                return l.count > 1;
            };

            prefabsDataList.onRemoveCallback = (ReorderableList l) =>
            {
                if (EditorUtility.DisplayDialog("Warning!",
                        "Are you sure you want to delete the prefab? It might be used in code.", "Yes", "No"))
                {
                    ReorderableList.defaultBehaviours.DoRemoveButton(l);
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_StartupPoolMode);
            EditorGUILayout.LabelField("Prefabs Data Count", prefabsDataList.count.ToString());
            prefabsDataList.DoLayoutList();

            serializedObject.ApplyModifiedProperties();
        }
    }
}
