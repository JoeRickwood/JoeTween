using UnityEngine;

namespace JoeTween
{
    public static class EasingFunctions
    {
        public static AnimationCurve EaseInOut()
        {
            return AnimationCurve.EaseInOut(0, 0, 1, 1);
        }

        public static AnimationCurve EaseOut()
        {
            AnimationCurve curve = new AnimationCurve
            (
                new Keyframe(0, 0, 0, 0.5f),
                new Keyframe(1, 1, 0, 0)
            );

            return curve;
        }
    }
}
