// using System.Collections.Generic;
// using System.Linq;
// using TDPF.Tools;
// using UnityEngine;
// using UnityEngine.Serialization;
//
// namespace TDPF.FuckUp.DialogueSystem
// {
//     public class DialogController : MonoBehaviour
//     {
//         public DialogConfig Config;
//         
//         [FormerlySerializedAs("DialogueView")] public DialogueControl dialogueControl;
//
//         public void Awake()
//         {
//             Fuck.Up.Subscribe(SetState);
//         }
//
//         private void OnDestroy()
//         {
//             Fuck.Up.Unsubscribe(SetState);
//         }
//
//         protected void SetState(ClueKey changed)
//         {
//             var dialogs = Config.Items;
//
//             var filtered = Filter().ToArray();
//             if (filtered.Length == 0)
//             {
//                 return;
//             }
//
//             if (filtered.Length > 1)
//             {
//                 Debug.LogError(
//                     $"Too many dialogs! {filtered.Length} {string.Join('\n', filtered.Select(d => d.name))}");
//             }
//
//             var dialog = filtered.Last();
//             if (dialog.Restrictions.AllPassing())
//             {
//                 Debug.Log($"PLAY DIALOG {dialog}");
//                 dialogueControl.ShowView(dialog);
//                 // Dialogue.Instance.Invoke(dialog);
//             }
//
//             List<DialogItem> Filter()
//             {
//                 var result = new List<DialogItem>();
//                 foreach (var d in dialogs)
//                 {
//                     if (d.Active)
//                     {
//                         continue;
//                     }
//
//                     if (d.Restrictions.Length > 0 && d.Restrictions.AllPassing())
//                     {
//                         result.Add(d);
//                     }
//                 }
//
//                 return result;
//             }
//         }
//     }
// }