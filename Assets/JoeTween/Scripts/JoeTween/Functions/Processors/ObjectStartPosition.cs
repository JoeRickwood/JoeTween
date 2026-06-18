using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ObjectStartPositionTweenModifier : TweenActionValueModifier<Vector3>
    {
        private Vector3 cachedPos;
        public TweenSpace space;

        public ObjectStartPositionTweenModifier(TweenSpace _space)
        {
            space = _space;
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            if(space == TweenSpace.LOCAL)
                cachedPos = _componentHolder.transform.localPosition;
            else if(space == TweenSpace.WORLD)
                cachedPos = _componentHolder.transform.position;
        }

        public override void AlterValue(out Vector3 _value)
        {
            _value = cachedPos;
        }
    }
}
