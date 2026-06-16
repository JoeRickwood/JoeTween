using System.Collections.Generic;
using UnityEngine;

namespace JoeTween
{
    [System.Serializable]
    public abstract class TweenAction
    {
        public AnimationCurve tweenCurve;
        public float animationLength = 1.0f;

        [SerializeReference]
        private List<TweenAction> sequenced;

        protected bool ended;
        protected bool looping;
        protected bool playing;

        public TweenAction(AnimationCurve _animationCurve, float _animationLength, bool _looping)
        {
            tweenCurve = _animationCurve;
            animationLength = _animationLength;

            ended = false;
            sequenced = new List<TweenAction>();
            looping = _looping;
            playing = false;
        }

        public TweenAction Clone()
        {
            return this.MemberwiseClone() as TweenAction;
        }


        public void SetLoopType(WrapMode _mode)
        {
            looping = _mode == WrapMode.Loop || _mode == WrapMode.PingPong;

            tweenCurve.preWrapMode = _mode;
            tweenCurve.postWrapMode = _mode;    
        }

        public virtual void UpdateTargetRecursive(GameObject _target)
        {
            for (int i = 0; i < sequenced.Count; i++)
            {
                sequenced[i].UpdateTargetRecursive(_target);
            }
        }

        public void AddSequenced(TweenAction _sequenced)
        {
            sequenced.Add(_sequenced);
        }

        public abstract object GetTarget();

        public virtual void Update(float time)
        {
            if (time <= animationLength || looping)
                return;

            if (!ended)
                EndAction();

            foreach (var t in sequenced)
                t.Update(time - animationLength);

            if (IsFinished())
                TweenManager.StopTween(this);
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

    [System.Serializable]
    public abstract class TweenAction<T> : TweenAction
    {
        [SerializeReference]
        public T component;

        public TweenAction(AnimationCurve _animationCurve, float _animationLength, T _component, bool _looping) : base(_animationCurve, _animationLength, _looping)
        {
            component = _component;
        }

        public override object GetTarget()
        {
            return component;
        }

        public override void UpdateTargetRecursive(GameObject _target)
        {
            component = _target.GetComponent<T>();

            base.UpdateTargetRecursive(_target);
        }
    }
}