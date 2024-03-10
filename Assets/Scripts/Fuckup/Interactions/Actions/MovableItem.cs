using FMODUnity;
using UI.Loader;
using UnityEngine;

namespace TDPF.FuckUp.Interactions
{
    public class MovableItem : InteractAction
    {
        public TweenObj tweenObj;

        [SerializeField] private StudioEventEmitter dragSound;

        protected override void Process()
        {
            if (tweenObj.TryToggle() && dragSound != null)
            {
                dragSound.Play();
            }
        }
    }
}