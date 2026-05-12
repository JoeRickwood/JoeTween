using UnityEngine;

namespace JoeTween
{
    internal class MoveTween : TweenAction<Transform>
    {
        public TweenSpace space;
        public Vector3 moveTo;

        private Vector3 startPos;
        private Vector3 endPos;


        public override void StartAction()
        {
            switch (space)
            {
                default: case TweenSpace.LOCAL: startPos = component.localPosition; break;
                case TweenSpace.WORLD: startPos = component.position; break;
            }

            endPos = moveTo;

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
            if(space == TweenSpace.LOCAL)
                component.localPosition = Vector3.LerpUnclamped(startPos, endPos, _time);
            else if (space == TweenSpace.WORLD)
                component.position = Vector3.LerpUnclamped(startPos, endPos, _time);
        }
    }
}
