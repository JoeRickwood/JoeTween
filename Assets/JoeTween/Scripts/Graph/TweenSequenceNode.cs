using NUnit.Framework;
using System;
using System.Collections.Generic;
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
        public JoeTweenNode[] nextNodeIDs;

        protected abstract void LoadValues();
    }

    [Serializable]
    class TweenStartNode : JoeTweenNode
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<TweenSequenceData>("Output").WithConnectorUI(PortConnectorUI.Arrowhead).Build();
        }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }
    }

    public interface IJoeTweenActionNode
    {

    }


    public abstract class TweenActionNodeBase : JoeTweenNode
    {
        public abstract TweenAction GetTweenAction(TweenSequenceData _data);
    }


    [Serializable]
    public abstract class TweenActionNode<T> : TweenActionNodeBase
    {
        protected AnimationCurve animationCurve;
        protected float tweenLength;
        protected T component;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddInputPort<TweenSequenceData>("Input").WithConnectorUI(PortConnectorUI.Arrowhead).Build();
            context.AddOutputPort<TweenSequenceData>("Output").WithConnectorUI(PortConnectorUI.Arrowhead).Build();

            context.AddInputPort<AnimationCurve>("Tween Curve").Build();
            context.AddInputPort<float>("Tween Length").Build();
        }

        protected override void LoadValues()
        {
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
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            TweenAction<UnityEngine.Transform> tween = new MoveTween
            (
                animationCurve, tweenLength, false,
                null, startPosition, endPosition
            );

            return tween;
        }

        protected override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Start Position").TryGetValue(out startPosition);
            GetInputPortByName("End Position").TryGetValue(out endPosition);
        }
    }
}
