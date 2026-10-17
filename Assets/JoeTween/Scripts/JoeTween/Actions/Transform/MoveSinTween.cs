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
    public class MoveSinTween : TweenAction<Transform>
    {
        public TweenSpace space;
        public TweenValue<Vector3> startPos;
        public TweenValue<Vector3> axis;
        public TweenValue<float> amplitude;
        public TweenValue<float> speed;

        public MoveSinTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, Vector3 _startPos, Vector3 _axis, float _amplitude, 
            float _speed
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            startPos.value = _startPos;
            axis.value = _axis;

            amplitude.value = _amplitude;
            speed.value = _speed;
        }

        public override void CloneModifiers()
        {
            base.CloneModifiers();

            if (startPos.valueModifier != null)
                startPos.valueModifier = startPos.valueModifier.Clone<Vector3>();

            if (axis.valueModifier != null)
                axis.valueModifier = axis.valueModifier.Clone<Vector3>();
        }

        internal override void StartAction()
        {
            startPos.OnActionStart(target);
            axis.OnActionStart(target);
            amplitude.OnActionStart(target);
            speed.OnActionStart(target);

            if(component != null)
            {
                switch (space)
                {
                    default: case TweenSpace.LOCAL: component.localPosition = startPos.GetValue(0f); break;
                    case TweenSpace.WORLD: component.position = startPos.GetValue(0f); break;
                }
            }

            base.StartAction();
        }

        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing && component != null)
            {
                Vector3 position = axis.GetValue(tweenTime) * Mathf.Sin(_time * speed.GetValue(tweenTime)) * amplitude.GetValue(tweenTime);

                if (space == TweenSpace.LOCAL)
                    component.localPosition = startPos.GetValue(tweenTime) + position;
                else if (space == TweenSpace.WORLD)
                    component.position = startPos.GetValue(tweenTime) + position;
            }
        }
    }
}
