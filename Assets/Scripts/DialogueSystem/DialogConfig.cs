using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TDPF.FuckUp.DialogueSystem
{
    [CreateAssetMenu(menuName = "SO/DialogConfig", fileName = "Config", order = 0)]
    public class DialogConfig : ScriptableObject
    {
        public DialogItem[] Items;
        public DialogueStack[] Stacks;

        public Dictionary<IActivateKey, List<DialogItem>> CollectDialoguesByKeys()
        {
            return Items
                .SelectMany(d => d.Restrictions.Concat(d.ReverseRestrictions)
                    .Select(r => (d, r)))
                .GroupBy(dr => dr.r, dr => dr.d)
                .ToDictionary(g => (IActivateKey)g.Key, g => g.ToList());
        }
    }
}