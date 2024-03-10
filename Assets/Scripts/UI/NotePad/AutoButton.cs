using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UI.NotePad
{
    public class AutoButton: MonoBehaviour
    {
        public Button button;

        private UnityAction Callback { get; set; }
        private bool IsSubscribed { get; set; }
        private bool Enabled => gameObject.activeInHierarchy;
        public RectTransform RectTransform => transform as RectTransform;
        
        public void OnEnable()
        {
            if (button == null || Callback == null)
            {
                return;
            }
            button.onClick.AddListener(Callback);
            IsSubscribed = true;
        }

        public void OnDisable()
        {
            if (button == null || Callback == null)
            {
                return;
            }
            button.onClick.RemoveListener(Callback);
            IsSubscribed = false;
        }

        public void SetCallback(Action callback)
        {
            if (IsSubscribed)
            {
                OnDisable();
            }
            Callback = () => callback();
            if (Enabled)
            {
                OnEnable();
            }
        }   
    }
}