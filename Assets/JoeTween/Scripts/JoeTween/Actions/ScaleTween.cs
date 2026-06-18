using System;
using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class ScaleTween : TweenAction<Transform>
    {
        public TweenValue<Vector3> startScale;
        public TweenValue<Vector3> endScale;

        public ScaleTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, Vector3 _startScale, Vector3 _endScale
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            startScale.value = _startScale;
            endScale.value = _endScale;
        }

        public override void StartAction()
        {
            startScale.OnActionStart(target);
            endScale.OnActionStart(target);

            //component.localScale = startScale;

            base.StartAction();
        }

        public override void CloneModifiers()
        {
            if (startScale.valueModifier != null)
                startScale.valueModifier = startScale.valueModifier.Clone<Vector3>();

            if (endScale.valueModifier != null)
                endScale.valueModifier = endScale.valueModifier.Clone<Vector3>();

            base.CloneModifiers();
        }

        public override void EndAction()
        {
            //component.localScale = endScale;

            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing)
            {
                component.localScale = Vector3.LerpUnclamped(startScale, endScale, GetTweenProgress(_time));
            }
        }
    }
}
