using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

#if AUTO_GOOGLE
using GooglePlayServices;
#endif

namespace WS.Auto
{
    public class BuildPipeline
    {
        private static readonly string Eol = Environment.NewLine;

        [MenuItem("自定义/构建/自动化Android", false, 102)]
        public static void AutoBuild_Android()
        {
#if !UNITY_ANDROID
            throw new ArgumentException("当前环境不是Android", nameof(AutoBuild_Android));
#endif
            Build_Common();
            Build_Android();
        }

        [MenuItem("自定义/构建/自动化iOS", false, 103)]
        public static void AutoBuild_iOS()
        {
#if !UNITY_IOS
            throw new ArgumentException("当前环境不是iOS", nameof(AutoBuild_iOS));
#endif
            Build_Common();
            Build_iOS();
        }

        public static void Build_Common()
        {
            PlayerSettings.companyName = BuildSettings.Instance.companyName;
            PlayerSettings.productName = BuildSettings.Instance.productName;
            PlayerSettings.bundleVersion = BuildSettings.Instance.version;

            //optimize
            PlayerSettings.gcIncremental = true;

            Build_Common_Expand();
        }

        public static void Build_Android()
        {
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android,
                BuildSettings.Instance.android.packageName);
            PlayerSettings.Android.bundleVersionCode = BuildSettings.Instance.android.bundleVersionCode;

            PlayerSettings.Android.useCustomKeystore = true;
            PlayerSettings.Android.keystoreName =
                $"{System.Environment.CurrentDirectory}/{BuildSettings.Instance.android.keystoreName}";
            PlayerSettings.Android.keystorePass = BuildSettings.Instance.android.keystorePass;
            PlayerSettings.Android.keyaliasName = BuildSettings.Instance.android.keyaliasName;
            PlayerSettings.Android.keyaliasPass = BuildSettings.Instance.android.keyaliasPass;

            EditorUserBuildSettings.buildAppBundle = BuildSettings.Instance.android.buildAAB;

            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

            Build_Android_Expand();

            Debug.Log($"##############" +
                      $"{System.Environment.CurrentDirectory}/{BuildSettings.Instance.android.keystoreName}");

            if (BuildSettings.Instance.autoGenerateAssetBundle)
            {
                GenerateAssetBundle_Android();
            }

            //打包
            if (BuildSettings.Instance.autoBuild)
            {
                Build_Android_StartBuild(BuildSettings.Instance.buildPath_Android);
            }
        }

        public static void Build_iOS()
        {
            PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, BuildSettings.Instance.iOS.packageName);
            PlayerSettings.iOS.buildNumber = BuildSettings.Instance.iOS.build;

            PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

            //optimize
            //加速度检测频率
            //PlayerSettings.accelerometerFrequency = 0;

            Build_iOS_Expand();

            if (BuildSettings.Instance.autoGenerateAssetBundle)
            {
            }

            //打包
            if (BuildSettings.Instance.autoBuild) Build_iOS_StartBuild(BuildSettings.Instance.buildPath_iOS);
        }


        #region 拓展

        private static void Build_Common_Expand()
        {
        }

        private static void Build_Android_Expand()
        {
#if AUTO_FACEBOOK
            //FacebookSDK自动生成Manifest
            Facebook.Unity.Editor.ManifestMod.GenerateManifest();

            // 获取当前的 AndroidManifest.xml 内容
            string manifestPath = "Assets/Plugins/Android/AndroidManifest.xml"; // 替换成你的 AndroidManifest.xml 的路径
            string manifestContent = System.IO.File.ReadAllText(manifestPath);

            // 使用正则表达式替换 authorities
            string oldAuthoritiesPattern = @"(com\.facebook\.app\.FacebookContentProvider)(\d+)";
            string newAuthorities = "${applicationIdPlaceholder}.FacebookContentProvider$2";
            manifestContent = Regex.Replace(manifestContent, oldAuthoritiesPattern, newAuthorities);

            // 将修改后的内容写回 AndroidManifest.xml
            System.IO.File.WriteAllText(manifestPath, manifestContent);
#endif

            Thread.Sleep(TimeSpan.FromSeconds(1));

#if AUTO_GOOGLE
            var assembly = Assembly.Load("Google.JarResolver");
            Type type = assembly.GetType($"GooglePlayServices.PlayServicesResolver");
            var method = type.GetMethod("ResolveSync", BindingFlags.NonPublic | BindingFlags.Static);
            object[] tempVersion = { false, true };
            method.Invoke(null, tempVersion);
            Debug.Log("################PlayServicesResolveSync");
#endif

            Thread.Sleep(TimeSpan.FromSeconds(1));
        }

        private static void Build_iOS_Expand()
        {
        }

        #endregion

        #region Generate AssetBundle

        private static void GenerateAssetBundle_Android()
        {
#if AUTO_UGF
            var assembly = Assembly.Load("Assembly-CSharp-Editor");

            Type type = assembly.GetType(
                $"{BuildSettings.Instance.nameSpace}.Editor.DataTableTools.DataTableGeneratorMenu");
            var method = type.GetMethod("GenerateDataTables", BindingFlags.NonPublic | BindingFlags.Static);
            method?.Invoke(null, null);

            Debug.Log("#############GenerateDataTables");


            Type type2 =
                assembly.GetType($"{BuildSettings.Instance.nameSpace}.Editor.ResourceTools.ResourceRuleEditor");

            object obj2 = type2.GetMethod("OpenWithReflection", BindingFlags.NonPublic | BindingFlags.Static)
                ?.Invoke(null, null);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            type2.GetMethod("RefreshResourceCollection").Invoke(obj2, null);

            type2.GetMethod("Save", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj2, null);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            type2.GetMethod("CloseWithReflection").Invoke(obj2, null);

            Debug.Log("#############RefreshResourceCollection");


            var assembly3 = Assembly.Load("UnityGameFramework.Editor");
            Type type3 = assembly3.GetType("UnityGameFramework.Editor.ResourceTools.ResourceBuilder");

            object obj3 =
                type3.GetMethod("OpenWithReflection", BindingFlags.NonPublic | BindingFlags.Static)?.Invoke(null, null);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            var control = type3.GetField("m_Controller",
                BindingFlags.NonPublic | BindingFlags.Instance);

            var tempControl = control.GetValue(obj3);

            control.FieldType.GetProperty("OutputDirectory")
                .SetValue(tempControl, $"{System.Environment.CurrentDirectory}/StreamingAssets");

            object[] tempVersion = { 1 };
            control.FieldType.GetProperty("InternalResourceVersion").SetValue(tempControl, 1);

            object[] temp0 = { 1 << 0, false };
            object[] temp1 = { 1 << 1, false };
            object[] temp2 = { 1 << 2, false };
            object[] temp3 = { 1 << 3, false };
            object[] temp4 = { 1 << 4, false };
            object[] temp5 = { 1 << 5, true };
            object[] temp6 = { 1 << 6, false };
            object[] temp7 = { 1 << 7, false };

            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp0);
            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp1);
            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp2);
            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp3);
            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp4);
            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp5);
            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp6);
            control.FieldType.GetMethod("SelectPlatform").Invoke(tempControl, temp7);


            control.SetValue(obj3, tempControl);

            type3.GetMethod("SaveConfiguration", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj3, null);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            // type3.GetField("m_OrderBuildResources", BindingFlags.NonPublic | BindingFlags.Instance)
            //     .SetValue(obj3, true);

            type3.GetMethod("BuildResources", BindingFlags.NonPublic | BindingFlags.Instance).Invoke(obj3, null);

            Thread.Sleep(TimeSpan.FromSeconds(1));

            type3.GetMethod("CloseWithReflection").Invoke(obj3, null);

            Debug.Log("#############BuildAssetBundle");

