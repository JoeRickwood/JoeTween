using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class RandomRangeFunctionNode : JoeTweenFunctionNode<float>
    {
        TweenValue<float> min;
        TweenValue<float> max;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<float>("Random Value").Build();

            context.AddInputPort<float>("Min").Build();
            context.AddInputPort<float>("Max").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            RandomRangeTweenModifier mod = new RandomRangeTweenModifier(new(min.value, min.valueModifier), new(max.value, max.valueModifier));

            return mod;
        }

        public override void LoadValues()
        {
            min = new TweenValue<float>();
            TweenSequenceNodeExtensions.LoadMod(ref min, GetInputPortByName("Min"));
            GetInputPortByName("Min").TryGetValue(out min.value);

            max = new TweenValue<float>();
            TweenSequenceNodeExtensions.LoadMod(ref max, GetInputPortByName("Max"));
            GetInputPortByName("Max").TryGetValue(out max.value);
        }
    }
}
