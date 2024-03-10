using TDPF.FuckUp.DialogueSystem;
using Utils;
using Zenject;

namespace TDPF.Player.Inventory
{
    public class InventoryController
    {
        [Inject] private SoundsConfig _soundsConfig;

        public IActivateKey Key => _soundsConfig.InventoryKey;
        public InventoryItem Item { get; private set; }
        public bool HasItem => Item != null;

        public void PlaySound(bool isSuccess)
        {
            _soundsConfig.Play(isSuccess ? _soundsConfig.ActivateSound : _soundsConfig.FailActivateSound);
        }
        
        public bool TryPut(InventoryItem item)
        {
            if (item == Item)
            {
                return false;
            }
            Item = item;
            if (HasItem)
            {
                Key.Activate(null);
            }
            else
            {
                Key.Deactivate();
            }
            return true;
        }

        public bool TryPop(out InventoryItem item)
        {
            item = Item;
            return TryPut(null);
        }
    }
}