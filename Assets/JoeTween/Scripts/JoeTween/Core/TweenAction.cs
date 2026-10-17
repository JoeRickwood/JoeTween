/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   TweenAction.cs
    Description :   Implementation For Action Tweens In Context Of GameObject-Components
    Author      :   Joe Rickwood
**************************************************************************/

using System;
using System.Collections.Generic;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Tween Value Is Used To Store A Variable Changable By A TweenValueModifier
    /// </summary>
    /// <typeparam name="T"> The value type of the variable </typeparam>
    [Serializable]
    public struct TweenValue<T>
    {
        public T value;

        [SerializeReference]
        public TweenActionValueModifier<T> valueModifier;

        public TweenValue(T _val, TweenActionValueModifier<T> _mod = null) 
        {
            value = _val;
            valueModifier = _mod; //Value Modifier Is Not Needed, However Can Exist
        }

        //Gets The True Value Of This TweenValue
        public T GetValue(float _tweenTime)
        {
            //Create Tmp Variation Of Value
            T tmp = value;

            //If Modifier Exists, Update The Value To Be The Modifiers One Instead
            if(valueModifier != null)
                valueModifier.AlterValue(ref tmp, _tweenTime);

            return tmp;
        }

        /// <summary>
        /// Used For Initializing Values On Tween Objects
        /// </summary>
        /// <param name="_obj">Object The Tween Is Acting On</param>
        public void OnActionStart(GameObject _obj) 
        {
            if (valueModifier != null)
                valueModifier.OnActionStart(_obj);
        }
    }

    [System.Serializable]
    public abstract class TweenAction
    {
        public AnimationCurve tweenCurve; //Tween Curve Used As A Easing Function To Smooth Out The Value Change
        public TweenValue<float> animationLength; //The Length Of Time The Animation Takes To Complete

        [SerializeReference]
        private List<TweenAction> sequenced; //List Of Sequenced Actions To Perform After This Action Complete
        public GameObject target; //Target That The Tween Component Exists On

        //General Tween Action Flow Bools
        protected bool ended;
        protected bool playing;
        protected bool looping;

        public TweenAction(AnimationCurve _animationCurve, float _animationLength, bool _looping)
        {
            tweenCurve = _animationCurve;
            animationLength.value = _animationLength;

            sequenced = new List<TweenAction>();

            ended = false;
            playing = false;

            looping = _looping;
        }

        //Should Be Called Recursively On Derived Actions To Clone All Values 
        public virtual void CloneModifiers()
        {
            if(animationLength.valueModifier != null)
                animationLength.valueModifier = animationLength.valueModifier.Clone<float>();
        }

        public TweenAction Clone()
        {
            TweenAction action = this.MemberwiseClone() as TweenAction;

            //Clones Modifiers Action Creating The Action
            action.CloneModifiers();

            return action;
        }

        //Sets Loop Mode Of The Tween
        public void SetLoopType(WrapMode _mode)
        {
            looping = _mode == WrapMode.Loop || _mode == WrapMode.PingPong;

            tweenCurve.preWrapMode = _mode;
            tweenCurve.postWrapMode = _mode;    
        }

        internal void Restart()
        {
            ended = false;
            playing = false;

            //Restart "Child" Actions
            foreach (var obj in sequenced)
            {
                obj.Restart();
            }
        }

        //Goes Through All Sequenced Actions And This Action And Updates The Target 
        public virtual void UpdateTargetRecursive(GameObject _target)
        {
            target = _target;

            for (int i = 0; i < sequenced.Count; i++)
            {
                sequenced[i].UpdateTargetRecursive(_target);
            }
        }

        //Adds A Sequenced Action To The Current Action
        public void AddSequenced(TweenAction _sequenced)
        {
            sequenced.Add(_sequenced);
        }

        public abstract object GetTarget();


        //This Base Function Should Be Called On Derived Actions Implementing Logic
        public virtual void Update(float _time)
        {
            float tweenTime = GetTweenProgress(_time);

            if (_time <= animationLength.GetValue(tweenTime) || looping)
                return;

            //Logic For If The Timer Is Currently AFTER The Animaton Is Finished
            
            //End Action If Not Ended Yet
            if (!ended)
                EndAction();

            foreach (var t in sequenced)
            {
                t.Update(_time - animationLength.GetValue(tweenTime));
            }      
        }

        //Has This Action Finished
        public bool IsFinished()
        {
            bool value = ended == true;

            if (sequenced == null)
                return value;

            //Recursively Goes Through And Checks If Its Sequenced Actions Are Finished
            foreach (var t in sequenced)
            {
                if (t.IsFinished() == false)
                    return false;
            }

            return value;
        }

        internal virtual void StartAction()
        {
            playing = true;

            animationLength.OnActionStart(target);
        }

        protected void SetPaused(bool _pauseState) 
        {
            playing = !_pauseState;
        }

        public bool GetPaused()
        {
            return !playing;
        }

        /// <summary>
        /// Ends The Action After 
        /// </summary>
        protected virtual void EndAction()
        {
            ended = true;
            playing = false;

            //Starts Each Action After This One Ends
            if (sequenced != null)
            {
                foreach (var t in sequenced)
                {
                    t.StartAction();
                }
            }
        }


        /// <summary>
        /// Stops The Action And All Sequenced Actions
        /// </summary>
        internal void StopAction()
        {
            if (sequenced != null)
            {
                foreach (var t in sequenced)
                {
                    t.StopAction();
                }
            }
        }

        internal float GetTweenProgress(float _time)
        {
            return tweenCurve.Evaluate(_time / animationLength.value);
        }
    }

    /// <summary>
    /// Typed Tween Action
    /// </summary>
    /// <typeparam name="T"> Type Of Component To Tween </typeparam>
    [System.Serializable]
    public abstract class TweenAction<T> : TweenAction
    {
        [SerializeReference]
        public T component;

        public TweenAction(AnimationCurve _animationCurve, float _animationLength, T _component, bool _looping) 
            : base(_animationCurve, _animationLength, _looping)
        {
            component = _component;
        }

        public override object GetTarget()
        {
            return component;
        }

        public override void UpdateTargetRecursive(GameObject _target)
        {
            //Resets The Gathered Component
            component = _target.GetComponent<T>();

            base.UpdateTargetRecursive(_target);
        }
    }
}