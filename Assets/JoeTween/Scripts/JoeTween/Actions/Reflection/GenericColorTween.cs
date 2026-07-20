/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   GenericColorTween.cs
    Description :   
    Author      :   Joe Rickwood
**************************************************************************/

using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class GenericColorTween : ReflectionTween<Color>
    {
        public GenericColorTween(string _componentName, string _memberName, AnimationCurve _animationCurve, float _animationLength, Transform _component, bool _looping, Color _start, Color _end)
            : base(_componentName, _memberName, _animationCurve, _animationLength, _component, _looping, _start, _end)
        {

        }

        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing && propertyInfo != null)
            {
                propertyInfo.SetValue(memberHolder, Color.Lerp(start.GetValue(tweenTime), end.GetValue(tweenTime), tweenTime));
            }
        }
    }
}