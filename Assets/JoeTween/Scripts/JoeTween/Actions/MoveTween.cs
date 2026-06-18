using System;
using UnityEngine;

namespace JoeTween
{
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

        public override void StartAction()
        {
            startPos.OnActionStart(target);
            endPos.OnActionStart(target);

            switch (space)
            {
                default: case TweenSpace.LOCAL: component.localPosition = startPos; break;
                case TweenSpace.WORLD: component.position = startPos; break;
            }

            base.StartAction();
        }

        public override void EndAction()
        {
            switch (space)
            {
                default: case TweenSpace.LOCAL: component.localPosition = endPos; break;
                case TweenSpace.WORLD: component.position = endPos; ; break;
            }

            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing)
            {
                if (space == TweenSpace.LOCAL)
                    component.localPosition = Vector3.LerpUnclamped(startPos, endPos, GetTweenProgress(_time));
                else if (space == TweenSpace.WORLD)
                    component.position = Vector3.LerpUnclamped(startPos, endPos, GetTweenProgress(_time));
            }
        }
    }
}
