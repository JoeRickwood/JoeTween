using System;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;


namespace JoeTween
{
    [Serializable]
    class TweenMaterialFlashNode : TweenActionNode<MeshFilter>
    {
        public Material material;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<Material>("Material").Build();
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            MaterialFlashTween tween = new MaterialFlashTween
            (
                animationCurve, tweenLength, false,
                null, material
            );

            return tween;
        }

        public override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Material").TryGetValue(out material);
        }
    }
}
