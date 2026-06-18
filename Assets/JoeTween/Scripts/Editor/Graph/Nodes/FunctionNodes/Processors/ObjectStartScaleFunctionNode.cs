using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class ObjectStartScaleFunctionNode : JoeTweenFunctionNode<Vector3>
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<Vector3>("Scale").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            return new ObjectStartScaleTweenModifier();
        }

        protected override void LoadValues()
        {

        }
    }
}
