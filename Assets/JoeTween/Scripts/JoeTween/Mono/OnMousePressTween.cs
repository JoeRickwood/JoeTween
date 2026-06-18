using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace JoeTween
{
    public class OnMousePressTween : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Flags]
        public enum MouseDownState
        {
            None = 0,
            MouseDown = 1,
            MouseUp = 2
        }

        [Serializable]
        public struct MouseDownTweenData
        {
            public MouseDownState state;
            public TweenRuntimeGraph tween;
        }

        public MouseDownTweenData[] tweens;

        public void OnPointerDown(PointerEventData eventData)
        {
            foreach (var t in tweens)
            {
                if (t.state.HasFlag(MouseDownState.MouseDown))
                    TweenManager.StartTween(gameObject, t.tween);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            foreach (var t in tweens)
            {
                if (t.state.HasFlag(MouseDownState.MouseUp))
                    TweenManager.StartTween(gameObject, t.tween);
            }
        }
    }
}
