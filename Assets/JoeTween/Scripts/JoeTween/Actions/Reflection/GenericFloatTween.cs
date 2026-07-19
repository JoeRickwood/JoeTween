/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   GenericFloatTween.cs
    Description :   Can Find A Float Property On Any Component And Lerp It Between Two Float Values Over Time
    Author      :   Joe Rickwood
**************************************************************************/

using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class GenericFloatTween : ReflectionTween<float>
    {
        public GenericFloatTween(string _componentName, string _memberName, AnimationCurve _animationCurve, float _animationLength, Transform _component, bool _looping, float _start, float _end) 
            : base(_componentName, _memberName, _animationCurve, _animationLength, _component, _looping, _start, _end)
        {

        }

        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing && propertyInfo != null)
            {
                propertyInfo.SetValue(memberHolder, Mathf.Lerp(start, end, _time));
            }
        }
    }
}