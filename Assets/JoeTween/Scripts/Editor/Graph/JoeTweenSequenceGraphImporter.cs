/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   TweenSequenceGraphImporter.cs
    Description :   Creates The Runtime Assets From The Graph Objects
    Author      :   Joe Rickwood
**************************************************************************/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;

using Unity.GraphToolkit.Editor;
using UnityEditor;
using UnityEditor.AssetImporters;

//This Script Is Placed Within And Editor Folder Which Is Ignored On Build Compile
namespace JoeTween
{
    [ScriptedImporter(1, TweenSequenceGraph.AssetExtension)]
    internal class TweenActionBuilder : ScriptedImporter
    {
        //BUILDS THE RUNTIME ASSET REFERENCE
        public override void OnImportAsset(AssetImportContext ctx)
        {
            //Loads The Graph Into The Importer
            var graph = GraphDatabase.LoadGraphForImporter<TweenSequenceGraph>(ctx.assetPath);

            if (graph == null)
            {
                Debug.LogError($"Failed to load TweenSequenceGraph asset: {ctx.assetPath}");
                return;
            }
 
            var runtimeAsset = ScriptableObject.CreateInstance<TweenRuntimeGraph>();

            //Finds The Start Node Of The Graph
            INode startNode = graph.GetStartNode();
            TweenStartNode tweenStartNode = graph.GetJoeTweenStartNode();

            //Creates The Sequence From The Start Node And Puts It Into The Runtime Asset
            TweenActionBuilder.BuildTweenActionSequence(runtimeAsset, startNode, tweenStartNode, graph);

            //Sets The Texture Of The Asset To A Icon Img
            var icon = AssetDatabase.LoadAssetAtPath<Texture2D>(
                    "Assets/Plugins/JoeTween/Icons/TweenGraphIcon.png");

            EditorGUIUtility.SetIconForObject(runtimeAsset, icon);

            //Applies The Runtime Asset Data To The Created TweenRuntimeGraph Instance
            ctx.AddObjectToAsset("RuntimeAsset", runtimeAsset);
            ctx.SetMainObject(runtimeAsset);
        }

        public static void BuildTweenActionSequence(TweenRuntimeGraph _runtimeAsset, INode _startNode, TweenStartNode _joeTweenStartNode, TweenSequenceGraph _graph)
        {
            TweenSequenceData data = new TweenSequenceData();

            //Get All ACTION Nodes In The Graph
            INode[] allNodes = _graph.GetNodes().Where(node => node is TweenActionNodeBase).ToArray();

            //Initialize The Lists To Be The Same Size As The Node Count
            _runtimeAsset.actionData = new TweenRuntimeData[allNodes.Length];
            _runtimeAsset.actions = new TweenAction[allNodes.Length];

            _joeTweenStartNode?.LoadValues();
            _runtimeAsset.loopCount = _joeTweenStartNode.loopCount;

            for (int i = 0; i < allNodes.Length; i++)
            {
                var newData = new TweenRuntimeData();
                _runtimeAsset.actionData[i] = newData;

                //Foreach Action Node, Create A Tween Action, Not Linked To Any Sequenced Yet
                _runtimeAsset.actions[i] = BuildTweenActionFromNode(data, allNodes[i]); 

                //Find The Output Node Values From This Action Nodes Output (Connects To Other Action Nodes)
                INode[] nextNodes = GetNextNodes(allNodes[i]);
                newData.sequencedActions = new int[nextNodes.Length]; //Initialie The Sequenced Actions To Be The Same Length As Output Nodes

                for (int j = 0; j < nextNodes.Length; j++)
                {
                    //Find The Index of The Sequenced Action
                    int idx = FindIndexInArray(allNodes, nextNodes[j]);

                    //If The Sequenced Node Exists, Apply It To The Object, If The Value Is -1, The Node Does Not Exist
                    newData.sequencedActions[j] = idx;
                }
            }

            //Find The Start Index
            int startIdx = FindIndexInArray(allNodes, _startNode);
            _runtimeAsset.entranceIndex = startIdx; //Assign The Entrance Index
        }


        //_____________________________________________________________
        //Helper Functions Used In The Creation Of The RuntimeAsset

        private static INode[] GetNextNodes(INode currentNode)
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

        private static TweenAction BuildTweenActionFromNode(TweenSequenceData _data, INode _node)
        {
            TweenAction cur = null;

            if (_node is TweenActionNodeBase nodeBase)
            {
                cur = nodeBase.GetTweenAction(_data);
            }

            return cur;
        }

        private static int FindIndexInArray(INode[] _arr, INode _obj)
        {
            for (int i = 0; i < _arr.Length; i++)
            {
                if (_obj == _arr[i])
                    return i;
            }

            return -1;
        }
    }
}