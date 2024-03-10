using System;
using TDPF.FuckUp.Interactions;
using UnityEngine;
using Zenject;

namespace TDPF.Player.Inventory
{
    public class PickSlotObject : InteractiveObject
    {
        [Inject] private InventoryController _controller;

        [SerializeField] private string takeDescription;
        [SerializeField] private string giveDescription;
        [SerializeField] private GameObject visual;
        [SerializeField] private InventoryItem item;
        [SerializeField] private bool canTakeItem;
        [SerializeField] private bool canGiveItem;
        [SerializeField] private BoolObject itemBool;

        public bool HasItem
        {
            get => itemBool.Value;
            set => itemBool.Value = value;
        }

        public override string Description =>
            CanGiveToPlayer ? takeDescription : CanTakeFromPlayer ? giveDescription  : base.Description;
        public override bool IsAbleToInteract => base.IsAbleToInteract && _controller.Item == item && (CanGiveToPlayer || CanTakeFromPlayer);

        public bool CanGiveToPlayer => HasItem && canGiveItem;
        public bool CanTakeFromPlayer => !HasItem && canTakeItem;

        private void Awake()
        {
            UpdateState();
        }

        protected override void ProcessInteract()
        {
            if (!CanGiveToPlayer || !_controller.TryPut(item))
            {
                if (!CanTakeFromPlayer || !_controller.TryPop(out _))
                {
                    _controller.PlaySound(false);
                    return;
                }
            }
            HasItem = !HasItem;
            _controller.PlaySound(true);
            UpdateState();
            base.ProcessInteract();
        }

        private void UpdateState()
        {
            visual.SetActive(HasItem);
        }
    }
}