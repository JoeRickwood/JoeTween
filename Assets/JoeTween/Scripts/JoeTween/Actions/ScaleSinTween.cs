/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   MoveSinTween.cs
    Description :   Tweens That Move An Object With A Sin Wave
    Author      :   Joe Rickwood
**************************************************************************/


using System;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Moved A Target Transform Component Over Two Position Vectors
    /// </summary>
    [System.Serializable]
    public class ScaleSinTween : TweenAction<Transform>
    {
        public TweenValue<Vector3> startScale;
        public TweenValue<Vector3> axis;
        public TweenValue<float> amplitude;
        public TweenValue<float> speed;

        public ScaleSinTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, Vector3 _startScale, Vector3 _axis, float _amplitude, 
            float _speed
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            startScale.value = _startScale;
            axis.value = _axis;

            amplitude.value = _amplitude;
            speed.value = _speed;
        }

        public override void CloneModifiers()
        {
            base.CloneModifiers();

            if (startScale.valueModifier != null)
                startScale.valueModifier = startScale.valueModifier.Clone<Vector3>();

            if (axis.valueModifier != null)
                axis.valueModifier = axis.valueModifier.Clone<Vector3>();
        }

        internal override void StartAction()
        {
            startScale.OnActionStart(target);
            axis.OnActionStart(target);
            amplitude.OnActionStart(target);
            speed.OnActionStart(target);

            base.StartAction();
        }

        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing && component != null)
            {
                Vector3 scale = startScale.GetValue(tweenTime) + axis.GetValue(tweenTime) * Mathf.Sin(_time * speed.GetValue(tweenTime)) * amplitude.GetValue(tweenTime);

                component.localScale = scale;
            }
        }
    }
}
