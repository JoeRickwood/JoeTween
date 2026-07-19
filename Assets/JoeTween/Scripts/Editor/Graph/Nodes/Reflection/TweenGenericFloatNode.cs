using System;

namespace JoeTween
{
    [Serializable]
    class TweenGenericFloatNode : TweenReflectionNode<float>
    {
        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            GenericFloatTween tween = new GenericFloatTween
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
