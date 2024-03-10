using UnityEngine;

namespace TDPF.FuckUp.DialogueSystem
{
    [CreateAssetMenu(menuName = "SO/CharacterItem", fileName = "CharacterItem", order = 0)]
    public class CharacterItem: ScriptableObject
    {
        [field: SerializeField]
        public string Name { get; private set; }

        [field: SerializeField]
        public Color Color { get; private set; }
    }
}