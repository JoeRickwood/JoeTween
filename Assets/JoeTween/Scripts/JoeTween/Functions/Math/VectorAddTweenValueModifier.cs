using System;
using UnityEngine;
using UnityEngine.Assertions.Must;

namespace JoeTween
{
    public enum VectorMathExpression
    {
        ADD,
        SUBTRACT,
        MULTIPLY,
        CROSS_PRODUCT
    }

    /// <summary>
    /// Adds Two Vectors Together And Sets The Alter Value To The Result
    /// </summary>
    [Serializable]
    public class VectorMathTweenModifier : TweenActionValueModifier<Vector3>
    {
        public VectorMathExpression mathExpression;
        public TweenValue<Vector3> a; //First Vector To Add
        public TweenValue<Vector3> b; //Second Vector To Add

        public VectorMathTweenModifier(VectorMathExpression _expression, TweenValue<Vector3> _a, TweenValue<Vector3> _b)
        {
            mathExpression = _expression;
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

        private Vector3 MultiplyVectors(Vector3 _a, Vector3 _b)
        {
            return new Vector3(_a.x * _b.x, _a.y * _b.y, _a.z * _b.z);
        }

        public override void AlterValue(ref Vector3 _value, float _tweenTime)
        {
            Vector3 valA = Vector3.zero, valB = Vector3.zero;

            valA = a.GetValue(_tweenTime);
            valB = b.GetValue(_tweenTime);


            switch (mathExpression)
            {
                case VectorMathExpression.ADD:
                    _value = (valA + valB);
                    break;
                case VectorMathExpression.SUBTRACT:
                    _value = (valA - valB);
                    break;
                case VectorMathExpression.MULTIPLY:
                    _value = MultiplyVectors(valA, valB);
                    break;
                case VectorMathExpression.CROSS_PRODUCT:
                    _value = Vector3.Cross(valA, valB);
                    break;
                default:
                    _value = (valA + valB);
                    break;
            }
        }
    }
}
