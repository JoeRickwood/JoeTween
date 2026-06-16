using System;
using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEditor.AssetImporters;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using UnityEngine;

namespace JoeTween
{
    [ScriptedImporter(1, TweenSequenceGraph.AssetExtension)]
    internal class TweenActionBuilder : ScriptedImporter
    {
        //BUILDS THE RUNTIME ASSET REFERENCE
        public override void OnImportAsset(AssetImportContext ctx)
        {
            var graph = GraphDatabase.LoadGraphForImporter<TweenSequenceGraph>(ctx.assetPath);

            if (graph == null)
            {
                Debug.LogError($"Failed to load TweenSequenceGraph asset: {ctx.assetPath}");
                return;
            }
 
            var runtimeAsset = ScriptableObject.CreateInstance<TweenRuntimeGraph>();

            INode startNode = graph.GetStartNode();
            TweenActionBuilder.BuildTweenAction(runtimeAsset, startNode);

            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                    "Assets/JoeTween/Icons/TweenGraphIcon.png");

            EditorGUIUtility.SetIconForObject(runtimeAsset, icon);


            ctx.AddObjectToAsset("RuntimeAsset", runtimeAsset);
            ctx.SetMainObject(runtimeAsset);
        }

        private struct QueuedAction
        {
            public INode node;
            public TweenAction action;

            internal QueuedAction(INode _node, TweenAction _action)
            {
                node = _node;
                action = _action;
            }
        }

        //Adds A Array Of T Values To A Queue Of Type T
        private static void AddArrayToQueue<T>(ref Queue<T> _queue, ref T[] _arr)
        {
            for (int i = 0; i < _arr.Length; i++)
            {
                _queue.Enqueue(_arr[i]);
            }
        }

        public static void BuildTweenAction(TweenRuntimeGraph _runtimeAsset, INode _startNode)
        {
            TweenSequenceData data = new TweenSequenceData();

            var visited = new HashSet<INode>();

            // Queue for breadth-first traversal
            var nodesToProcess = new Queue<QueuedAction>();
            
            var firstNode = BuildQueuedActionArray(data, GetNextNodes(_startNode));

            if (firstNode != null)
            {
                AddArrayToQueue(ref nodesToProcess, ref firstNode);
            }

            // Process all reachable nodes
            int index = 0;
            while (nodesToProcess.Count > 0)
            {
                var currentNode = nodesToProcess.Dequeue();

                // Skip if we've already processed this node
                if (!visited.Add(currentNode.node))
                    continue;

                var nodes = BuildQueuedActionArray(data, GetNextNodes(currentNode.node));
                _runtimeAsset.actions.Add(currentNode.action);
                _runtimeAsset.strides.Add(nodes.Length);

                for (int i = 0; i < nodes.Length; i++)
                {
                    _runtimeAsset.indexSequences.Add(_runtimeAsset.GetIndex(nodes[i].action));
                }

                AddArrayToQueue(ref nodesToProcess, ref nodes);
                index++;
            }

            BlankTween tween = new BlankTween(null, 0, false, null);
            _runtimeAsset.actions.Add(tween);
            _runtimeAsset.strides.Add(firstNode.Length);
            _runtimeAsset.entranceIndex = index;

            for (int i = 0; i < firstNode.Length; i++)
            {
                _runtimeAsset.indexSequences.Add(_runtimeAsset.GetIndex(firstNode[i].action));
            }
        }

        static INode[] GetNextNodes(INode currentNode)
        {
            var outputPort = currentNode.GetOutputPortByName("Output");

            List<IPort> outPorts = new List<IPort>();
            outputPort.GetConnectedPorts(outPorts);

            INode[] nodes = new INode[outPorts.Count];
            for (int i = 0; i < nodes.Length; i++)
            {
                nodes[i] = outPorts[i].GetNode();
            }
            
            return nodes;
        }

        static QueuedAction[] BuildQueuedActionArray(TweenSequenceData _data, INode[] _nodes)
        {
            QueuedAction[] arr = new QueuedAction[_nodes.Length];

            for (int i = 0; i < _nodes.Length; i++)
            {
                arr[i] = new QueuedAction(_nodes[i], (_nodes[i] as TweenActionNodeBase).GetTweenAction(_data));
            }

            return arr;
        }
    }
}
