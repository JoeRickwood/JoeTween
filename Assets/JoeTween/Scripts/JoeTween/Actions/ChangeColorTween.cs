using UnityEngine;
using UnityEngine.UI;

namespace JoeTween
{
    [System.Serializable]
    public class ChangeSpriteColorTween : TweenAction<SpriteRenderer>
    {
        public TweenValue<Color> colorChange;
        public TweenValue<float> opacity;
        private TweenValue<Color> startColor;
        
        public ChangeSpriteColorTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            SpriteRenderer _component, Color _colorChange, float _opacity
        ) : base(_animationCurve, _animationLength, _component, _looping)
        {
            colorChange.value = _colorChange;
            opacity.value = _opacity; 
        }

        public override void StartAction()
        {
            startColor.value = component.color;

            base.StartAction();
        }

        public override void EndAction()
        {
            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing)
            {
                component.color = Color.LerpUnclamped(startColor, colorChange, GetTweenProgress(_time) * opacity);
            }
        }
    }

    [System.Serializable]
    public class ChangeUIColorTween : TweenAction<Graphic>
    {
        public TweenValue<Color> colorChange;
        public TweenValue<float> opacity;
        private TweenValue<Color> startColor;

        public ChangeUIColorTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Graphic _component, Color _colorChange, float _opacity
        ) : base(_animationCurve, _animationLength, _component, _looping)
        {
            colorChange.value = _colorChange;
            opacity.value = _opacity;
        }

        public override void StartAction()
        {
            startColor.value = component.color;

            base.StartAction();
        }

        public override void EndAction()
        {
            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing)
            {
                component.color = Color.LerpUnclamped(startColor, colorChange, GetTweenProgress(_time) * opacity);
            }
        }
    }
}
