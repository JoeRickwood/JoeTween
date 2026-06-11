using UnityEngine;

namespace JoeTween
{
    public class Tween
    {
        internal string name;
        internal TweenAction action;
        internal float tweenTime;
        internal GameObject target;

        internal Tween(string _name, TweenAction _start)
        {
            name = _name;
            action = _start;
            tweenTime = 0;
        }

        internal void Update()
        {
            tweenTime += Time.deltaTime;
            action.Update(tweenTime);
        }

        internal void Play()
        {
            action.SetPaused(false);
        }

        internal void Pause()
        {
            action.SetPaused(true);
        }

        internal void Stop()
        {
            action.StopAction();
        }
    }
}
