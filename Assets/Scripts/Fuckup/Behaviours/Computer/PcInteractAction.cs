using TDPF.FuckUp.Interactions;
using UnityEngine;

namespace TDPF.FuckUp
{
    public class PcInteractAction : InteractAction
    {
        [SerializeField] private ComputerController.PcAction pcAction;
        [SerializeField] private ComputerController controller;

        protected override void Process()
        {
            controller.Interact(pcAction);
        }
    }
}