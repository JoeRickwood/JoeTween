using System;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Makes A Value Lower Over The Course Of he Span Of The Animation
    /// </summary>
    [Serializable]
    public class DampenValueModifier : TweenActionValueModifier<float>
    {
        public float baseValue;
        [SerializeReference] private AnimationCurve dampenCurve;

        public DampenValueModifier(float _baseValue, AnimationCurve _dampeningCurve)
        {
            baseValue = _baseValue;
            dampenCurve = _dampeningCurve;
        }

        //Returns cached position
        public override void AlterValue(ref float _value, float _tweenTime)
        {
            _value = baseValue * dampenCurve.Evaluate(_tweenTime);
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
           
        }
    }
}
