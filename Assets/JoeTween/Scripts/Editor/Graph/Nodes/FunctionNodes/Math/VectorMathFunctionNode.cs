using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class VectorMathFunctionNode : JoeTweenFunctionNode<Vector3>
    {
        TweenValue<Vector3> a;
        TweenValue<Vector3> b;
        VectorMathExpression expression;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<Vector3>("Position").Build();

            context.AddInputPort<VectorMathExpression>("Type").Build();

            context.AddInputPort<Vector3>("A").Build();
            context.AddInputPort<Vector3>("B").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            VectorMathTweenModifier mod = new VectorMathTweenModifier(expression, new(a.value, a.valueModifier), new(b.value, b.valueModifier));

            return mod;
        }

        public override void LoadValues()
        {
            expression = VectorMathExpression.ADD;
            GetInputPortByName("Type").TryGetValue(out expression);

            a = new TweenValue<Vector3>();
            a.valueModifier = GetModFromPort<Vector3>(GetInputPortByName("A"));
            GetInputPortByName("A").TryGetValue(out a.value);

            b = new TweenValue<Vector3>();
            b.valueModifier = GetModFromPort<Vector3>(GetInputPortByName("B"));
            GetInputPortByName("B").TryGetValue(out b.value);
        }
    }
}
