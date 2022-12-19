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
            {"androidKeystorePass", "androidKeyaliasName", "androidKeyaliasPass"};

        public static void AutoBuild()
        {
#if UNITY_ANDROID
            BuildAndroid();
#elif UNITY_IOS
            BuildIOS();
#endif
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

            //var command = System.Environment.GetCommandLineArgs();

            
            if (options.TryGetValue("customBuildPath", out string customBuildPath))
            {
                BuildSettings.Instance.android.buildAAB =  options["customBuildPath"].EndsWith(".aab");
            }
            
            foreach (var dic in options)
            {
                Debug.Log($"###################{dic.Key}");
            }
            
            BuildPipeline.Build_Common();
            BuildPipeline.Build_Android(customBuildPath);

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
                bool secret = ((IList) Secrets).Contains(flag);
                string displayValue = secret ? "*HIDDEN*" : "\"" + value + "\"";

                // Assign
                Console.WriteLine($"Found flag \"{flag}\" with value {displayValue}.");
                providedArguments.Add(flag, value);
            }
        }
    }
}