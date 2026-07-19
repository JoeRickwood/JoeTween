using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    class TweenGenericColorNode : TweenReflectionNode<Color>
    {
        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            GenericColorTween tween = new GenericColorTween
            (
                componentName, memberName, animationCurve, tweenLength, 
                null, false, start, end
            );

            TweenSequenceNodeExtensions.LoadMod(ref tween.start, GetInputPortByName("Start"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.end, GetInputPortByName("End"));

            return tween;
        }
    }
}
