using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace JoeTween
{
    [Serializable]
    class ChangeSpriteColorActionNode : TweenActionNode<UnityEngine.SpriteRenderer>
    {
        public Color colorChange;
        public float opacity;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Color>("Color Change").Build();
            context.AddInputPort<float>("Opacity").Build();
        }


        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            ChangeSpriteColorTween tween = new ChangeSpriteColorTween
            (
                animationCurve, tweenLength, false,
                null, colorChange, opacity
            );

            TweenSequenceNodeExtensions.LoadMod(ref tween.colorChange, GetInputPortByName("Color Change"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.opacity, GetInputPortByName("Opacity"));

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Color Change").TryGetValue(out colorChange);
            GetInputPortByName("Opacity").TryGetValue(out opacity);
        }
    }


    [Serializable]
    class ChangeUIColorActionNode : TweenActionNode<Graphic>
    {
        public Color colorChange;
        public float opacity;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Color>("Color Change").Build();
            context.AddInputPort<float>("Opacity").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            ChangeUIColorTween tween = new ChangeUIColorTween
            (
                animationCurve, tweenLength, false,
                null, colorChange, opacity
            );

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Color Change").TryGetValue(out colorChange);
            GetInputPortByName("Opacity").TryGetValue(out opacity);
        }
    }
}
