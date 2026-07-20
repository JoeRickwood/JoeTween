/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   VectorMultiplierTweenModifier.cs
    Description :   Modifier For Vector3 Tweens That Multiplies A Vector By A Scalar
    Author      :   Joe Rickwood
**************************************************************************/

using System;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Adds Two Vectors Together And Sets The Alter Value To The Result
    /// </summary>
    [Serializable]
    public class VectorMultiplyTweenModifier : TweenActionValueModifier<Vector3>
    {
        public TweenValue<Vector3> a; //First Vector To Add
        public TweenValue<float> b; //Vector Multiplier

        public VectorMultiplyTweenModifier(TweenValue<Vector3> _a, TweenValue<float> _b)
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
                b.valueModifier = b.valueModifier.Clone<float>();

            return base.Clone<T>();
        }

        public override void AlterValue(ref Vector3 _value, float _tweenTime)
        {
            Vector3 valA = Vector3.zero;
            float valB = 0;

            valA = a.GetValue(_tweenTime);
            valB = b.GetValue(_tweenTime);

            _value = valA * valB;
        }
    }
}
