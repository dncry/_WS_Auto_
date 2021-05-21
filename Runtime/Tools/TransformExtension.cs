/*----------------------------------------------------------------
** Creator：万硕
** Time：2021年05月21日 星期五 15:06
----------------------------------------------------------------*/

using UnityEngine;

namespace WS.Auto
{
    public static class TransformExtension
    {
        public static void ResetToWorld(this Transform source)
        {
            source.position = Vector3.zero;
            source.rotation = Quaternion.identity;
            source.localScale = Vector3.one;
        }

        public static void ResetToLocal(this Transform source)
        {
            source.localPosition = Vector3.zero;
            source.localRotation = Quaternion.identity;
            source.localScale = Vector3.one;
        }
    }
}