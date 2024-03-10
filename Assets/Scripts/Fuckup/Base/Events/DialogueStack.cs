using System;
using System.Collections.Generic;
using FMODUnity;
using UnityEngine;

namespace TDPF.FuckUp.DialogueSystem
{
    public enum SoundStrategy
    {
        Never, After, SameTime
    }
    
    [Serializable]
    public class ItemSettings
    {
        [field: SerializeField] public SoundStrategy Strategy { get; private set; } = SoundStrategy.Never;
        [field: SerializeField] public EventReference SoundEvent { get; private set; }
        [field: SerializeField] public bool SilenceVoice { get; private set; }
    }
    
    [CreateAssetMenu(menuName = "SO/DialogueStack", fileName = "DialogueStack", order = 0)]
    public class DialogueStack : ClueKey, IDialogueNode
    {
        [Serializable]
        public class Item: IDialogueItem
        {
            public Item(CharacterItem dCharacter, string dText)
            {
                Character = dCharacter;
                Text = dText;
            }

            [field: SerializeField] 
            public ItemSettings Settings { get; private set; }

            [field: SerializeField]
            public CharacterItem Character { get; private set; }

            [field: SerializeField, TextArea]
            public string Text { get; private set; }
        }

        [field: SerializeField] 
        public ClueRestriction Restrictions { get; private set; }

        [field: SerializeField] 
        public List<Item> Items { get; private set; }

        public IEnumerable<IDialogueItem> GetItems() => Items;
    }
}