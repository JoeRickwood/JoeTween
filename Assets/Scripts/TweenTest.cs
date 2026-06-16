using UnityEngine;
using JoeTween;

public class TweenTest : MonoBehaviour
{
    public TweenRuntimeGraph tween;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
            TweenManager.StartTween(gameObject, tween);
    }
}
