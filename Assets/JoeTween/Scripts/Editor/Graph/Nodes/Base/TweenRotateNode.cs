using NUnit;
using System;
using UnityEngine;


namespace JoeTween
{
    [Serializable]
    class RotateActionNode : TweenActionNode<UnityEngine.Transform>
    {
        public Vector3 startRotation;
        public Vector3 endRotation;
        public TweenSpace space;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Vector3>("Start Rotation").Build();
            context.AddInputPort<Vector3>("End Rotation").Build();

            context.AddInputPort<TweenSpace>("Space").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            RotateTween tween = new RotateTween
            (
                animationCurve, tweenLength, false,
                null, startRotation, endRotation
            );


            TweenSequenceNodeExtensions.LoadMod(ref tween.startRotation, GetInputPortByName("Start Rotation"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.endRotation, GetInputPortByName("End Rotation"));

            tween.space = space;

            return tween;
        }

        public override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Start Rotation").TryGetValue(out startRotation);
            GetInputPortByName("End Rotation").TryGetValue(out endRotation);

            GetInputPortByName("Space").TryGetValue(out space);
        }
    }
}
