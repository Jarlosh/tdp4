using UnityEngine;

namespace TDPF.Triggers
{
    public class SoundTrigger : Trigger
    {
        [SerializeReference] private FMODUnity.StudioEventEmitter soundToTrigger;
        [SerializeField] private bool singleShot = false;

        protected override void Process()
        {
            if (singleShot && isTriggered) {
                return;
            }

            isTriggered = true;
            soundToTrigger.Play();
        }

        private bool isTriggered = false;
    }
}