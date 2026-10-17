using System;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public class DampenValueFunctionNode : JoeTweenFunctionNode<float>
    {
        public float baseValue;
        public AnimationCurve dampenCurve;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            context.AddOutputPort<float>("Value").Build();

            context.AddInputPort<float>("Base Value").Build();
            context.AddInputPort<AnimationCurve>("Dampen Curve").Build();
        }

        public override TweenActionValueModifier GetModifier()
        {
            LoadValues();

            return new DampenValueModifier(baseValue, dampenCurve);
        }

        public override void LoadValues()
        {
            LoadValueFromPort(GetInputPortByName("Base Value"), out baseValue);
            LoadValueFromPort(GetInputPortByName("Dampen Curve"), out dampenCurve);
        }
    }
}
