/*----------------------------------------------------------------
** 项目名称: _Tools
** 文件名称：StringExtension
** 创 建 者：万硕
** 创建时间：2021年03月30日 星期二 11:13
** 文件版本：V1.0.0
* ===============================================================
** 功能描述：
**		
**
**----------------------------------------------------------------*/


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
            var path = name;
            Transform target = null;

            if (name == parent.name)
            {
                Debug.Log("错误");
                return null;
            }

            foreach (var VARIABLE in parent.GetComponentsInChildren<Transform>(true))
            {
                if (VARIABLE.name == name)
                {
                    target = VARIABLE;
                }
            }

            if (target == null)
            {
                Debug.Log("错误:找不到子物体");
                return null;
            }

            if (target.parent == parent)
            {
                return path;
            }

            path = target.parent.name + "/" + path;
            while (target.parent != parent)
            {
                target = target.parent;

                if (target.parent != parent)
                {
                    path = target.parent.name + "/" + path;
                }
            }

            return path;
        }

        public static Transform ToChildTransform(this string name, Transform parent)
        {
            Transform target = null;

            if (name == parent.name)
            {
                Debug.Log("错误");
                return null;
            }

            foreach (var VARIABLE in parent.GetComponentsInChildren<Transform>(true))
            {
                if (VARIABLE.name == name)
                {
                    target = VARIABLE;
                    return target;
                }
            }

            Debug.Log("错误:找不到子物体");
            return null;
        }
    }
}