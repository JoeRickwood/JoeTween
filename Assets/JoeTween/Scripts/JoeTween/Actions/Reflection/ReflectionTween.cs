/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   ReflectionTween.cs
    Description :   Core foundation-class for all reflection-based tweens
    Author      :   Joe Rickwood
**************************************************************************/

using JoeTween;
using System;
using System.Reflection;
using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public abstract class ReflectionTween<T> : TweenAction<Transform>
    {
        public string componentName;
        public string propertyName;

        protected object memberHolder;
        protected PropertyInfo propertyInfo;

        public TweenValue<T> start;
        public TweenValue<T> end;

        public ReflectionTween(string _componentName, string _memberName, AnimationCurve _animationCurve, float _animationLength, Transform _component, bool _looping, T _start, T _end)
            : base(_animationCurve, _animationLength, _component, _looping)
        {
            componentName = _componentName;
            propertyName = _memberName;

            start.value = _start;
            end.value = _end;
        }

        public override void UpdateTargetRecursive(GameObject _target)
        {
            base.UpdateTargetRecursive(_target);

            memberHolder = _target.GetComponent(componentName);

            if (memberHolder == null)
                return;

            Type type = memberHolder.GetType();
            propertyInfo = type.GetProperty(
                propertyName,
                BindingFlags.Instance |
                BindingFlags.Public |
                BindingFlags.NonPublic
            );
        }
    }
}