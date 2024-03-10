using TDPF.FuckUp.Interactions;
using UnityEngine;

namespace TDPF.FuckUp.Interaction
{
    public class InteractiveTrigger : InteractiveObject
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            { 
                Interact();
            }
        }
    }
}