using UnityEngine;

namespace JoeTween
{
    internal abstract class TweenAction<T>
    {
        public float animationLength = 1.0f;
        private float t = 0.0f;

        public T component;

        public abstract void Update(float _time);

        public virtual void StartAction(T _component)
        {
            component = _component;
        }
        public virtual void EndAction(T _transform)
        {
            t = animationLength;
        }
    }
}