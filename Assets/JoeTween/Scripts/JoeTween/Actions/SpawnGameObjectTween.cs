/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   SpawnGameObjectTween.cs
    Description :   Spwans A GameObject On The Start Of The Tween At A Position In The World
    Author      :   Joe Rickwood
**************************************************************************/


using System;
using UnityEngine;
using UnityEngine.VFX;

namespace JoeTween
{
    /// <summary>
    /// Moved A Target Transform Component Over Two Position Vectors
    /// </summary>
    [System.Serializable]
    public class SpawnGameObjectTween : TweenAction<Transform>
    {
        public float lifetime;
        public TweenSpace space;
        [SerializeReference] public GameObject gameObjectPrefab;
        public TweenValue<Vector3> spawnPos;

        public SpawnGameObjectTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            Transform _component, float _lifetime, TweenSpace _space, Vector3 _spawnPos, GameObject _gameObjectPrefab
        ) : base(_animationCurve, _animationLength,_component, _looping)
        {
            lifetime = _lifetime;
            space = _space;
            gameObjectPrefab = _gameObjectPrefab;
            spawnPos.value = _spawnPos;
        }

        public override void CloneModifiers()
        {
            base.CloneModifiers();

            if (spawnPos.valueModifier != null)
                spawnPos.valueModifier = spawnPos.valueModifier.Clone<Vector3>();
        }

        internal override void StartAction()
        {
            spawnPos.OnActionStart(target);

            if (gameObjectPrefab == null)
                return;

            Vector3 position = space == TweenSpace.LOCAL ? target.transform.position + spawnPos.GetValue(0.0f) : spawnPos.GetValue(0.0f);

            GameObject cur = GameObject.Instantiate(gameObjectPrefab, position, Quaternion.identity);
            var visualEffect = cur.GetComponent<VisualEffect>();

            if (lifetime !< 0)
                GameObject.Destroy(cur, lifetime);

            if (visualEffect != null)
                visualEffect?.Play();

            base.StartAction();
        }

        protected override void EndAction()
        {
            base.EndAction();
        }

        public override void Update(float _time)
        {
            base.Update(_time);
        }
    }
}
