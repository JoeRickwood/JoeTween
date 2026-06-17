using System;
using UnityEngine;


namespace JoeTween
{
    [Serializable]
    class ScaleActionNode : TweenActionNode<UnityEngine.Transform>
    {
        public Vector3 startScale;
        public Vector3 endScale;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Vector3>("Start Scale").Build();
            context.AddInputPort<Vector3>("End Scale").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            ScaleTween tween = new ScaleTween
            (
                animationCurve, tweenLength, false,
                null, startScale, endScale
            );

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Start Scale").TryGetValue(out startScale);
            GetInputPortByName("End Scale").TryGetValue(out endScale);
        }
    }
}
