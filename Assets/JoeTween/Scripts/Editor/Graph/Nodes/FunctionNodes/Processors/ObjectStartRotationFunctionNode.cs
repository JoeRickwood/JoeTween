using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ObjectStartRotationFunctionNode : JoeTweenFunctionNode<Vector3>
    {
        public TweenSpace space;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<Vector3>("Rotation").Build();
            context.AddInputPort<TweenSpace>("Tween Space").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            return new ObjectStartRotationTweenModifier(space);
        }

        protected override void LoadValues()
        {
            LoadValueFromPort(GetInputPortByName("Tween Space"), out space);
        }
    }
}
