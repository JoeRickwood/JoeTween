using System;
using Unity.GraphToolkit.Editor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    class MoveActionNode : TweenActionNode<UnityEngine.Transform>
    {
        public Vector3 startPosition;
        public Vector3 endPosition;
        public TweenSpace space;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Vector3>("Start Position").Build();
            context.AddInputPort<Vector3>("End Position").Build();

            context.AddInputPort<TweenSpace>("Space").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            MoveTween tween = new MoveTween
            (
                animationCurve, tweenLength, false,
                null, startPosition, endPosition
            );

            tween.startPos.LoadMod(GetInputPortByName("Start Position"));
            tween.endPos.LoadMod(GetInputPortByName("End Position"));

            tween.space = space;

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Start Position").TryGetValue(out startPosition);
            GetInputPortByName("End Position").TryGetValue(out endPosition);

            GetInputPortByName("Space").TryGetValue(out space);
        }
    }
}
