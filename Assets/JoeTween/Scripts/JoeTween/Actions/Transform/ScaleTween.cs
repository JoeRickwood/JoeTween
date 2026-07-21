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

        internal override void StartAction()
        {
            startScale.OnActionStart(target);
            endScale.OnActionStart(target);

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

        protected override void EndAction()
        {
            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing && component != null)
            {
                component.localScale = Vector3.LerpUnclamped(startScale.GetValue(tweenTime), endScale.GetValue(tweenTime), tweenTime);
            }
        }
    }
}
