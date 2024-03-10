using UnityEngine;

namespace UI.NotePad
{
    public class OpenAnimator: MonoBehaviour
    {
        private static readonly int OpenBlend = Animator.StringToHash("OpenBlend");
        public float Target { get; set; } = 0;
        public float Progress { get; set; } = 0;
        public AnimationCurve Curve;

        public Animator animator;

        public float duration = 1;

        private void Update()
        {
            var isPositive = (Progress - Target) > 0;
            Progress = Mathf.MoveTowards(Progress, Target, Time.deltaTime / duration);
            var p = !isPositive ? Curve.Evaluate(Progress) : 1 - Curve.Evaluate(1 - Progress);
            animator.SetFloat(OpenBlend, Curve.Evaluate(p));   
        }
    }
}