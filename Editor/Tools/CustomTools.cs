using UnityEditor;
using UnityEngine;


namespace WS.Auto
{
    public class CustomTools : EditorWindow
    {
        [MenuItem("自定义/工具", false, 105)]
        private static void ShowWindow()
        {
            var window = GetWindow<CustomTools>();
            window.titleContent = new GUIContent("工具");
            window.Show();
        }

     
        private Vector3 m_EulerAngle;
        private Color m_Color;
 
        protected void OnGUI()
        {
            EditorGUILayout.Space(20);
            EditorGUILayout.LabelField("工具");

            m_EulerAngle = EditorGUILayout.Vector3Field("欧拉角", m_EulerAngle);
            if (GUILayout.Button("欧拉角to四元数")) Euler2Quaternion();
            
            EditorGUILayout.Space(10);
            
            m_Color = EditorGUILayout.ColorField("颜色测试", m_Color);
            
        }

        private void Euler2Quaternion()
        {
            Debug.Log(Quaternion.Euler(m_EulerAngle).ToString("f4") );
        }
    }
}