using System.Collections.Generic;
using System.Linq;
using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEditor.AssetImporters;
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
            TweenActionBuilder.BuildTweenAction(runtimeAsset, startNode, graph);

            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                    "Assets/JoeTween/Icons/TweenGraphIcon.png");

            EditorGUIUtility.SetIconForObject(runtimeAsset, icon);


            ctx.AddObjectToAsset("RuntimeAsset", runtimeAsset);
            ctx.SetMainObject(runtimeAsset);
        }

        public static int FindIndexInArray(INode[] _arr, INode _obj)
        {
            for (int i = 0; i < _arr.Length; i++)
            {
                if (_obj == _arr[i])
                    return i;
            }

            return -1;
        }

        public static void BuildTweenAction(TweenRuntimeGraph _runtimeAsset, INode _startNode, TweenSequenceGraph _graph)
        {
            TweenSequenceData data = new TweenSequenceData();

            INode[] allNodes = _graph.GetNodes().Where(node => node is TweenActionNodeBase).ToArray();
            _runtimeAsset.actionData = new TweenRuntimeData[allNodes.Length];
            _runtimeAsset.actions = new TweenAction[allNodes.Length];

            for (int i = 0; i < allNodes.Length; i++)
            {
                var newData = new TweenRuntimeData();
                _runtimeAsset.actionData[i] = newData;
                _runtimeAsset.actions[i] = BuildTweenActionFromNode(data, allNodes[i]);

                INode[] nextNodes = GetNextNodes(allNodes[i]);
                newData.sequencedActions = new int[nextNodes.Length];

                for (int j = 0; j < nextNodes.Length; j++)
                {
                    int idx = FindIndexInArray(allNodes, nextNodes[j]);

                    if (idx != -1)
                        newData.sequencedActions[j] = idx;
                }
            }

            int startIdx = FindIndexInArray(allNodes, _startNode);
            _runtimeAsset.entranceIndex = startIdx;
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

        static TweenAction BuildTweenActionFromNode(TweenSequenceData _data, INode _node)
        {
            TweenAction cur = null;

            if (_node is TweenActionNodeBase nodeBase)
            {
                cur = nodeBase.GetTweenAction(_data);
            }

            return cur;
        }
    }
}
