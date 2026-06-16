using System;
using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public class BlankTween : TweenAction<Transform>
    {
        public BlankTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component 
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            
        }
    }
}
