using UnityEngine;
using JoeTween;

public class TweenTest : MonoBehaviour
{

    private void Start()
    {
        AnimationCurve moveCurve = EasingFunctions.EaseOut();

        TweenAction<Transform> tween = new MoveTween
        (
            moveCurve, 1f, false,
            transform, Vector3.zero, new Vector3(5, 5, 0)
        );

        tween.SetLoopType(WrapMode.Default);

        TweenManager.StartTween(tween);
    }
}
