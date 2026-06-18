using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class VectorAddTweenModifier : TweenActionValueModifier<Vector3>
    {
        public TweenValue<Vector3> a;
        public TweenValue<Vector3> b;

        public VectorAddTweenModifier(TweenValue<Vector3> _a, TweenValue<Vector3> _b)
        {
            a = _a;
            b = _b;
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            a.valueModifier?.OnActionStart(_componentHolder);
            b.valueModifier?.OnActionStart(_componentHolder);
        }

        public override TweenActionValueModifier<T> Clone<T>()
        {
            if(a.valueModifier != null)
                a.valueModifier = a.valueModifier.Clone<Vector3>();

            if (b.valueModifier != null)
                b.valueModifier = b.valueModifier.Clone<Vector3>();

            return base.Clone<T>();
        }

        public override void AlterValue(out Vector3 _value)
        {
            Vector3 valA = Vector3.zero, valB = Vector3.zero;

            valA = a.GetValue();
            valB = b.GetValue();

            _value = (valA + valB);
        }
    }
}
