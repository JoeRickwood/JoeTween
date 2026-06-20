/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   MoveTween.cs
    Description :   Tweens That Move An Object From A Start Position To An End Position Over Time
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
    public class MoveTween : TweenAction<Transform>
    {
        public TweenSpace space;
        public TweenValue<Vector3> startPos;
        public TweenValue<Vector3> endPos;

        public MoveTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, Vector3 _startPos, Vector3 _endPos
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            startPos.value = _startPos;
            endPos.value = _endPos;
        }

        public override void CloneModifiers()
        {
            base.CloneModifiers();

            if (startPos.valueModifier != null)
                startPos.valueModifier = startPos.valueModifier.Clone<Vector3>();

            if (endPos.valueModifier != null)
                endPos.valueModifier = endPos.valueModifier.Clone<Vector3>();
        }

        internal override void StartAction()
        {
            startPos.OnActionStart(target);
            endPos.OnActionStart(target);

            if(component != null)
            {
                switch (space)
                {
                    default: case TweenSpace.LOCAL: component.localPosition = startPos; break;
                    case TweenSpace.WORLD: component.position = startPos; break;
                }
            }

            base.StartAction();
        }

        protected override void EndAction()
        {
            if (component != null)
            {
                switch (space)
                {
                    default: case TweenSpace.LOCAL: component.localPosition = endPos; break;
                    case TweenSpace.WORLD: component.position = endPos; ; break;
                }
            }

            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing && component != null)
            {
                if (space == TweenSpace.LOCAL)
                    component.localPosition = Vector3.LerpUnclamped(startPos, endPos, GetTweenProgress(_time));
                else if (space == TweenSpace.WORLD)
                    component.position = Vector3.LerpUnclamped(startPos, endPos, GetTweenProgress(_time));
            }
        }
    }
}
