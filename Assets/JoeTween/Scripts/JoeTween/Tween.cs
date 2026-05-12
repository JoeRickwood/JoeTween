using UnityEngine;

namespace JoeTween
{
    public class Tween
    {
        internal string name;
        internal TweenAction startAction;

        internal Tween(string _name, TweenAction _start)
        {
            name = _name;
            startAction = _start;
        }

        internal void Play(GameObject _target)
        {
            TweenManager.StartTween(_target, this);
        }

        internal void InitializeTween(GameObject _target)
        {
            startAction.Initialize(_target);
        }
    }
}
