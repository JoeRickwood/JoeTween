/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   EasingFunctions.cs
    Description :   Useful Creation Of Basic Easing Functions For Users To Use With JoeTween
    Author      :   Joe Rickwood
**************************************************************************/

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

        public static float EaseOutPow(float _t, float _pow = 3.0f)
        {
            return 1.0f - Mathf.Pow(1.0f - _t, _pow);
        }

        public static float EaseInPow(float _t, float _pow = 3.0f)
        {
            return Mathf.Pow(_t, _pow);
        }
    }
}
