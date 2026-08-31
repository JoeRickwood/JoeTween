using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace JoeTween
{
    [Graph(AssetExtension)]
    [Serializable]
    public class TweenSequenceGraph : Graph
    {
        public const string AssetExtension = "twg";

        [MenuItem("Assets/Create/JoeTween/TweenSequenceGraph", false)]
        static void CreateAssetFile()
        {
            GraphDatabase.PromptInProjectBrowserToCreateNewAsset<TweenSequenceGraph>();

            EditorApplication.delayCall += () =>
            {
                var asset = Selection.activeObject;

                var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                    "Assets/JoeTween/Icons/TweenGraphIcon.png");

                EditorGUIUtility.SetIconForObject(asset, icon);
            };
        }

        public override void OnGraphChanged(GraphLogger infos)
        {
            base.OnGraphChanged(infos);

            CheckGraphErrors(infos);
        }

        void CheckGraphErrors(GraphLogger infos)
        {
            List<TweenStartNode> startNodes = GetNodes().OfType<TweenStartNode>().ToList();

            switch (startNodes.Count)
            {
                case 0:
                    infos.LogError("Add a StartNode in your Tween Sequence Graph.", this);
                    break;
                case >= 1:
                    {
                        foreach (var startNode in startNodes.Skip(1))
                        {
                            infos.LogWarning($"JoeTween only supports one StartNode per graph. Only the first created one will be used.", startNode);
                        }
                        break;
                    }
            }
        }

        public INode GetStartNode()
        {
            return GetNodes().OfType<TweenStartNode>().ToList().FirstOrDefault(); 
        }

        internal TweenStartNode GetJoeTweenStartNode()
        {
            return GetNodes().OfType<TweenStartNode>().ToList().FirstOrDefault();
        }
    }
}