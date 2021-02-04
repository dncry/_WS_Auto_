using UnityEditor;
using UnityEngine;

namespace WS.Auto
{
    public class BuildWithCommandLine
    {
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
            BuildPipeline.Build_Android(command[8], command[9]);

            Debug.Log("#################Complete#################");

            BuildSettings.Instance.autoBuild = false;
        }


        public static void BuildIOS()
        {
            Debug.Log("#################BuildIOS#################");

            BuildSettings.Instance.autoBuild = true;

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
            BuildPipeline.Build_iOS(command[9]);


            Debug.Log("#################Complete#################");

            BuildSettings.Instance.autoBuild = false;
        }
    }
}