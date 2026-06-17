using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ScreenPositionFunctionNode : JoeTweenFunctionNode<Vector3>
    {
        public Vector2 anchor;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<Vector3>("Screen Position").Build();

            context.AddInputPort<Vector2>("Screen Anchor").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            return new ScreenPositionTweenModifier(anchor);
        }

        protected override void LoadValues()
        {
            LoadValueFromPort(GetInputPortByName("Screen Anchor"), out anchor);
        }
    }
}
