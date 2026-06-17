using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JoeTween
{
    public class OnMouseOverTween : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [Flags]
        public enum MouseOverState
        {
            None = 0,
            MouseEnter = 1,
            MouseExit = 2
        }

        [Serializable]
        public struct MouseOverTweenData
        {
           public MouseOverState state;
           public TweenRuntimeGraph tween;
        }

        public MouseOverTweenData[] tweens;

        public void OnPointerEnter(PointerEventData eventData)
        {
            foreach (var t in tweens)
            {
                if (t.state.HasFlag(MouseOverState.MouseEnter))
                    TweenManager.StartTween(gameObject, t.tween);
            }     
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            foreach (var t in tweens)
            {
                if (t.state.HasFlag(MouseOverState.MouseExit))
                    TweenManager.StartTween(gameObject, t.tween);
            }
        }
    }
}
