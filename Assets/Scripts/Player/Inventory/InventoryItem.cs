using TDPF.FuckUp;
using UnityEngine;

namespace TDPF.Player.Inventory
{
    [CreateAssetMenu(menuName = "SO/InventoryItem", fileName = "InventoryItem", order = 0)]
    public class InventoryItem : ClueKey
    {
        [field: SerializeField] public GameObject Prefab { get; set; }
    }
}