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

        public override void StartAction()
        {
            base.StartAction();
        }

        public override void EndAction()
        {
            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing)
            {
                float value = Mathf.Sin(_time * frequency) * amplitude;

                if(space == TweenSpace.LOCAL)
                    component.localEulerAngles = axis.GetValue() * value;
                else if(space == TweenSpace.WORLD)
                    component.eulerAngles = axis.GetValue() * value;
            }
        }
    }
}
