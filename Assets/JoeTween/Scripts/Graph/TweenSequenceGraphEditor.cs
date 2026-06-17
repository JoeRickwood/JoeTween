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
}
