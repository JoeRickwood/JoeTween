using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ObjectStartRotationTweenModifier : TweenActionValueModifier<Vector3>
    {
        private Vector3 cachedRot;
        public TweenSpace space;

        public ObjectStartRotationTweenModifier(TweenSpace _space)
        {
            space = _space;
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            if(space == TweenSpace.LOCAL)
                cachedRot = _componentHolder.transform.localEulerAngles;
            else if(space == TweenSpace.WORLD)
                cachedRot = _componentHolder.transform.eulerAngles;
        }

        public override void AlterValue(out Vector3 _value)
        {
            _value = cachedRot;
        }
    }
}
