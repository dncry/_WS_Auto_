/*----------------------------------------------------------------
** 项目名称: _Tools
** 文件名称：StringExtension
** 创 建 者：dncry
** 创建时间：2021年03月30日 星期二 11:13
** 文件版本：V1.0.0
* ===============================================================
** 功能描述：
**		
**
**----------------------------------------------------------------*/


using System;
using System.Text;
using UnityEngine;

namespace WS.Auto
{
    public static class StringExtension
    {
        public static Color ToColor(this string color)
        {
            ColorUtility.TryParseHtmlString(color, out Color nowColor);
            return nowColor;
        }
        
        public static string ToChildPath(this string name, Transform parent)
        {
            if (IsNullParent(parent)||IsSameNameWithParent(name, parent)) return null;
            var path =new StringBuilder(name,20) ;
            var target = GetSameNameChildTransform(name,parent);
            if (IsNullChild(target)) return null;
            if (IsFirstLayerChild(parent,target)) return path.ToString();
            return AddAllParentNameToPath(path, parent, target);
        }

        private static bool IsNullParent(Transform parent )
        {
            if (parent.IsNull())
            {
                Debug.LogWarning("错误:不能传入空");
                return true;
            }
            return false;
        }

        private static bool IsSameNameWithParent(string name, Transform parent)
        {
            if (name == parent.name)
            {
                Debug.LogWarning("错误:名字与parent重复");
                return true;
            }
            return false;
        }

        private static Transform GetSameNameChildTransform(string name, Transform parent)
        {
            var childGroup = parent.GetComponentsInChildren<Transform>(true);
            foreach (var child in childGroup)
            {
                if (child.name == name)
                {
                    return child;
                }
            }

            return null;
        }
     
        private static bool IsNullChild(Transform child )
        {
            if (!child.IsNull()) return false;
            Debug.LogWarning("错误:不能传入空");
            return true;
        }
       
        private static bool IsFirstLayerChild(Transform parent,Transform child)
        {
            return child.parent==parent;
        }
       
        private static string AddAllParentNameToPath(StringBuilder path,Transform parent,Transform child)
        {
            AddSingleParentNameToPath(path,child);
            while (!IsFirstLayerChild(parent,child))
            {
                child = child.parent;
                if (!IsFirstLayerChild(parent,child))
                    AddSingleParentNameToPath(path,child);
            }
            return path.ToString();
        }
        
        private static void AddSingleParentNameToPath(StringBuilder path, Transform child)
        {
            path.Insert(0, "/").Insert(0,child.parent.name);
        }
        
        public static bool IsNull<T>( this T source )
        {
            return source==null;
        }
        
        public static Transform ToChildTransform(this string name, Transform parent)
        {
            if (IsNullParent(parent)||IsSameNameWithParent(name, parent)) return null;
            return GetChildThroughName(parent,name);
        }
        
        private static Transform GetChildThroughName(Transform parent, String name)
        {
            var childGroup = parent.GetComponentsInChildren<Transform>(true);
            foreach (var child in childGroup) 
            {
                if (child.name == name)
                {
                    return child;
                }
            }
            Debug.LogWarning("错误:找不到子物体");
            return null;
        }
    }
}