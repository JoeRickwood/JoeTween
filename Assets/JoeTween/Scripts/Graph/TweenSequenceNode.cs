using System;
using UnityEditor;
using UnityEngine;
using Unity.GraphToolkit.Editor;
namespace JoeTween
{
    [Serializable]
    class TweenSequenceNode : Node 
    {
        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            //context.AddInputPort<float>("Input").Build();
            //context.AddOutputPort<MyCustomType>("Output").Build();
        }
    }
}
