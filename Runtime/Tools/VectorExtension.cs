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


        public static Vector3 World2Screen(this Vector3 worldPos, Camera worldCamera)
        {
            var screenPos = worldCamera.WorldToScreenPoint(worldPos);
            return screenPos;
        }

        public static Vector3 Screen2World(this Vector3 screenPos, Camera worldCamera)
        {
            var worldPos = worldCamera.ScreenToWorldPoint(screenPos);
            return worldPos;
        }

        public static Vector3 World2World(this Vector3 worldPos, Camera worldCamera1, Camera worldCamera2)
        {
            var screenPos = worldCamera1.WorldToScreenPoint(worldPos);
            var worldPos2 = worldCamera2.ScreenToWorldPoint(screenPos);
            return worldPos2;
        }
    }
}