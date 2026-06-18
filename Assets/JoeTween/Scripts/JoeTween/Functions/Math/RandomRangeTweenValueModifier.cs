using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class RandomRangeTweenModifier : TweenActionValueModifier<float>
    {
        public TweenValue<float> min;
        public TweenValue<float> max;

        private float random;

        public RandomRangeTweenModifier(TweenValue<float> _min, TweenValue<float> _max)
        {
            min = _min;
            max = _max;
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            min.valueModifier?.OnActionStart(_componentHolder);
            max.valueModifier?.OnActionStart(_componentHolder);

            random = UnityEngine.Random.Range(min.GetValue(), max.GetValue());
        }

        public override TweenActionValueModifier<T> Clone<T>()
        {
            if (min.valueModifier != null)
                min.valueModifier = min.valueModifier.Clone<float>();

            if (max.valueModifier != null)
                max.valueModifier = max.valueModifier.Clone<float>();

            return base.Clone<T>();
        }

        public override void AlterValue(out float _value)
        {
            _value = random;
        }
    }
}
