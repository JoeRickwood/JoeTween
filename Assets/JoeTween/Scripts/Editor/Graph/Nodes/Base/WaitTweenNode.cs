using System;


namespace JoeTween
{
    [Serializable]
    class WaitTweenNode : TweenActionNode<UnityEngine.Transform>
    {
        public override TweenAction GetTweenAction(TweenSequenceData _data)
        {
            LoadValues();

            BlankTween tween = new BlankTween
            (
                animationCurve, tweenLength, false, component        
            );

            return tween;
        }

        public override void LoadValues()
        {
            base.LoadValues();
        }
    }
}
