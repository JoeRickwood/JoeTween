using System;
using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class RotateSinTween : TweenAction<Transform>
    {
        public TweenValue<Vector3> axis;
        public TweenValue<float> frequency;
        public TweenValue<float> amplitude;
        public TweenSpace space;

        public RotateSinTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, Vector3 _rotationAxis, float _frequency, float _ampltidude
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            axis.value = _rotationAxis;
            frequency.value = _frequency;
            amplitude.value = _ampltidude;
        }

        internal override void StartAction()
        {
            axis.OnActionStart(target);
            frequency.OnActionStart(target);
            amplitude.OnActionStart(target);

            base.StartAction();
        }

        public override void CloneModifiers()
        {
            if (axis.valueModifier != null)
                axis.valueModifier = axis.valueModifier.Clone<Vector3>();

            if (frequency.valueModifier != null)
                frequency.valueModifier = frequency.valueModifier.Clone<float>();

            if (amplitude.valueModifier != null)
                amplitude.valueModifier = amplitude.valueModifier.Clone<float>();

            base.CloneModifiers();
        }

        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing)
            {
                float value = Mathf.Sin(_time * frequency) * amplitude;

                if(space == TweenSpace.LOCAL)
                    component.localRotation = Quaternion.Euler(axis.GetValue() * value);
                else if(space == TweenSpace.WORLD)
                    component.rotation = Quaternion.Euler(axis.GetValue() * value);
            }
        }
    }
}
