using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TDPF.TimeSwitch;
using TDPF.Tools;
using UnityEngine;

namespace TDPF.FuckUp.DialogueSystem
{
    public interface IWithClueRestriction
    {
        TimeMoment TimeMoment { get; }
        IList<ClueKey> Keys { get; }
        IList<ClueKey> NotKeys { get; }
    }
    
    [Serializable]
    public class ClueRestriction: IWithClueRestriction
    {
        [field: SerializeField] public TimeMoment TimeMoment { get; private set; } = TimeMoment.Future | TimeMoment.Past;
        [field: SerializeField] public ClueKey[] keys;
        [field: SerializeField] public ClueKey[] notKeys;

        // backward compatibility
        IList<ClueKey> IWithClueRestriction.Keys => keys; 
        IList<ClueKey> IWithClueRestriction.NotKeys => notKeys;

        internal void Check(TimeMoment moment)
        {
            var result = this.AllPassing(moment);
            var sb = new StringBuilder($"Result: {result}");
            var wrongKeys = keys.Where(k => !k.Active).ToArray();
            var wrongNotKeys = keys.Where(k => !k.Active).ToArray();
            if(wrongKeys.Any() || wrongNotKeys.Any())
            {
                sb.AppendLine("Errors:");
                if (wrongKeys.Any())
                {
                    sb.AppendLine("\tKeys:");
                    foreach (var k in keys.Where(k => !k.Active))
                    {
                        sb.AppendLine($"\t\t[{k}] must be active!");
                    }   
                }
                if (wrongNotKeys.Any())
                {
                    sb.AppendLine("\tNotKeys:");
                    foreach (var k in notKeys.Where(k => k.Active))
                    {
                        sb.AppendLine($"\t\t[{k}] must be inactive!");
                    }
                }
            }
            Debug.Log(sb.ToString());
        }
    }
    
    public static class WithClueRestrictionExtensions
    {
        public static bool AllPassing(this IWithClueRestriction withRestrictions, TimeMoment current)
        {
            return withRestrictions.Keys.AllPassing() &&
                   !withRestrictions.NotKeys.AnyPassing() &&
                   (withRestrictions.TimeMoment & current) != 0;
        }
    }
}