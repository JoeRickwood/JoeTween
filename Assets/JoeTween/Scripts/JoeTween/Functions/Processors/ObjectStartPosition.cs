using System;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Cached Object Position At The Start Of The Action
    /// Then Sets The Altered Value To The Cached Position
    /// </summary>
    [Serializable]
    public class ObjectStartPositionTweenModifier : TweenActionValueModifier<Vector3>
    {
        private Vector3 cachedPos;
        public TweenSpace space; //Space in which to operate the position cache

        public ObjectStartPositionTweenModifier(TweenSpace _space)
        {
            space = _space;
        }

        //Caches Position On Action Start
        public override void OnActionStart(GameObject _componentHolder)
        {
            if (_componentHolder == null)
                return;

            //Cache Position
            if (space == TweenSpace.LOCAL)
                cachedPos = _componentHolder.transform.localPosition;
            else if(space == TweenSpace.WORLD)
                cachedPos = _componentHolder.transform.position;
        }

        //Returns cached position
        public override void AlterValue(ref Vector3 _value, float _tweenTime)
        {
            _value = cachedPos;
        }
    }
}
