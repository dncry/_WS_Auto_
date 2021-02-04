using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace WS.Auto
{
    public class BuildPipeline
{
    [MenuItem("自定义/构建/自动化Android")]
    public static void AutoBuild_Android()
    {
#if !UNITY_ANDROID
        throw new ArgumentException("当前环境不是Android", nameof(AutoBuild_Android));
#endif
        Build_Common();
        Build_Android();
    }

    [MenuItem("自定义/构建/自动化iOS")]
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

    public static void Build_Android(string keystorePath = "", string outPath = "")
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.Android, BuildSettings.Instance.android.packageName);
        PlayerSettings.Android.bundleVersionCode = BuildSettings.Instance.android.bundleVersionCode;


        if (keystorePath != "")
        {
            PlayerSettings.Android.keystoreName = keystorePath;
        }

        PlayerSettings.Android.keystorePass = BuildSettings.Instance.android.keystorePass;

        PlayerSettings.Android.keyaliasName = BuildSettings.Instance.android.keyaliasPass;
        PlayerSettings.Android.keyaliasPass = BuildSettings.Instance.android.keyaliasPass;

        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

        //optimize
        //PlayerSettings
        //PlayerSettings.GetPropertyString("companyName", BuildTargetGroup.Android);

        Build_Android_Expand();

        //打包
        if (BuildSettings.Instance.autoBuild) Build_Android_StartBuild(outPath);
    }

    public static void Build_iOS(string outPath = "")
    {
        PlayerSettings.SetApplicationIdentifier(BuildTargetGroup.iOS, BuildSettings.Instance.iOS.packageName);
        PlayerSettings.iOS.buildNumber = BuildSettings.Instance.iOS.build;

        PlayerSettings.defaultInterfaceOrientation = UIOrientation.Portrait;

        //optimize
        //加速度检测频率
        //PlayerSettings.accelerometerFrequency = 0;

        Build_iOS_Expand();


        //打包
        if (BuildSettings.Instance.autoBuild) Build_iOS_StartBuild(outPath);
    }


    #region 拓展

    private static void Build_Common_Expand()
    {
        //可寻址
        //AddressableAssetSettings.CleanPlayerContent();
        //AddressableAssetSettings.BuildPlayerContent();
    }

    private static void Build_Android_Expand()
    {
        //FacebookSDK自动生成Manifest
        //Facebook.Unity.Editor. ManifestMod.GenerateManifest();
        //LionStudios.Facebook.Editor.Android.ManifestMod.GenerateManifest();
    }

    private static void Build_iOS_Expand()
    {
    }

    #endregion


    #region Build

    private static void Build_Android_StartBuild(string outPath = "")
    {
        
        string nowTime = System.DateTime.Now.ToString("_yyyyMMdd_HH_mm");
        string filePath = outPath == ""
            ? BuildSettings.Instance.buildPath_Android
            : outPath
              + BuildSettings.Instance.productName + "_" +
              BuildSettings.Instance.version + "_" + nowTime;
        filePath += ".apk";

        var buildPlayerOptions = new BuildPlayerOptions
        {
            target = BuildTarget.Android,
            locationPathName = filePath,
            options = BuildOptions.None,
            scenes = BuildSettings.Instance.scenePaths
        };

        BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions);
    }


    private static void Build_iOS_StartBuild(string outPath = "")
    {
        string nowTime = System.DateTime.Now.ToString("_MMdd_HH_mm");
        string filePath = outPath == ""
            ? BuildSettings.Instance.buildPath_iOS : outPath + nowTime;

        var buildPlayerOptions = new BuildPlayerOptions
        {
            target = BuildTarget.iOS,
            locationPathName = filePath,
            options = BuildOptions.None
        };

        BuildReport report = UnityEditor.BuildPipeline.BuildPlayer(buildPlayerOptions);
    }

    #endregion
}
}