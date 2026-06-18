/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   TweenActionModifier.cs
    Description :   ActionModifiers Are Used To Generate Values To Alter The Value On A TweenValue
                    Examples Could Include Random Number Generation, Object Start Position And Math 
                    Sequence Nodes

    Author      :   Joe Rickwood
**************************************************************************/

using System;
using UnityEngine;


namespace JoeTween
{
    /// <summary>
    /// Base Abstract Modifier Class To Be Overriden By Modifiers
    /// </summary>
    [Serializable]
    public abstract class TweenActionValueModifier
    {
        // Clones The Base Value Modifier And Casts It To The Type Cloned
        // We Clone The Modifier When We Create A TweenActionInstance
        // Should Be Overriden When New Modifier Behaviour Is Created, 
        public virtual TweenActionValueModifier<T> Clone<T>()
        {
            return this.MemberwiseClone() as TweenActionValueModifier<T>;
        }
    }

    /// <summary>
    /// A Typed Value Modifier Used When
    /// </summary>
    /// <typeparam name="T"> Type Of Value Altered By This Value Modifier </typeparam>
    [Serializable]
    public abstract class TweenActionValueModifier<T> : TweenActionValueModifier
    {
        public abstract void OnActionStart(GameObject _componentHolder);
        public abstract void AlterValue(out T _value);
    }
}