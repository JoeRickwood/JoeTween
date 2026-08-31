using JoeTween;
using UnityEngine;

public class TweenPlayer : MonoBehaviour
{
    public TweenRuntimeGraph tween;

    private void Start()
    {
        tween.Play(gameObject);
    }
}
