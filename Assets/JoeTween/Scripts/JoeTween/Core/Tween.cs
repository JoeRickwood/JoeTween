/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   Tween.cs
    Description :   Implementation For Tween Data Structure Class To Be Played On The TweenManager
    Author      :   Joe Rickwood
**************************************************************************/

using UnityEngine;
using UnityEngine.Events;

namespace JoeTween
{
    public sealed class Tween
    {
        public string name;
        public TweenAction action;
        private float tweenTime;
        public GameObject target;
        public bool isPaused;


        public UnityEvent<GameObject> onTweenPlay;
        public UnityEvent<GameObject> onTweenStop;
        public UnityEvent<GameObject> onTweenComplete;

        internal Tween(string _name, TweenAction _start)
        {
            name = _name;
            action = _start;
            tweenTime = 0;

            onTweenPlay = new UnityEvent<GameObject>();
            onTweenStop = new UnityEvent<GameObject>();
            onTweenComplete = new UnityEvent<GameObject>();
        }

        internal void Update()
        {
            tweenTime += Time.deltaTime; //Iterate On Tween Time

            action.Update(tweenTime); //Update The Action With The Current Tween Time

            if (action.IsFinished())
            {
                TweenManager.StopTween(this);
                onTweenComplete?.Invoke(target);
            }
        }

        internal void Play()
        {
            isPaused = false;
            onTweenPlay?.Invoke(target);
        }

        internal void Pause()
        {
            isPaused = true;
            onTweenStop?.Invoke(target);
        }

        internal void Stop()
        {
            action.StopAction();
        }
    }
}
