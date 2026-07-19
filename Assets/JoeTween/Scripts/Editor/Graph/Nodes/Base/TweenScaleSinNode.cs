using System;
using UnityEngine;


namespace JoeTween
{
    [Serializable]
    class ScaleSinActionNode : TweenActionNode<UnityEngine.Transform>
    {
        public Vector3 startScale;
        public Vector3 axis;
        public float frequency;
        public float amplitude;
        public TweenSpace space;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Vector3>("Start Scale").Build();
            context.AddInputPort<Vector3>("Scale Axis").Build();

            context.AddInputPort<float>("Frequency").Build();
            context.AddInputPort<float>("Amplitude").Build();

            context.AddInputPort<TweenSpace>("Space").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            ScaleSinTween tween = new ScaleSinTween
            (
                animationCurve, tweenLength, false,
                null, startScale, axis, amplitude, frequency
            );

            TweenSequenceNodeExtensions.LoadMod(ref tween.startScale, GetInputPortByName("Start Scale"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.axis, GetInputPortByName("Scale Axis"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.speed, GetInputPortByName("Frequency"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.amplitude, GetInputPortByName("Amplitude"));

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Start Scale").TryGetValue(out startScale);
            GetInputPortByName("Scale Axis").TryGetValue(out axis);
            GetInputPortByName("Frequency").TryGetValue(out frequency);
            GetInputPortByName("Amplitude").TryGetValue(out amplitude);

            GetInputPortByName("Space").TryGetValue(out space);
        }
    }
}
