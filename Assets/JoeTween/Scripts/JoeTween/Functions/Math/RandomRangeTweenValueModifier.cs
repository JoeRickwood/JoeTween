using System;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Float Modifier to take the random between two range values and sets the result to the out value
    /// </summary>
    [Serializable]
    public class RandomRangeTweenModifier : TweenActionValueModifier<float>
    {
        public TweenValue<float> min; //minimum range value
        public TweenValue<float> max; //maximum range value

        private float random; //cached random

        public RandomRangeTweenModifier(TweenValue<float> _min, TweenValue<float> _max)
        {
            min = _min;
            max = _max;
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            min.valueModifier?.OnActionStart(_componentHolder);
            max.valueModifier?.OnActionStart(_componentHolder);

            //caches random for use throughout the action
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
