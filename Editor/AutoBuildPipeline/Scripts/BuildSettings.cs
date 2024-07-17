using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace WS.Auto
{
    public class BuildSettings : ScriptableObject
    {
        #region 通用

        public const string BuildSettingsAssetName = "";
        public const string BuildSettingsPath = "";
        public const string BuildSettingsAssetExtension = ".asset";

        private static List<BuildSettings.OnChangeCallback> onChangeCallbacks =
            new List<BuildSettings.OnChangeCallback>();

        private static BuildSettings instance;

        public static void RegisterChangeEventCallback(BuildSettings.OnChangeCallback callback) =>
            BuildSettings.onChangeCallbacks.Add(callback);

        public static void UnregisterChangeEventCallback(BuildSettings.OnChangeCallback callback) =>
            BuildSettings.onChangeCallbacks.Remove(callback);

        private static void SettingsChanged() =>
            BuildSettings.onChangeCallbacks.ForEach((Action<BuildSettings.OnChangeCallback>) (callback => callback()));

        public delegate void OnChangeCallback();


        public static BuildSettings Instance
        {
            get
            {
                BuildSettings.instance = BuildSettings.NullableInstance;
                if ((UnityEngine.Object) BuildSettings.instance == (UnityEngine.Object) null)
                    BuildSettings.instance = ScriptableObject.CreateInstance<BuildSettings>();
                return BuildSettings.instance;
            }
        }

        public static BuildSettings NullableInstance
        {
            get
            {
                if ((UnityEngine.Object) BuildSettings.instance == (UnityEngine.Object) null)
                    BuildSettings.instance =
                        AssetDatabase.LoadAssetAtPath<ScriptableObject>(
                                Path.Combine(Path.Combine("Assets", "_WS_Auto_/Editor/AutoBuildPipeline"),
                                    "BuildSettings.asset")) as
                            BuildSettings;
                return BuildSettings.instance;
            }
        }

        #endregion

        public string nameSpace;
        
        public string companyName = "";
        public string productName = "";
        public string version = "0.1";

        public string[] scenePaths;

        public _Android android;
        public _iOS iOS;


        [Serializable]
        public class _Android
        {
            public string packageName = "";
            public int bundleVersionCode = 0;

            public string keystoreName = "";
            public string keystorePass = "";
            public string keyaliasName = "";
            public string keyaliasPass = "";

            public bool separateAsset = false;
            public bool buildAAB = false;
        }


        [Serializable]
        public class _iOS
        {
            public string packageName = "";
            public string build = "0";
        }

        [Header("是否自动打包")] public bool autoBuild = false;
        public bool autoGenerateAssetBundle;
        public string buildPath_Android = "";
        public string buildPath_iOS = "";
        
        
        
        [HideInInspector]
        public bool isCloudBuild = false;
    }
}