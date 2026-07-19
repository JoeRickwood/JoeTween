using System;
using UnityEngine;


namespace JoeTween
{
    [Serializable]
    class MoveSinActionNode : TweenActionNode<UnityEngine.Transform>
    {
        public Vector3 startPosition;
        public Vector3 axis;
        public float frequency;
        public float amplitude;
        public TweenSpace space;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Vector3>("Start Position").Build();
            context.AddInputPort<Vector3>("Move Axis").Build();

            context.AddInputPort<float>("Frequency").Build();
            context.AddInputPort<float>("Amplitude").Build();

            context.AddInputPort<TweenSpace>("Space").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            MoveSinTween tween = new MoveSinTween
            (
                animationCurve, tweenLength, false,
                null, startPosition, axis, amplitude, frequency
            );

            TweenSequenceNodeExtensions.LoadMod(ref tween.startPos, GetInputPortByName("Start Position"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.axis, GetInputPortByName("Move Axis"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.speed, GetInputPortByName("Frequency"));
            TweenSequenceNodeExtensions.LoadMod(ref tween.amplitude, GetInputPortByName("Amplitude"));

            tween.space = space;

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Start Position").TryGetValue(out startPosition);
            GetInputPortByName("Move Axis").TryGetValue(out axis);
            GetInputPortByName("Frequency").TryGetValue(out frequency);
            GetInputPortByName("Amplitude").TryGetValue(out amplitude);

            GetInputPortByName("Space").TryGetValue(out space);
        }
    }
}
