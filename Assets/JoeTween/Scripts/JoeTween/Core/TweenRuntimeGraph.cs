/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   TweenRuntimeGraph.cs
    Description :   The Tween Runtime Graph Provides A Data Structure Used To Construct
                    The Tween Sequence From, Passed Into The Tween Manager To Build And Run
                    Or Call Play On This Object To Play On The Tween Manager
    Author      :   Joe Rickwood
**************************************************************************/

using System.Collections.Generic;
using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// Runtime data used to build the sequence of actions
    /// </summary>
    [System.Serializable]
    public class TweenRuntimeData
    {
        public int[] sequencedActions;

        public TweenRuntimeData()
        {
            sequencedActions = new int[0];
        }
    }

    public class TweenRuntimeGraph : ScriptableObject
    {
        [SerializeReference]
        public TweenAction[] actions; //List Of Actions To Utilize With The Runtime Data To Build The Sequence

        [SerializeReference]
        public TweenRuntimeData[] actionData; //Data To Build The Sequence From
        public int entranceIndex; //Index Of Action To Start From, Usually Set To The Start Node When Building This Data Object

        /// <summary>
        /// Plays The Tween On A Target On The TweenManager 
        /// </summary>
        /// <param name="_target"></param>
        public void Play(GameObject _target)
        {
            TweenManager.StartTween(_target, this);
        }

        internal TweenRuntimeGraph Get()
        {
            TweenRuntimeGraph graph = Instantiate(this);

            return graph;
        }

        internal TweenAction GetSequence()
        {
            //Clone List First
            List<TweenAction> cloned = new List<TweenAction>();

            for (int i = 0; i < actions.Length; i++)
            {
                if (actions[i] == null)
                    continue;

                cloned.Add(actions[i].Clone());
            }

            for (int i = 0; i < cloned.Count; i++)
            {
                int[] sequenced = actionData[i].sequencedActions;

                for (int j = 0; j < sequenced.Length; j++)
                {
                    //If Less Than Zero, Dont Add Action To Sequence List
                    if(sequenced[j] < 0)
                        continue;

                    cloned[i].AddSequenced(cloned[sequenced[j]]);
                }
            }

            return cloned[entranceIndex];
        }

        internal int GetIndex(TweenAction _action)
        {
            for (int i = 0; i < actions.Length; i++)
            {
                if (actions[i] == _action)
                    return i;
            }

            return -1;
        }
    }
}
