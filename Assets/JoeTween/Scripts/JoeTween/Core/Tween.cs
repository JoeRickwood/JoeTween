/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   Tween.cs
    Description :   Implementation For Tween Data Structure Class To Be Played On The TweenManager
    Author      :   Joe Rickwood
**************************************************************************/

using UnityEngine;

namespace JoeTween
{
    public sealed class Tween
    {
        public string name;
        public TweenAction action;
        private float tweenTime;
        public GameObject target;
        public bool isPaused;

        internal Tween(string _name, TweenAction _start)
        {
            name = _name;
            action = _start;
            tweenTime = 0;
        }

        internal void Update()
        {
            tweenTime += Time.deltaTime; //Iterate On Tween Time

            action.Update(tweenTime); //Update The Action With The Current Tween Time
        }

        internal void Play()
        {
            isPaused = false;
        }

        internal void Pause()
        {
            isPaused = true;
        }

        internal void Stop()
        {
            action.StopAction();
        }
    }
}
