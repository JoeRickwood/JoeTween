using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ScreenPositionTweenModifier : TweenActionValueModifier<Vector3>
    {
        public Vector2 anchor;

        public ScreenPositionTweenModifier(Vector2 _anchor)
        {
            anchor = _anchor;
        }

        public override void AlterValue(out Vector3 _value)
        {
            _value = new Vector3(Screen.width * anchor.x, Screen.height * anchor.y, 0);
        }
    }
}
