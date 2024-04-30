
namespace WS.Auto
{
    using UnityEngine;


    public static class VectorExtension
    {
        public static Vector3 RotateWithAxis(this Vector3 source, Vector3 axis, float angle)
        {
            Quaternion q = Quaternion.AngleAxis(angle, axis); // 旋转系数
            return q * source; // 返回目标点
        }

        /// <summary>
        /// 世界坐标转UI绝对坐标
        /// </summary>
        /// <param name="worldPos"></param>
        /// <param name="worldCamera"></param>
        /// <param name="uiCanvas">Canvas画布</param>
        /// <param name="uiCamera"></param>
        /// <returns></returns>
        public static Vector3 World2UI(this Vector3 worldPos, Camera worldCamera, RectTransform uiCanvas,
            Camera uiCamera)
        {
            var screenPos = worldCamera.WorldToScreenPoint(worldPos);
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(uiCanvas, screenPos, uiCamera,
                out var uiPos))
            {
            }

            return uiPos;
        }

        /// <summary>
        /// 屏幕坐标转UI相对坐标
        /// </summary>
        /// <returns></returns>
        public static Vector2 Screen2UI(this Vector2 screenPos, RectTransform uiCanvas, Camera uiCamera)
        {
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                uiCanvas, screenPos, uiCamera,
                out var uiPosition))
            {
            }

            return uiPosition;
        }
    }
}
