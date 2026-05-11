using System.Collections.Generic;
using UnityEngine;

namespace JoeTween
{
    public static class TweenManager
    {
        private static bool Initialized = false;

        //Link GameObjects To Tween Sequences
        private static Dictionary<GameObject, List<Tween>> activeTweenSequences;


        private static void Initialize()
        {
            Initialized = true;

            activeTweenSequences = new Dictionary<GameObject, List<Tween>>();
        }

        public static void StartTween(GameObject _obj, Tween _tween)
        {
            if(!Initialized)
                Initialize();

            activeTweenSequences[_obj].Add(_tween);
        }

        public static void StopTween(GameObject _obj, Tween _tween)
        {
            if (!Initialized)
                Initialize();

            //TODO -> Code This
        }

        public static void StopAllTweens(GameObject _obj)
        {
            if (!Initialized)
                Initialize();

            //TODO -> Code This
        }

    }
}
