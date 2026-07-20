using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class MakeVectorTweenModifier : TweenActionValueModifier<Vector3>
    {
        public TweenValue<float> x;
        public TweenValue<float> y;
        public TweenValue<float> z;

        public MakeVectorTweenModifier(TweenValue<float> _x, TweenValue<float> _y, TweenValue<float> _z)
        {
            x = _x;
            y = _y;
            z = _z;
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            x.valueModifier?.OnActionStart(_componentHolder);
            y.valueModifier?.OnActionStart(_componentHolder);
            z.valueModifier?.OnActionStart(_componentHolder);
        }

        public override TweenActionValueModifier<T> Clone<T>()
        {
            if(x.valueModifier != null)
                x.valueModifier = x.valueModifier.Clone<float>();

            if (y.valueModifier != null)
                y.valueModifier = y.valueModifier.Clone<float>();

            if (z.valueModifier != null)
                z.valueModifier = z.valueModifier.Clone<float>();

            return base.Clone<T>();
        }

        public override void AlterValue(ref Vector3 _value, float _tweenTime)
        {
            float valX, valY, valZ;

            valX = x.GetValue(_tweenTime);
            valY = y.GetValue(_tweenTime);
            valZ = z.GetValue(_tweenTime);

            _value = new Vector3(valX, valY, valZ);
        }
    }
}
