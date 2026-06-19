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

            switch (space)
            {
                default: case TweenSpace.LOCAL: component.localEulerAngles = startRotation; break;
                case TweenSpace.WORLD: component.eulerAngles = startRotation; break;
            }

            base.StartAction();
        }

        protected override void EndAction()
        {
            switch (space)
            {
                default: case TweenSpace.LOCAL: component.localEulerAngles = endRotation; break;
                case TweenSpace.WORLD: component.eulerAngles = endRotation; break;
            }

            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing)
            {
                if (space == TweenSpace.LOCAL)
                    component.localRotation = Quaternion.SlerpUnclamped(Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), GetTweenProgress(_time));
                else if (space == TweenSpace.WORLD)
                    component.rotation = Quaternion.SlerpUnclamped(Quaternion.Euler(startRotation), Quaternion.Euler(endRotation), GetTweenProgress(_time));
            }
        }
    }
}
