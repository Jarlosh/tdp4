using System;
using FMODUnity;
using UnityEngine;

namespace TDPF.FuckUp.Interactions
{
    public class SoundInteractAction : InteractAction
    {
        [SerializeField] protected StudioEventEmitter eventEmitter;
        [SerializeField] protected EventReference reference;
        
        protected override void Process()
        {
            try
            {
                if (eventEmitter != null)
                {
                    eventEmitter.Play();
                }
                else if (!reference.IsNull)
                {
                    RuntimeManager.PlayOneShot(reference);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}