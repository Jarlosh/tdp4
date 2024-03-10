using TDPF.FuckUp;
using UnityEngine;

namespace UI.Loader
{
    [CreateAssetMenu(menuName = "SO/NotepadKey", fileName = "NotepadKey", order = 0)]
    public class NotepadKey: ScriptableObject
    {
        public ClueKey[] Restrictions;
        public string Text;
    }
}