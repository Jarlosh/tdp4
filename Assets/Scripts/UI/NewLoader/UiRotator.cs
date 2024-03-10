using DG.Tweening;
using UnityAsync;
using UnityEngine;

namespace UI.NewLoader
{
    public class UiRotator: MonoBehaviour
    {
        public float duration;
        public float angle;
        public float delay;
        private bool _active;
        private Tween _tween;
        public AnimationCurve curve;

        public bool Active { get; set; }

        private void Awake()
        {
            Honk();
            
        }

        private async void Honk()
        {
            await Await.Seconds(delay);
            Shake(true);
        }

        private void Shake(bool isOut)
        {
            var rot = Quaternion.AngleAxis(angle * (isOut ? 1 : -1), Vector3.forward);
            _tween = transform.DORotate(rot.eulerAngles, duration).SetEase(curve).OnComplete(() =>
            {
                _tween = null;
                Shake(!isOut);
            });
        }
    }
}