#endif
        }

        #endregion


        #region Build

        private static void Build_Android_StartBuild(string outPath)
        {
            var datatimenow = DateTime.Now.ToString("MM_dd_HH_mm");
            string filePath =
                $"{System.Environment.CurrentDirectory}/{outPath}/{BuildSettings.Instance.productName + "_" + BuildSettings.Instance.version + "_" + datatimenow}";


            if (BuildSettings.Instance.android.buildAAB)
            {
                filePath += ".aab";
            }
            else
            {
                filePath += ".apk";
            }

            if (BuildSettings.Instance.isCloudBuild)
            {
                //filePath = outPath;
            }

            Debug.Log($"################{filePath}");

            var buildPlayerOptions = new BuildPlayerOptions
            {
                target = BuildTarget.Android,
                locationPathName = filePath,
                options = BuildOptions.None,
                scenes = GetScene()
            };

            BuildSummary buildSummary = UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions).summary;
            ReportSummary(buildSummary);

            Debug.Log($"################Build Success");

            if (BuildSettings.Instance.isCloudBuild)
            {
                ExitWithResult(buildSummary.result);
            }
        }

        private static void ReportSummary(BuildSummary summary)
        {
            Console.WriteLine(
                $"{Eol}" +
                $"###########################{Eol}" +
                $"#      Build results      #{Eol}" +
                $"###########################{Eol}" +
                $"{Eol}" +
                $"Duration: {summary.totalTime.ToString()}{Eol}" +
                $"Warnings: {summary.totalWarnings.ToString()}{Eol}" +
                $"Errors: {summary.totalErrors.ToString()}{Eol}" +
                $"Size: {summary.totalSize.ToString()} bytes{Eol}" +
                $"{Eol}"
            );
        }

        private static void ExitWithResult(BuildResult result)
        {
            switch (result)
            {
                case BuildResult.Succeeded:
                    Console.WriteLine("Build succeeded!");
                    EditorApplication.Exit(0);
                    break;
                case BuildResult.Failed:
                    Console.WriteLine("Build failed!");
                    EditorApplication.Exit(101);
                    break;
                case BuildResult.Cancelled:
                    Console.WriteLine("Build cancelled!");
                    EditorApplication.Exit(102);
                    break;
                case BuildResult.Unknown:
                default:
                    Console.WriteLine("Build result is unknown!");
                    EditorApplication.Exit(103);
                    break;
            }
        }


        private static void Build_iOS_StartBuild(string outPath = "")
        {
            string filePath = outPath == ""
                ? BuildSettings.Instance.buildPath_iOS
                : outPath;

            var buildPlayerOptions = new BuildPlayerOptions
            {
                target = BuildTarget.iOS,
                locationPathName = filePath,
                options = BuildOptions.None,
                scenes = GetScene()
            };

            BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions);
        }


        private static string[] GetScene()
        {
            string[] tempScene = new string[BuildSettings.Instance.scenePaths.Length];

            for (int i = 0; i < tempScene.Length; i++)
            {
                tempScene[i] = $"{System.Environment.CurrentDirectory}/{BuildSettings.Instance.scenePaths[i]}";

                Debug.Log(tempScene[i]);
            }

            return tempScene;
        }

        #endregion
    }
}