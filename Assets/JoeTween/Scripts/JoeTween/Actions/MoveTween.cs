using UnityEngine;

namespace JoeTween
{
    internal class MoveTween : TweenAction<Transform>
    {
        public override void Update(float _deltaTime)
        {
            component.position
        }
    }
}
