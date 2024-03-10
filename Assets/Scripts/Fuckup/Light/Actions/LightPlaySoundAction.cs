using System;
using FMODUnity;
using UnityEngine;

namespace TDPF.FuckUp.Interactions
{
    public class LightPlaySoundAction: InteractAction
    {
        [SerializeField] private StudioEventEmitter emitter;
        [SerializeField] private EventReference fallback;
        
        protected override void Process()
        {
            try
            {
                if (emitter != null)
                {
                    emitter.Play();
                }
                else
                {
                    RuntimeManager.PlayOneShotAttached(fallback, gameObject);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}