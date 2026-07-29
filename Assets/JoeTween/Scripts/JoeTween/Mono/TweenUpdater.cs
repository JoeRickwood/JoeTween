using UnityEngine;

namespace JoeTween
{
    /// <summary>
    /// This Is Used To Perform The Update Function On The Tween Manager Class
    /// This Class Should Not Be Deleted From A Scene Once Instantiated
    /// </summary>
    public class TweenUpdater : MonoBehaviour
    {
        private void Update()
        {
 
            TweenManager.UpdateTweens();
        }
    }
}
