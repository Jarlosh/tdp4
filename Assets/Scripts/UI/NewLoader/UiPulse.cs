using DG.Tweening;
using UnityEngine;

namespace UI.NewLoader
{
    public class UiPulse: MonoBehaviour
    {
        public float duration;
        public Vector3 targetScale = Vector3.one * 1.2f;
        private Vector3 _startScale;
        private bool _active;
        private Tween _tween;
        public AnimationCurve curve;

        public bool Active { get; set; }

        private void Awake()
        {
            _startScale = transform.localScale;
            Shake(true);
        }

        private void Update()
        {
            if (_tween == null && Active)
            {
                Shake(true);
            }
        }

        private void Shake(bool isOut)
        {
            _tween = transform.DOScale(isOut ? targetScale : _startScale, duration).SetEase(curve).OnComplete(() =>
            {
                _tween = null;
                Shake(!isOut);
            });
        }
    }
}