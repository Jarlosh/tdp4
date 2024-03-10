using TDPF.FuckUp.DialogueSystem;
using UnityEngine;
using Utils;

namespace TDPF.FuckUp
{
    [CreateAssetMenu(menuName = "SO/ClueKey", fileName = "ClueKey", order = 0)]
    public class ClueKey: ScriptableObject, IActivateKey
    {
        [field: SerializeField, ReadOnly]
        public virtual bool Active { get; private set; }

        private void OnEnable()
        {
            #if UNITY_EDITOR
            Debug.Log($"Reset [{GetType().Name}] Active");
            #endif
            
            Active = false;
        }

        void IActivateKey.Activate(GameObject gameObject)
        {
            #if UNITY_EDITOR
            Debug.Log($"Activating {name}", gameObject);
            if (Active)
            {
                Debug.LogError($"{name} already active!", gameObject);
            }
            #endif
            
            Active = true;
        }

        public override string ToString()
        {
            return name;
        }

        void IActivateKey.Deactivate()
        {
            #if UNITY_EDITOR
            Debug.Log($"Deactivating {name}");
            if (!Active)
            {
                Debug.LogError($"{name} already inactive!");
            }
            #endif
            Active = false;
        }
    }
}