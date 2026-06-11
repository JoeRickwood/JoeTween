using System;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEngine;
namespace JoeTween
{
    public struct TweenSequenceData
    {

    }

    public abstract class JoeTweenNode : Node
    {
        protected abstract void LoadValues();
    }


    [Serializable]
    public abstract class TweenActionNode<T> : JoeTweenNode
    {
        protected AnimationCurve animationCurve;
        protected float tweenLength;
        protected T component;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<TweenSequenceData>("Input").Build();

            context.AddInputPort<T>("Component").Build();
            context.AddInputPort<AnimationCurve>("Tween Curve").Build();
            context.AddInputPort<float>("Tween Length").Build();
        }

        public abstract TweenAction GetTweenAction();

        protected override void LoadValues()
        {
            GetInputPortByName("Component").TryGetValue(out component);
            GetInputPortByName("Tween Curve").TryGetValue(out animationCurve);
            GetInputPortByName("Tween Length").TryGetValue(out tweenLength);
        }
    }

    [Serializable]
    class MoveActionNode : TweenActionNode<UnityEngine.Transform>
    {
        public Vector3 startPosition;
        public Vector3 endPosition;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Vector3>("Start Position").Build();
            context.AddInputPort<Vector3>("End Position").Build();

            context.AddOutputPort<TweenSequenceData>("Output").Build();
        }

        public override TweenAction GetTweenAction()
        {
            TweenAction<UnityEngine.Transform> tween = new MoveTween
            (
                animationCurve, tweenLength, false,
                component, Vector3.zero, new Vector3(5, 5, 0)
            );

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Start Position").TryGetValue(out startPosition);
            GetInputPortByName("End Position").TryGetValue(out startPosition);
        }
    }
}
