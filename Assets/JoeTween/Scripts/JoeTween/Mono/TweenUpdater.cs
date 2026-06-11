using UnityEngine;

namespace JoeTween
{
    public class TweenUpdater : MonoBehaviour
    {
        private void Update()
        {
            TweenManager.UpdateTweens();
        }
    }
}
