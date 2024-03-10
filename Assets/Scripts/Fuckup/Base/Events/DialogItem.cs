using System.Collections.Generic;
using TDPF.TimeSwitch;
using UnityEngine;

namespace TDPF.FuckUp.DialogueSystem
{
    [CreateAssetMenu(menuName = "SO/DialogItem", fileName = "DialogItem", order = 0)]
    public class DialogItem : ClueKey, IDialogueNode, IDialogueItem, IWithClueRestriction
    {
        [field: SerializeField] public ClueKey[] Restrictions { get; private set; }
        [field: SerializeField] public ClueKey[] ReverseRestrictions { get; private set; }
        [field: SerializeField] public CharacterItem Character { get; private set; }
        [field: SerializeField, TextArea] public string Text { get; private set; }
        [field: SerializeField] public TimeMoment TimeMoment { get; private set; } = TimeMoment.Future | TimeMoment.Past;
        public ItemSettings Settings => null;

        IList<ClueKey> IWithClueRestriction.Keys => Restrictions;
        IList<ClueKey> IWithClueRestriction.NotKeys => ReverseRestrictions;

        public IEnumerable<IDialogueItem> GetItems()
        {
            yield return this;
        }
        
        const string NotAssigned = "N/A";
        
        public override string ToString()
        {
            var characterName = Character == null ? NotAssigned : Character.Name;
            return $"[{characterName}: {Text}]";
        }
    }
}
