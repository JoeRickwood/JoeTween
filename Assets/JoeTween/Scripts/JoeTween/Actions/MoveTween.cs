using System;
using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class MoveTween : TweenAction<Transform>
    {
        public TweenSpace space;
        public Vector3 startPos;
        public Vector3 endPos;

        public MoveTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, Vector3 _startPos, Vector3 _endPos
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            startPos = _startPos;
            endPos = _endPos;
        }

        public override void StartAction()
        {
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
