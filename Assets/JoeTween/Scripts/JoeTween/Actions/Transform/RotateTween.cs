using System;
using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class RotateTween : TweenAction<Transform>
    {
        public TweenValue<Vector3> startRotation;
        public TweenValue<Vector3> endRotation;
        public TweenSpace space;

        public RotateTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, Vector3 _startRotation, Vector3 _endRotation
        ) : base(_animationCurve, _animationLength, _component, _looping)
        {
            startRotation.value = _startRotation;
            endRotation.value = _endRotation;
        }

        public override void CloneModifiers()
        {
            if (startRotation.valueModifier != null)
                startRotation.valueModifier = startRotation.valueModifier.Clone<Vector3>();

            if (endRotation.valueModifier != null)
                endRotation.valueModifier = endRotation.valueModifier.Clone<Vector3>();

            base.CloneModifiers();
        }

        internal override void StartAction()
        {
            startRotation.OnActionStart(target);
            endRotation.OnActionStart(target);

            if(component != null)
            {
                switch (space)
                {
                    default: case TweenSpace.LOCAL: component.localEulerAngles = startRotation.GetValue(0f); break;
                    case TweenSpace.WORLD: component.eulerAngles = startRotation.GetValue(0f); break;
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
                    default: case TweenSpace.LOCAL: component.localEulerAngles = endRotation.GetValue(1f); break;
                    case TweenSpace.WORLD: component.eulerAngles = endRotation.GetValue(1f); break;
                }
            }

            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing & component != null)
            {
                if (space == TweenSpace.LOCAL)
                    component.localRotation = Quaternion.SlerpUnclamped(Quaternion.Euler(startRotation.GetValue(tweenTime)), Quaternion.Euler(endRotation.GetValue(tweenTime)), tweenTime);
                else if (space == TweenSpace.WORLD)
                    component.rotation = Quaternion.SlerpUnclamped(Quaternion.Euler(startRotation.GetValue(tweenTime)), Quaternion.Euler(endRotation.GetValue(tweenTime)), tweenTime);
            }
        }
    }
}
