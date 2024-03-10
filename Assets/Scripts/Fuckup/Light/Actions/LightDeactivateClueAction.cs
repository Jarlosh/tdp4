using UnityEngine;
using Zenject;

namespace TDPF.FuckUp.Interactions
{
    public class LightDeactivateClueAction: InteractAction
    {
        [Inject] private FuckUpController _fuckUpController;

        [SerializeField] private ClueKey toDeactivate;
        [SerializeField] private bool fireOnce = true;
        private bool _used;

        protected override void Awake()
        {
            base.Awake();
            _used = false;
        }

        protected override void Process()
        {
            if ((!fireOnce || !_used) && !toDeactivate.Active)
            {
                _fuckUpController.Deactivate(toDeactivate);
                _used = true;
            }
        }
    }
}