/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   GenericVectorTween.cs
    Description :   
    Author      :   Joe Rickwood
**************************************************************************/

using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class GenericVectorTween : ReflectionTween<Vector3>
    {
        public GenericVectorTween(string _componentName, string _memberName, AnimationCurve _animationCurve, float _animationLength, Transform _component, bool _looping, Vector3 _start, Vector3 _end)
            : base(_componentName, _memberName, _animationCurve, _animationLength, _component, _looping, _start, _end)
        {

        }

        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing && propertyInfo != null)
            {
                propertyInfo.SetValue(memberHolder, Vector3.Lerp(start.GetValue(tweenTime), end.GetValue(tweenTime), tweenTime));
            }
        }
    }
}