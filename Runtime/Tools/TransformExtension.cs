/*----------------------------------------------------------------
** Creator：dncry
** Time：2021年05月21日 星期五 15:06
----------------------------------------------------------------*/

using UnityEngine;

#if AUTO_DOTWEEN
using DG.Tweening;
using DG.Tweening.Core;
#endif

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

#if AUTO_DOTWEEN
        public static Sequence DOJumpZ(
            this Transform target,
            Vector3 endValue,
            float jumpPower,
            int numJumps,
            float duration,
            bool snapping = false)
        {
            if (numJumps < 1)
                numJumps = 1;
            float startPosY = 0.0f;
            float offsetY = -1f;
            bool offsetYSet = false;
            Sequence s = DOTween.Sequence();
            Tween yTween = (Tween)DOTween
                .To((DOGetter<Vector3>)(() => target.position), (DOSetter<Vector3>)(x => target.position = x),
                    new Vector3(0.0f, 0.0f, jumpPower), duration / (float)(numJumps * 2))
                .SetOptions(AxisConstraint.Z, snapping).SetEase<Tweener>(Ease.OutQuad).SetRelative<Tweener>()
                .SetLoops<Tweener>(numJumps * 2, LoopType.Yoyo)
                .OnStart<Tweener>((TweenCallback)(() => startPosY = target.position.z));
            s.Append((Tween)DOTween
                    .To((DOGetter<Vector3>)(() => target.position), (DOSetter<Vector3>)(x => target.position = x),
                        new Vector3(endValue.x, 0.0f, 0.0f), duration).SetOptions(AxisConstraint.X, snapping)
                    .SetEase<Tweener>(Ease.Linear))
                .Join((Tween)DOTween
                    .To((DOGetter<Vector3>)(() => target.position), (DOSetter<Vector3>)(x => target.position = x),
                        new Vector3(0.0f, endValue.y, 0.0f), duration).SetOptions(AxisConstraint.Y, snapping)
                    .SetEase<Tweener>(Ease.Linear)).Join(yTween).SetTarget<Sequence>((object)target)
                .SetEase<Sequence>(DOTween.defaultEaseType);
            yTween.OnUpdate<Tween>((TweenCallback)(() =>
            {
                if (!offsetYSet)
                {
                    offsetYSet = true;
                    offsetY = s.isRelative ? endValue.z : endValue.z - startPosY;
                }

                Vector3 position = target.position;
                position.z += DOVirtual.EasedValue(0.0f, offsetY, yTween.ElapsedPercentage(), Ease.OutQuad);
                target.position = position;
            }));
            return s;
        }
#endif
        
        
    }
}