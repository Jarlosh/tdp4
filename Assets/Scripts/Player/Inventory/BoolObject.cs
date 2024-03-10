using UnityEngine;

namespace TDPF.Player.Inventory
{
    public class BoolObject: MonoBehaviour
    {
        [field: SerializeField] public bool Value { get; set; }
    }
}