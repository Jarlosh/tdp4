using UnityEngine;

namespace TDPF.FuckUp.DialogueSystem
{
    public interface IActivateKey
    {
        bool Active { get; }
        void Activate(GameObject activator);
        void Deactivate();
    }
}