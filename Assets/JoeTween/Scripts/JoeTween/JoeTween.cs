using System.Collections.Generic;
using UnityEngine;

namespace JoeTween
{
    public enum TweenSpace
    {
        LOCAL,
        WORLD
    }

    public static class TweenManager
    {
        private static bool Initialized = false;

        //Link GameObjects To Tween Sequences
        private static Dictionary<object, List<Tween>> activeTweenSequences;
        private static GameObject dummy;
        private static Queue<Tween> cleanup;

        private static void Initialize()
        {
            if (Initialized && dummy != null)
                return;

            Initialized = true;

            activeTweenSequences = new Dictionary<object, List<Tween>>();
            cleanup = new Queue<Tween>();

            if (dummy == null)
                dummy = new GameObject("TweenUpdateManager", typeof(TweenUpdater));

            dummy.hideFlags = HideFlags.HideInInspector | HideFlags.HideInHierarchy;

            GameObject.DontDestroyOnLoad(dummy);
        }

        public static void StartTween(GameObject _target, TweenRuntimeGraph _tween)
        {
            if (!Initialized || dummy == null)
                Initialize();

            TweenRuntimeGraph graph = _tween.Get();

            Tween tween = new Tween($"Action{System.Guid.NewGuid()}", graph.GetSequence());
            tween.action.UpdateTargetRecursive(_target);
            object target = tween.action.GetTarget();


            if (!activeTweenSequences.ContainsKey(target))
                activeTweenSequences.Add(target, new List<Tween>());

            activeTweenSequences[target].Add(tween);

            Debug.Log($"Tween {tween}");

            tween.Play();
        }


        public static void StartTween(TweenAction _tweenAction)
        {
            if(!Initialized)
                Initialize();

            Tween tween = new Tween($"Action{System.Guid.NewGuid()}", _tweenAction);
            object target = _tweenAction.GetTarget();


            if (!activeTweenSequences.ContainsKey(target))
                activeTweenSequences.Add(target, new List<Tween>());

            activeTweenSequences[_tweenAction.GetTarget()].Add(tween);


            tween.Play();
        }

        public static void StopTween(TweenAction _tween)
        {
            if (!Initialized)
                Initialize();

            //List Of Sequences On A GameObject
            foreach (var item in activeTweenSequences[_tween.GetTarget()])
            {
                if (item.action != _tween)
                    continue;

                item.Stop();
                cleanup.Enqueue(item);

                Debug.Log($"Queued cleanup: {item.name}");

                return;
            }
        }

        internal static void UpdateTweens()
        {
            foreach(List<Tween> tweenList in activeTweenSequences.Values)
            {
                foreach (Tween tween in tweenList)
                {
                    tween.Update();
                }
            }

            //Cleanup Queue
            while(cleanup.Count > 0)
            {
                Tween tween = cleanup.Dequeue();

                Debug.Log($"Removing tween: {tween.name}");

                activeTweenSequences[tween.action.GetTarget()].Remove(tween);
            }
        }
    }
}
