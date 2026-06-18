using System;
using UnityEngine;


namespace JoeTween
{
    [Serializable]
    class RotateSinActionNode : TweenActionNode<UnityEngine.Transform>
    {
        public Vector3 axis;
        public float frequency;
        public float amplitude;
        public TweenSpace space;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Vector3>("Rotation Axis").Build();
            context.AddInputPort<float>("Frequency").Build();
            context.AddInputPort<float>("Amplitude").Build();

            context.AddInputPort<TweenSpace>("Space").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            RotateSinTween tween = new RotateSinTween
            (
                animationCurve, tweenLength, false,
                null, axis, frequency, amplitude
            );


            TweenSequenceNodeExtensions.LoadMod(ref tween.axis, GetInputPortByName("Rotation Axis"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.frequency, GetInputPortByName("Frequency"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.amplitude, GetInputPortByName("Amplitude"));

            tween.space = space;

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Rotation Axis").TryGetValue(out axis);
            GetInputPortByName("Frequency").TryGetValue(out frequency);
            GetInputPortByName("Amplitude").TryGetValue(out amplitude);

            GetInputPortByName("Space").TryGetValue(out space);
        }
    }
}
