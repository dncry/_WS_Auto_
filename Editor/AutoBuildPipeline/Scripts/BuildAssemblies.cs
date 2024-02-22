/*----------------------------------------------------------------
** Creator：dncry
** Time：2021年12月29日 星期三 14:24
----------------------------------------------------------------*/

using UnityEditor;
using UnityEditor.Build;
using UnityEngine;

namespace WS.Auto
{
    public class BuildAssemblies  : IFilterBuildAssemblies
    {
        public int callbackOrder { get; }
        public string[] OnFilterAssemblies(BuildOptions buildOptions, string[] assemblies)
        {
            Debug.Log(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
            
            foreach (var str in assemblies)
            {
                Debug.Log(str);
            }

            Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<");
            
            return assemblies;
        }
    }
}
