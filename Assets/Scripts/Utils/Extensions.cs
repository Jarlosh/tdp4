using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TDPF.FuckUp;
using TDPF.FuckUp.DialogueSystem;
using TDPF.TimeSwitch;
using UnityEngine;

namespace TDPF.Tools
{
    public static class Extensions
    {
        public static bool IsPast(this TimeMoment moment)
        {
            return (moment & TimeMoment.Past) != 0;
        }

        public static bool IsFuture(this TimeMoment moment)
        {
            return (moment & TimeMoment.Future) != 0;
        }

        public static (T current, T next) Order<T>(this TimeMoment targetMoment, T past, T future)
        {
            return IsPast(targetMoment) ? (future, past) : (past, future);
        }

        public static TimeMoment Invert(this TimeMoment moment)
        {
            return ~moment;
        }

        public static bool AllPassing(this IEnumerable<IActivateKey> keys)
        {
            foreach (var key in keys)
            {
                if (!key.Active)
                {
                    return false;
                }
            }
            return true;
        }

        public static T Validate<T>(this T key, GameObject parent) where T : ClueKey
        {
            if (key == null)
            {
                Debug.LogError($"KEY in {parent.gameObject.name} is null!");
            }

            return key;
        }

        public static T ValidateAll<T>(this T keys, GameObject parent) where T : IList<ClueKey>
        {
            foreach (var key in keys)
            {
                key.Validate(parent);
            }

            return keys;
        }

        public static bool AnyPassing(this IEnumerable<IActivateKey> keys)
        {
            foreach (var key in keys)
            {
                if (key == null)
                {
                    continue;
                }

                if (key.Active)
                {
                    return true;
                }
            }

            return false;
        }

        public static void Add<TK, TL, TV>(this IDictionary<TK, TL> dictionary, TK key, TV value)
            where TL: IList<TV>, new()
        {
            if (!dictionary.TryGetValue(key, out var list))
            {
                dictionary[key] = list = new TL();
            }
            list.Add(value);
        }

        public static string GetScenePath(this GameObject obj) => GetScenePath(obj.transform);
        public static string GetScenePath(this Component component) => GetScenePath(component.transform);
        public static string GetScenePath(this Transform transform)
        {
            return string.Join(Path.DirectorySeparatorChar, 
                TraceHierarchyUp(transform)
                .Select(t => t.gameObject.name)
                .Reverse());
        }

        public static IEnumerable<Transform> TraceHierarchyUp(Transform transform)
        {
            yield return transform;
            while (transform.parent != null)
            {
                yield return transform = transform.parent;
            }
        }
    }
}