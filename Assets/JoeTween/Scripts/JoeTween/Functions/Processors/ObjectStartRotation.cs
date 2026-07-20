using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Cached Object Rotation At The Start Of The Action
    /// Then Sets The Altered Value To The Cached Rotation
    /// </summary>
    [System.Serializable]
    public class ObjectStartRotationTweenModifier : TweenActionValueModifier<Vector3>
    {
        private Vector3 cachedRot;
        public TweenSpace space; //Space in which to operate the rotation cache

        public ObjectStartRotationTweenModifier(TweenSpace _space)
        {
            space = _space;
        }

        //Caches Rotation On Action Start
        public override void OnActionStart(GameObject _componentHolder)
        {
            if (_componentHolder == null)
                return;

            //Cache rotation
            if (space == TweenSpace.LOCAL)
                cachedRot = _componentHolder.transform.localEulerAngles;
            else if(space == TweenSpace.WORLD)
                cachedRot = _componentHolder.transform.eulerAngles;
        }

        //Returns cached rotation
        public override void AlterValue(ref Vector3 _value, float _tweenTime)
        {
            _value = cachedRot;
        }
    }
}
