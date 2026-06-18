using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class MakeVectorFunctionNode : JoeTweenFunctionNode<Vector3>
    {
        TweenValue<float> x;
        TweenValue<float> y;
        TweenValue<float> z;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<Vector3>("Vector").Build();

            context.AddInputPort<float>("X").Build();
            context.AddInputPort<float>("Y").Build();
            context.AddInputPort<float>("Z").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            MakeVectorTweenModifier mod = new MakeVectorTweenModifier(
                new(x.value, x.valueModifier), 
                new(y.value, y.valueModifier),
                new(z.value, z.valueModifier)
            );

            return mod;
        }

        protected override void LoadValues()
        {
            x = new TweenValue<float>();
            TweenSequenceNodeExtensions.LoadMod(ref x, GetInputPortByName("X"));
            GetInputPortByName("X").TryGetValue(out x.value);

            y = new TweenValue<float>();
            TweenSequenceNodeExtensions.LoadMod(ref y, GetInputPortByName("Y"));
            GetInputPortByName("Y").TryGetValue(out y.value);

            z = new TweenValue<float>();
            TweenSequenceNodeExtensions.LoadMod(ref z, GetInputPortByName("Z"));
            GetInputPortByName("Y").TryGetValue(out z.value);
        }
    }
}
