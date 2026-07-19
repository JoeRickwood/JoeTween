using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    class TweenGenericVectorNode : TweenReflectionNode<Vector3>
    {
        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            GenericVectorTween tween = new GenericVectorTween
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
