using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WS.Auto
{
    public class CreateBuildSettings
{
    [MenuItem("自定义/构建/项目配置")]
    private static void _CreateBuildSettings()
    {
        if ((UnityEngine.Object) BuildSettings.NullableInstance == (UnityEngine.Object) null)
        {
            //Debug.Log("AAAAAAAAAAAA");
            BuildSettings instance = ScriptableObject.CreateInstance<BuildSettings>();
            string path1 = Path.Combine(Application.dataPath, "_WS_Auto_/Editor/AutoBuildPipeline");
            if (!Directory.Exists(path1))
                Directory.CreateDirectory(path1);
            string path2 = Path.Combine(Path.Combine("Assets", "_WS_Auto_/Editor/AutoBuildPipeline"), "BuildSettings.asset");
            AssetDatabase.CreateAsset((UnityEngine.Object) instance, path2);
        }

        //Debug.Log("BBBBBBBBBBBBBBBBBB");
        Selection.activeObject = (UnityEngine.Object) BuildSettings.Instance;
    }
}
    
}