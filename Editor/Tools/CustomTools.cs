using UnityEditor;
using UnityEngine;


namespace WS.Auto
{
    public class CustomTools : EditorWindow
    {
        [MenuItem("自定义/工具")]
        private static void ShowWindow()
        {
            var window = GetWindow<CustomTools>();
            window.titleContent = new GUIContent("TITLE");
            window.Show();
        }

     
        private Vector3 m_EulerAngle;

 
        protected void OnGUI()
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("工具");

            m_EulerAngle = EditorGUILayout.Vector3Field("欧拉角", m_EulerAngle);

            if (GUILayout.Button("欧拉角to四元数")) Euler2Quaternion();
        }

        private void Euler2Quaternion()
        {
            Debug.Log(Quaternion.Euler(m_EulerAngle).ToString("f4") );
        }
    }
}