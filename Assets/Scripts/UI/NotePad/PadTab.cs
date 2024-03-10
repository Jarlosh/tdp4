using System.Collections.Generic;
using System.Linq;
using TDPF.FuckUp;
using TDPF.Tools;
using UI.Loader;

namespace UI.NotePad
{
    public class PadTab: AutoButton
    {
        public List<NotepadKey> Keys;
        public List<NotepadKey> Resolved;

        public void OnResolved()
        {
            foreach (var key in Keys)
            {
                if (!Resolved.Contains(key) && key.Restrictions.AllPassing())
                {
                    Resolved.Add(key);
                }
            }
        }

        public string[] GetTexts()
        {
            return Resolved.Select(r => r.Text).ToArray();
        }
    }
}