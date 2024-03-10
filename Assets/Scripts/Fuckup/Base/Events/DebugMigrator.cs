using Zenject;

namespace TDPF.FuckUp.DialogueSystem
{
    #if UNITY_EDITOR
    // ReSharper disable once RedundantUsingDirective
    using UnityEditor;
    #endif
    public class DebugMigrator: MonoInstaller
    {
        // public List<DialogItem> Dialogs;
        // public List<DialogueStack> Stacks;
        //
        // [ContextMenu("Honk")]
        // void Honk()
        // {
        //     var regex = new Regex("[0-9]+", RegexOptions.IgnoreCase);
        //     var stackNames = Stacks.ToDictionary(s => int.Parse(regex.Match(s.name).Value));
        //     foreach (var (k, v) in stackNames)
        //     {
        //         v.Items.Clear();
        //     }
        //
        //     foreach (var dialog in Dialogs)
        //     {
        //         var result = regex.Match(dialog.name).Value;
        //         if (!int.TryParse(result, out var index))
        //         {
        //             Debug.LogError(dialog.name);
        //         }
        //         else if(stackNames.TryGetValue(index, out var stack))
        //         {
        //             stack.Items.Add(new DialogueStack.Item(dialog.Character, dialog.Text));
        //         }
        //         else
        //         {
        //             Debug.LogError($"no {index}");
        //         }
        //     }
        //     foreach (var (k, v) in stackNames)
        //     {
        //         SetODirty(v);
        //     }
        //
        //     void SetODirty(Object o)
        //     {
        //         #if UNITY_EDITOR
        //         EditorUtility.SetDirty(o);
        //         #endif
        //     }
        // }

        // [ContextMenu("Honk")]
        // void Honk()
        // {
        // foreach (var name in stackNames)
        // {
        //     Debug.Log(name.Key);
        // }
        // foreach (var dialog in Dialogs)
        // {
        //     var c = Characters.FirstOrDefault(c => c.Name == dialog.Speaker);
        //     if (c != null)
        //     {
        //         dialog.Character = c;
        //         EditorUtility.SetDirty(dialog);
        //         EditorUtility.SetDirty(c);
        //     }
        // }
    }
}