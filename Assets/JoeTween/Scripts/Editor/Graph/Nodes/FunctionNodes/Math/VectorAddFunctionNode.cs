using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class VectorAddFunctionNode : JoeTweenFunctionNode<Vector3>
    {
        TweenValue<Vector3> a;
        TweenValue<Vector3> b;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<Vector3>("Position").Build();

            context.AddInputPort<Vector3>("A").Build();
            context.AddInputPort<Vector3>("B").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            VectorAddTweenModifier mod = new VectorAddTweenModifier(new(a.value, a.valueModifier), new(b.value, b.valueModifier));

            return mod;
        }

        protected override void LoadValues()
        {
            a = new TweenValue<Vector3>();
            a.valueModifier = GetModFromPort(GetInputPortByName("A"));
            GetInputPortByName("A").TryGetValue(out a.value);

            b = new TweenValue<Vector3>();
            b.valueModifier = GetModFromPort(GetInputPortByName("B"));
            GetInputPortByName("B").TryGetValue(out b.value);
        }
    }
}
