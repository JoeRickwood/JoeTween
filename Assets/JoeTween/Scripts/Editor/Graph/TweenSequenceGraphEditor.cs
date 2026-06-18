using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEngine;

namespace JoeTween
{
    [CustomEditor(typeof(TweenSequenceGraph))]
    public class TweenSequenceGraphEditor : Editor
    {   
        private void OnEnable()
        {
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                "Assets/JoeTween/Icons/TweenGraphIcon.png");

            if (icon != null)
            {
                EditorGUIUtility.SetIconForObject(target, icon);
            }
        }
    }

    public static class TweenSequenceNodeExtensions
    {
        public static void LoadMod<T>(ref TweenValue<T> tweenValue, IPort _port)
        {
            tweenValue.valueModifier = null;
            List<IPort> connectedPorts = new List<IPort>();
            _port.GetConnectedPorts(connectedPorts);

            if (connectedPorts == null || connectedPorts.Count == 0)
                return;

            IPort port = connectedPorts[0];

            if (port.GetNode() is JoeTweenFunctionNode<T> node)
            {
                tweenValue.valueModifier = node.GetModifier() as TweenActionValueModifier<T>;
            }
            else
            {
                tweenValue.valueModifier = null;
            }
        }
    }

    }
