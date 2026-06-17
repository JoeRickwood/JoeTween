using System.Collections.Generic;
using UnityEngine;

namespace JoeTween
{
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
        public TweenAction[] actions;

        [SerializeReference]
        public TweenRuntimeData[] actionData;
        public int entranceIndex;

        public TweenRuntimeGraph Get()
        {
            TweenRuntimeGraph graph = Instantiate(this);

            return graph;
        }

        public TweenAction GetSequence()
        {
            //Clone List First
            List<TweenAction> cloned = new List<TweenAction>();

            for (int i = 0; i < actions.Length; i++)
            {
                Debug.Log(actions[i]);

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

        public int GetIndex(TweenAction _action)
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
