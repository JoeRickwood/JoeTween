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
    }
}
