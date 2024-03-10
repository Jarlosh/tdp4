using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI.Loader
{
    public class TweenObj: MonoBehaviour
    {
        public Transform startPos, endPos;
        public bool rotate = true;
        public bool move = true;
        public bool resetOnAwake = true;
        public float duration = 0.5f;
        
        public bool debugKButton;

        private bool State { get; set; }

        private bool IsAnimating { get; set; }
        
        [ContextMenu("SetStart")]
        public void SetStart() => SetState(false);

        [ContextMenu("SetEnd")]
        public void SetEnd() => SetState(true);

        private void Awake()
        {
            if(resetOnAwake)
            {
                SetPosition(startPos);
            }
        }

        private void Update()
        {
            if (debugKButton && Input.GetKeyDown(KeyCode.K))
            {
                Toggle();
            }
        }

        private void SetPosition(Transform position)
        {
            if (position == null)
            {
                Debug.LogException(new NullReferenceException(nameof(position)));
            }
            else
            {
                var t = transform;
                t.position = position.position;
                t.rotation = position.rotation;
            }
        }

        private bool SetState(bool toEnd) => Animate(toEnd ? endPos : startPos);
        
        public bool TryToggle() => SetState(!State);
        
        public void Toggle() => TryToggle();
        
        private bool Animate(Transform t)
        {
            if (IsAnimating)
            {
                return false;
            }
            IsAnimating = true;

            State = t == endPos;
            var seq = DOTween.Sequence();
            if (move)
            {
                seq.Insert(0, transform.DOMove(t.position, duration));
            }
            if (rotate)
            {
                seq.Insert(0, transform.DORotate(t.rotation.eulerAngles, duration));
            }

            seq.InsertCallback(duration, () => { IsAnimating = false;});
            return true;
        }
    }
}