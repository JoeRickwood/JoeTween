/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   ChangeColorTween.cs
    Description :   Tweens That Make Use Of Color Changing On Both UI + Sprites
    Author      :   Joe Rickwood
**************************************************************************/


using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace JoeTween
{

    /// <summary>
    /// Should Be Used On Sprite Objects
    /// </summary>
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

        internal override void StartAction()
        {
            if(component != null)
                startColor.value = component.color;

            startColor.OnActionStart(target);
            opacity.OnActionStart(target);
            startColor.OnActionStart(target);

            base.StartAction();
        }


        public override void CloneModifiers()
        {
            if (colorChange.valueModifier != null)
                colorChange.valueModifier = colorChange.valueModifier.Clone<Color>();

            if (opacity.valueModifier != null)
                opacity.valueModifier = opacity.valueModifier.Clone<float>();

            if (startColor.valueModifier != null)
                startColor.valueModifier = startColor.valueModifier.Clone<Color>();


            base.CloneModifiers();
        }

        protected override void EndAction()
        {
            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing && component != null)
            {
                component.color = Color.LerpUnclamped(startColor.GetValue(tweenTime), colorChange.GetValue(tweenTime), tweenTime * opacity.GetValue(tweenTime));
            }
        }
    }

    /// <summary>
    /// Should Be Used On Any UI Objects Utilising The Graphic Component Including Text, Images, Raw Images Ect
    /// </summary>
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

        internal override void StartAction()
        {
            if (component != null)
                startColor.value = component.color;

            base.StartAction();
        }

        protected override void EndAction()
        {
            base.EndAction();
        }


        public override void Update(float _time)
        {
            base.Update(_time);

            float tweenTime = GetTweenProgress(_time);

            if (playing && component != null)
            {
                component.color = Color.LerpUnclamped(startColor.GetValue(tweenTime), colorChange.GetValue(tweenTime), tweenTime * opacity.GetValue(tweenTime));
            }
        }
    }
}
