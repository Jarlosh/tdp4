using TDPF.Tools;
using UnityEngine;
using Zenject;

namespace TDPF.FuckUp.Interactions
{
    public class ActivateEventAction : InteractAction
    {
        [Inject] private FuckUpController _fuckUpController;

        [SerializeField] private ClueKey toActivate;
        [SerializeField] private bool fireOnce = true;
        [SerializeField] private ClueKey[] restrictions;
        private bool _used;

        protected override void Awake()
        {
            base.Awake();
            _used = false;
        }

        protected override void Process()
        {
            if ((!fireOnce || !_used) && restrictions.AllPassing())
            {
                _fuckUpController.Activate(toActivate, gameObject);
                _used = true;
            }
        }
    }
}