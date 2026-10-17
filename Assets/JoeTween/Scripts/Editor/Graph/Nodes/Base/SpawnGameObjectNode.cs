using System;
using UnityEngine;

namespace JoeTween
{
    [Serializable]
    class SpawnGameObjectNode : TweenActionNode<UnityEngine.Transform>
    {
        public float lifetime;
        public TweenSpace space;
        public GameObject gameobjectPrefab;
        public Vector3 spawnPos;

        protected override void OnDefinePorts(IPortDefinitionContext context)
        {
            base.OnDefinePorts(context);

            context.AddInputPort<TweenSpace>("Space").Build();
            context.AddInputPort<Vector3>("Spawn Position").Build();
            context.AddInputPort<GameObject>("Spawned GameObject").Build();
            context.AddInputPort<float>("Lifetime").Build(); // -1 For Infinite Lifetime
        }

        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            SpawnGameObjectTween tween = new SpawnGameObjectTween
            (
                animationCurve, tweenLength, false,
                null, lifetime, space, spawnPos, gameobjectPrefab
            );

            TweenSequenceNodeExtensions.LoadMod(ref tween.spawnPos, GetInputPortByName("Spawn Position"));

            return tween;
        }

        public override void LoadValues()
        {
            base.LoadValues();

            GetInputPortByName("Space").TryGetValue(out space);
            GetInputPortByName("Spawn Position").TryGetValue(out spawnPos);
            GetInputPortByName("Spawned GameObject").TryGetValue(out gameobjectPrefab);
            GetInputPortByName("Lifetime").TryGetValue(out lifetime);
        }
    }
}
