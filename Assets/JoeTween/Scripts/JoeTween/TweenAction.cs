using System;
using Unity.VisualScripting.YamlDotNet.Core.Events;
using UnityEngine;

namespace JoeTween
{
    public abstract class TweenAction
    {
        public AnimationCurve tweenCurve;
        public float animationLength = 1.0f;

        private TweenAction[] sequenced;

        protected bool ended;
        protected bool looping;
        protected bool playing;

        public TweenAction(AnimationCurve _animationCurve, float _animationLength, bool _looping)
        {
            tweenCurve = _animationCurve;
            animationLength = _animationLength;

            ended = false;
            sequenced = new TweenAction[0];
            looping = _looping;
            playing = false;
        }

        public void SetLoopType(WrapMode _mode)
        {
            looping = _mode == WrapMode.Loop || _mode == WrapMode.PingPong;

            tweenCurve.preWrapMode = _mode;
            tweenCurve.postWrapMode = _mode;    
        }

        public abstract object GetTarget();

        public virtual void Update(float _time)
        {
            if (_time > animationLength && looping == false)
            {
                if (IsFinished())
                    TweenManager.StopTween(this);

                if (ended == false)
                    EndAction();

                foreach (var t in sequenced)
                {
                    t.Update(_time - animationLength);
                }
            }
        }

        public bool IsFinished()
        {
            bool value = ended == true;

            if (sequenced == null)
                return value;

            foreach (var t in sequenced)
            {
                if (t.IsFinished() == false)
                    return false;
            }

            return value;
        }

        public virtual void StartAction()
        {
            playing = true;
        }

        public void SetPaused(bool _pauseState) 
        {
            playing = !_pauseState;
        }

        public bool GetPaused()
        {
            return !playing;
        }

        public virtual void EndAction()
        {
            ended = true;
            playing = false;

            if (sequenced != null)
            {
                foreach (var t in sequenced)
                {
                    t.StartAction();
                }
            }
        }

        internal void StopAction()
        {
            
        }


        internal float GetTweenProgress(float _time)
        {
            return tweenCurve.Evaluate(_time / animationLength);
        }
    }

    public abstract class TweenAction<T> : TweenAction
    {
        public T component;

        public TweenAction(AnimationCurve _animationCurve, float _animationLength, T _component, bool _looping) : base(_animationCurve, _animationLength, _looping)
        {
            component = _component;
        }

        public override object GetTarget()
        {
            return component;
        }
    }
}