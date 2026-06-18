using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ObjectStartScaleTweenModifier : TweenActionValueModifier<Vector3>
    {
        private Vector3 cachedScale;

        public ObjectStartScaleTweenModifier()
        {
            
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            cachedScale = _componentHolder.transform.localScale;
        }

        public override void AlterValue(out Vector3 _value)
        {
            _value = cachedScale;
            
        }
    }
}
