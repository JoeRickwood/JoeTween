using System;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Caches the object scale at the start of the action
    /// Then returns the cached position per frame of the anim
    /// </summary>
    [Serializable]
    public class ObjectStartScaleTweenModifier : TweenActionValueModifier<Vector3>
    {
        private Vector3 cachedScale;

        public ObjectStartScaleTweenModifier()
        {
            
        }

        //Caches the scale on start 
        public override void OnActionStart(GameObject _componentHolder)
        {
            if (_componentHolder == null)
                return;

            cachedScale = _componentHolder.transform.localScale;
        }

        //Returns cached value
        public override void AlterValue(ref Vector3 _value, float _tweenTime)
        {
            _value = cachedScale;
        }
    }
}
