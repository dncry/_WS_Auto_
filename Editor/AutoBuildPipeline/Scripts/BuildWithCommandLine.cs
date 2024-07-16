using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace WS.Auto
{
    public class BuildWithCommandLine
    {
        private static readonly string Eol = Environment.NewLine;

        private static readonly string[] Secrets =
            { "androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass" };

        public static void AutoBuild()
        {
            Dictionary<string, string> options = new Dictionary<string, string>();
            ParseCommandLineArguments(out options);

            foreach (var dic in options)
            {
                Debug.Log($"###################{dic.Key}##########{dic.Value}#############");
            }


            if (options.TryGetValue("buildTarget", out string buildTarget))
            {
                if (string.Equals(buildTarget, "Android", StringComparison.OrdinalIgnoreCase))
                {
                    BuildAndroid();
                }

                if (string.Equals(buildTarget, "iOS", StringComparison.OrdinalIgnoreCase))
                {
                    BuildIOS();
                }
            }
        }

        //private static int parameterCount = 2;

        public static void BuildAndroid()
        {
            Debug.Log("#################BuildAndroid#################");

            BuildSettings.Instance.autoBuild = true;
            BuildSettings.Instance.isCloudBuild = true;
            BuildSettings.Instance.autoGenerateAssetBundle = true;

            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;


            Dictionary<string, string> options = new Dictionary<string, string>();
            ParseCommandLineArguments(out options);

            // if (options.TryGetValue("customBuildPath", out string customBuildPath))
            // {
            //     BuildSettings.Instance.android.buildAAB =  customBuildPath.EndsWith(".aab");
            // }

            if (options.TryGetValue("buildVersion", out string buildVersion))
            {
                BuildSettings.Instance.version = buildVersion;
            }

            if (options.TryGetValue("buildBundleCode", out string buildBundleCode))
            {
                BuildSettings.Instance.android.bundleVersionCode = int.Parse(buildBundleCode);
            }

            if (options.TryGetValue("buildPath", out string buildPath))
            {
                BuildSettings.Instance.buildPath_Android = buildPath;
            }

            if (options.TryGetValue("buildSeparateAsset", out string buildSeparateAsset))
            {
                bool.TryParse(buildSeparateAsset, out bool value);
                BuildSettings.Instance.android.separateAsset = value;
            }

            if (options.TryGetValue("buildAndroidType", out string buildAndroidType))
            {
                if (buildAndroidType == "apk")
                {
                    BuildSettings.Instance.android.buildAndroidType = BuildSettings.BuildAndroidType.Apk;
                }
                if (buildAndroidType == "aab")
                {
                    BuildSettings.Instance.android.buildAndroidType = BuildSettings.BuildAndroidType.Aab;
                }
                if (buildAndroidType == "androidStudio")
                {
                    BuildSettings.Instance.android.buildAndroidType = BuildSettings.BuildAndroidType.AndroidStudio;
                }
            }


            if (options.TryGetValue("sdkPath", out string sdkPath))
            {
                if (sdkPath != "")
                {
                    Debug.Log("********** Set sdkPath ***********");

                    EditorPrefs.SetBool("SdkUseEmbedded", false);
                    EditorPrefs.SetString("AndroidSdkRoot", sdkPath);
                }
            }

            if (options.TryGetValue("ndkPath", out string ndkPath))
            {
                if (ndkPath != "")
                {
                    Debug.Log("********** Set ndkPath ***********");

                    EditorPrefs.SetBool("NdkUseEmbedded", false);
                    EditorPrefs.SetString("AndroidNdkRoot", ndkPath);
                }
            }

            if (options.TryGetValue("jdkPath", out string jdkPath))
            {
                if (jdkPath != "")
                {
                    Debug.Log("********** Set jdkPath ***********");

                    EditorPrefs.SetBool("JdkUseEmbedded", false);
                    EditorPrefs.SetString("JdkPath", jdkPath);
                }
            }

            if (options.TryGetValue("gradlePath", out string gradlePath))
            {
                if (gradlePath != "")
                {
                    Debug.Log("********** Set gradlePath ***********");

                    EditorPrefs.SetBool("GradleUseEmbedded", false);
                    EditorPrefs.SetString("GradlePath", gradlePath);
                }
            }


            foreach (var dic in options)
            {
                Debug.Log($"###################{dic.Key}##########{dic.Value}#############");
            }

            BuildPipeline.Build_Common();
            BuildPipeline.Build_Android();

            Debug.Log("#################Complete#################");

            BuildSettings.Instance.autoBuild = false;
            BuildSettings.Instance.isCloudBuild = false;
            BuildSettings.Instance.autoGenerateAssetBundle = false;
        }


        public static void BuildIOS()
        {
            Debug.Log("#################BuildIOS#################");

            BuildSettings.Instance.autoBuild = true;
            BuildSettings.Instance.isCloudBuild = true;
            BuildSettings.Instance.autoGenerateAssetBundle = true;

            // 0. keystore   1.输出路径
            foreach (string VARIABLE in System.Environment.GetCommandLineArgs())
            {
                Debug.Log($"#################{VARIABLE}#################");
            }

            var command = System.Environment.GetCommandLineArgs();

            // if (command.Length != parameterCount)
            // {
            //     Debug.Log($"#################{command.Length}#################");
            //     return;
            // }

            BuildPipeline.Build_Common();
            //BuildPipeline.Build_iOS(command[9]);
            BuildPipeline.Build_iOS();


            Debug.Log("#################Complete#################");

            BuildSettings.Instance.autoBuild = false;
            BuildSettings.Instance.isCloudBuild = false;
            BuildSettings.Instance.autoGenerateAssetBundle = false;
        }


        private static void ParseCommandLineArguments(out Dictionary<string, string> providedArguments)
        {
            providedArguments = new Dictionary<string, string>();
            string[] args = Environment.GetCommandLineArgs();

            Console.WriteLine(
                $"{Eol}" +
                $"###########################{Eol}" +
                $"#    Parsing settings     #{Eol}" +
                $"###########################{Eol}" +
                $"{Eol}"
            );

            // Extract flags with optional values
            for (int current = 0, next = 1; current < args.Length; current++, next++)
            {
                // Parse flag
                bool isFlag = args[current].StartsWith("-");
                if (!isFlag) continue;
                string flag = args[current].TrimStart('-');

                // Parse optional value
                bool flagHasValue = next < args.Length && !args[next].StartsWith("-");
                string value = flagHasValue ? args[next].TrimStart('-') : "";
                bool secret = ((IList)Secrets).Contains(flag);
                string displayValue = secret ? "*HIDDEN*" : "\"" + value + "\"";

                // Assign
                Console.WriteLine($"Found flag \"{flag}\" with value {displayValue}.");
                providedArguments.Add(flag, value);
            }
        }
    }
}