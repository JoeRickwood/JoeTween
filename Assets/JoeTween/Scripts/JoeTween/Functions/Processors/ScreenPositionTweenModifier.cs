using System;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Used To Convert From Screen Space Into Object-Space
    /// TODO -> Change To Transform Space Node
    /// </summary>
    [Serializable]
    public class ScreenPositionTweenModifier : TweenActionValueModifier<Vector3>
    {
        //Anchor To Be Placed On The Screen
        // E.g. (0.5, 0.5) = center of screen
        public Vector2 anchor;

        public ScreenPositionTweenModifier(Vector2 _anchor)
        {
            anchor = _anchor;
        }

        public override void OnActionStart(GameObject _componentHolder)
        {
            
        }

        public override void AlterValue(out Vector3 _value)
        {
            //Gets Screen Width And Screen Height And Multiplies By The Anchors
            _value = new Vector3(Screen.width * anchor.x, Screen.height * anchor.y, 0);
        }
    }
}
