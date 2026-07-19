using System;
using System.Collections.Generic;
using System.Reflection;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    public struct TweenSequenceData
    {

    }

    public abstract class JoeTweenNode : Node
    {
        protected void LoadValueFromPort<T>(IPort _port, out T _value)
        {
            if (_port.TryGetValue(out _value))
                return;
        }

        protected abstract void LoadValues();
    }


    public abstract class JoeTweenFunctionNode : JoeTweenNode
    {
        public abstract TweenActionValueModifier GetModifier();
    }

    public abstract class JoeTweenFunctionNode<T> : JoeTweenFunctionNode
    {
        public TweenActionValueModifier<T> GetModFromPort(IPort _port) 
        {
            TweenActionValueModifier<T> nodeMod = null;
            List<IPort> connectedPorts = new List<IPort>();
            _port.GetConnectedPorts(connectedPorts);

            if (connectedPorts == null || connectedPorts.Count == 0)
                return nodeMod;

            IPort port = connectedPorts[0];

            if (port.GetNode() is JoeTweenFunctionNode<T> node)
            {
                nodeMod = node.GetModifier() as TweenActionValueModifier<T>;
            }

            return nodeMod;
        }
    }

    [Serializable]
    class TweenStartNode : TweenActionNodeBase
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<TweenSequenceData>("Output").WithConnectorUI(PortConnectorUI.Arrowhead).Build();
        }

        protected override void LoadValues()
        {

        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            TweenAction tween = new BlankTween
            (
                null, 0, false, null
            );

            return tween;
        }
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
}
