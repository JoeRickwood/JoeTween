using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ObjectStartPositionFunctionNode : JoeTweenFunctionNode<Vector3>
    {
        public TweenSpace space;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<Vector3>("Position").Build();
            context.AddInputPort<TweenSpace>("Tween Space").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            return new ObjectStartPositionTweenModifier(space);
        }

        protected override void LoadValues()
        {
            LoadValueFromPort(GetInputPortByName("Tween Space"), out space);
        }
    }
}
