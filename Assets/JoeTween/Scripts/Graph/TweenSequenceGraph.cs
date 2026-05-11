using System;
using UnityEditor;
using UnityEngine;
using Unity.GraphToolkit.Editor;

[Graph(AssetExtension)]
[Serializable]
class TweenSequenceGraph : Graph
{
    public const string AssetExtension = "twg";

    [MenuItem("Assets/Create/JoeTween/TweenSequenceGraph", false)]
    static void CreateAssetFile()
    {
        GraphDatabase.PromptInProjectBrowserToCreateNewAsset<TweenSequenceGraph>();
    }
}
