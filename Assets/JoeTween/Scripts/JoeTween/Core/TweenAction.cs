using System;
using System.Collections.Generic;
using Unity.GraphToolkit.Editor;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    public struct TweenValue<T>
    {
        public T value;

        [SerializeReference]
        public TweenActionValueModifier<T> valueModifier;

        public T GetValue()
        {
            T tmp = value;

            if(valueModifier != null)
                valueModifier.AlterValue(out tmp);

            return tmp;
        }

        public void LoadMod(IPort _port)
        {
            valueModifier = null;
            List<IPort> connectedPorts = new List<IPort>();
            _port.GetConnectedPorts(connectedPorts);

            if (connectedPorts == null || connectedPorts.Count == 0)
                return;

            IPort port = connectedPorts[0];

            if (port.GetNode() is JoeTweenFunctionNode<T> node)
            {
                valueModifier = node.GetModifier() as TweenActionValueModifier<T>;
            }   
            else
            {
                valueModifier = null;
            }      
        }


        public static implicit operator T(TweenValue<T> tweenValue)
        {
            return tweenValue.GetValue();
        }
    }

    [Serializable]
    public class TweenActionValueModifier
    {
        public TweenActionValueModifier<T> Clone<T>()
        {
            return this.MemberwiseClone() as TweenActionValueModifier<T>;
        }
    }

    [Serializable]
    public abstract class TweenActionValueModifier<T> : TweenActionValueModifier
    {
        public abstract void AlterValue(out T _value);
    }

    [System.Serializable]
    public abstract class TweenAction
    {
        public AnimationCurve tweenCurve;
        public TweenValue<float> animationLength;

        [SerializeReference]
        private List<TweenAction> sequenced;

        protected bool ended;
        protected bool looping;
        protected bool playing;

        public TweenAction(AnimationCurve _animationCurve, float _animationLength, bool _looping)
        {
            tweenCurve = _animationCurve;
            animationLength.value = _animationLength;

            ended = false;
            sequenced = new List<TweenAction>();
            looping = _looping;
            playing = false;
        }

        public virtual void CloneModifiers()
        {
            if(animationLength.valueModifier != null)
                animationLength.valueModifier = animationLength.valueModifier.Clone<float>();
        }

        public TweenAction Clone()
        {
            TweenAction action = this.MemberwiseClone() as TweenAction;

            action.CloneModifiers();

            return action;
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


            Debug.Log($"{sequenced.Count} {this}");
        }

        public abstract object GetTarget();

        public virtual void Update(float _time)
        {
            if (_time <= animationLength || looping)
                return;

            if (!ended)
                EndAction();

            foreach (var t in sequenced)
            {
                
                t.Update(_time - animationLength);
            }      

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