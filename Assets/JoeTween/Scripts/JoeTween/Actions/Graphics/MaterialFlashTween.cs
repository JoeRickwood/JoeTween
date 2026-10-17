/***********************************************************************
    Auckland
    New Zealand

    (c) 2026 Joe Rickwood

    File Name   :   MaterialFlashTween.cs
    Description :   Tweens That Make A Object Flash A Material
    Author      :   Joe Rickwood
**************************************************************************/


using UnityEngine;
using UnityEngine.UI;

namespace JoeTween
{

    /// <summary>
    /// Should Be Used On Sprite Objects
    /// </summary>
    [System.Serializable]
    public class MaterialFlashTween : TweenAction<MeshFilter>
    {
        public Material materialChange;

        public MaterialFlashTween(
            AnimationCurve _animationCurve, float _animationLength, bool _looping,
            MeshFilter _component, Material _materialChange
        ) : base(_animationCurve, _animationLength, _component, _looping)
        {
            materialChange = _materialChange;
        }

        internal override void StartAction()
        {
            base.StartAction();
        }

        public override void CloneModifiers()
        {
            base.CloneModifiers();
        }

        protected override void EndAction()
        {
            base.EndAction();
        }

        public override void Update(float _time)
        {
            base.Update(_time);

            if (playing && component != null)
            {
                Matrix4x4 matrix = Matrix4x4.TRS(component.transform.position, component.transform.rotation, component.transform.lossyScale * 1.01f);

                MaterialPropertyBlock block = new MaterialPropertyBlock();
                block.SetFloat("_Alpha", GetTweenProgress(_time));

                Graphics.DrawMesh(component.mesh, matrix, materialChange, 0, null, 0, block);
            }
        }
    }
}
