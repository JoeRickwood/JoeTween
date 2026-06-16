using System.Collections.Generic;
using UnityEngine;

namespace JoeTween
{
    public class TweenRuntimeGraph : ScriptableObject
    {
        public int entranceIndex;

        [SerializeReference]
        public List<TweenAction> actions;

        [SerializeReference]
        public List<int> strides;

        [SerializeReference]
        public List<int> indexSequences;

        public TweenRuntimeGraph() 
        {
            actions = new List<TweenAction>();
            indexSequences = new List<int>();
            strides = new List<int>();
        }

        public TweenRuntimeGraph Get()
        {
            TweenRuntimeGraph graph = Instantiate(this);

            return graph;
        }

        public TweenAction GetSequence()
        {
            //Clone List First
            List<TweenAction> cloned = new List<TweenAction>();
            foreach (var action in actions)
            {
                cloned.Add(action.Clone());
            }

            int startIndex = 0;
            for (int i = 0; i < cloned.Count; i++)
            {
                List<int> indices = indexSequences.GetRange(startIndex, strides[i]);

                for (int j = 0; j < indices.Count; j++)
                {
                    if(indices[j] < 0)
                        continue;

                    cloned[i].AddSequenced(cloned[indices[j]].Clone());
                }  

                startIndex += strides[i];
            }

            return cloned[entranceIndex];
        }

        public int GetIndex(TweenAction _action)
        {
            for (int i = 0; i < actions.Count; i++)
            {
                if (actions[i] == _action)
                    return i;
            }

            return -1;
        }
    }
}
