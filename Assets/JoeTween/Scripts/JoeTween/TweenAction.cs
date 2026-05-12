using Unity.VisualScripting.YamlDotNet.Core.Events;
using UnityEngine;

namespace JoeTween
{
    internal abstract class TweenAction
    {
        public AnimationCurve tweenCurve;
        public float animationLength = 1.0f;

        private TweenAction[] sequenced;

        protected bool ended;

        public abstract void Initialize(GameObject _component);

        public virtual void Update(float _time)
        {
            if (_time > animationLength)
            {
                if (ended == false)
                    EndAction();

                foreach (var t in sequenced)
                {
                    t.Update(_time - animationLength);
                }
            }
        }

        public virtual void StartAction()
        {

        }

        public virtual void EndAction()
        {
            ended = true;

            if (sequenced != null)
            {
                foreach (var t in sequenced)
                {
                    t.StartAction();
                }
            }
        }
    }

    internal abstract class TweenAction<T> : TweenAction
    {
        public T component;

        public override void Initialize(GameObject _component)
        {
            component = _component.GetComponent<T>();
        }
    }
}