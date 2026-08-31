/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   JoeTween.cs
    Description :   Injection-Point For JoeTween Into A Regular Project, Allowing A User
                    To Start Tweens On GameObjects Using The Static TweenManager Static Class
    Author      :   Joe Rickwood
**************************************************************************/


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

        /// <summary>
        /// Initializes The Tween Manager
        /// Dosent Need To Be Called By Any External Scripts, Tween Manager Will Handle All Calls 
        /// </summary>
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

        /// <summary>
        /// Main StartTween Function, Allows For A TweenRuntimeGraph To Be Instanced 
        /// And Played On A Input GameObject _target
        /// </summary>
        /// <param name="_target">Target To Apply The Tween Instance To</param>
        /// <param name="_tween">Selected Tween To Apply To The Target</param>
        public static Tween StartTween(GameObject _target, TweenRuntimeGraph _tween)
        {
            if (!Initialized || dummy == null)
                Initialize();

            TweenRuntimeGraph graph = _tween.Get();

            Tween tween = new Tween($"Action{System.Guid.NewGuid()}", graph.GetSequence());
            tween.action.UpdateTargetRecursive(_target);
            tween.infiniteLoop = _tween.loopCount == -1;
            tween.loopCount = _tween.loopCount;

            object target = tween.action.GetTarget();


            if (!activeTweenSequences.ContainsKey(target))
                activeTweenSequences.Add(target, new List<Tween>());

            activeTweenSequences[target].Add(tween);

            tween.Play();
            return tween;
        }

        /// <summary>
        /// Used For More Manual Creation Of The Tween On A Object
        /// Tween Action Should Be Created Before-Hand And Target Updated Pre-Emptively
        /// Good For Storing Custom, Procedurally Generated Tweens Then Applying
        /// </summary>
        /// <param name="_tweenAction">Tween Action Sequence To Run</param>
        public static Tween StartTween(TweenAction _tweenAction)
        {
            if(!Initialized)
                Initialize();

            Tween tween = new Tween($"Action{System.Guid.NewGuid()}", _tweenAction);
            object target = _tweenAction.GetTarget();


            if (!activeTweenSequences.ContainsKey(target))
                activeTweenSequences.Add(target, new List<Tween>());

            activeTweenSequences[_tweenAction.GetTarget()].Add(tween);


            tween.Play();
            return tween;
        }

        /// <summary>
        /// Stops A Tween Currently Running, Tween Can Be Cached When Calling StartTween()
        /// </summary>
        /// <param name="_tween"> Tween To Execute The Stop Funtion </param>
        public static void StopTween(Tween _tween)
        {
            if (!Initialized)
                Initialize();

            //List Of Sequences On A GameObject

            if (!activeTweenSequences.ContainsKey(_tween.action.GetTarget()))
                return;

            foreach (var item in activeTweenSequences[_tween.action.GetTarget()])
            {
                if (item != _tween)
                    continue;

                item.Stop();
                cleanup.Enqueue(item);

                return;
            }
        }

        /// <summary>
        /// Stops A Tween Currently Running, This Function Is Usually 
        /// Used Internally However Has Function Outside Of Internal Use
        /// </summary>
        /// <param name="_tween"></param>
        public static void StopTween(TweenAction _tween)
        {
            if (!Initialized)
                Initialize();

            //List Of Sequences On A GameObject

            if (!activeTweenSequences.ContainsKey(_tween.GetTarget()))
                return;

            foreach (var item in activeTweenSequences[_tween.GetTarget()])
            {
                if (item.action != _tween)
                    continue;

                item.Stop();
                cleanup.Enqueue(item);

                return;
            }
        }

        /// <summary>
        /// Runs The Update Function On All Active Tween Sequences
        /// Used Internally By The Tween Updater
        /// </summary>
        internal static void UpdateTweens()
        {
            if (activeTweenSequences == null)
                return;

            foreach(List<Tween> tweenList in activeTweenSequences.Values)
            {
                if (tweenList == null)
                    continue;

                foreach (Tween tween in tweenList)
                {
                    if (tween == null)
                        continue;

                    tween.Update();
                }
            }

            //Cleanup Queue
            while(cleanup.Count > 0)
            {
                Tween tween = cleanup.Dequeue();

                activeTweenSequences[tween.action.GetTarget()].Remove(tween);
            }
        }
    }
}
