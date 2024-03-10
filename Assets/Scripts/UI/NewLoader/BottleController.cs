using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI.NewLoader
{
    public class BottleController: MonoBehaviour
    {
        [SerializeField] private float positiveSpeed;
        
        [SerializeField] private float negativeSpeedCoefficient;
        
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;
        [SerializeField] private float power;
        [SerializeField] private float rotateSpeedCoefficient;

        [SerializeField] private AnimationCurve curve;
        [SerializeField] private AnimationCurve beerCurve;
        [FormerlySerializedAs("particleSystem")] [SerializeField] private new ParticleSystem ps;
        [SerializeField] private Image _filled;

        public bool Blocked { get; set; } = true;

        private float Progress { get; set; } = 1;
        private float RotateProgress { get; set; } = 1;

        private float CurrenAngle => Mathf.Lerp(-minAngle,-Mathf.Abs(maxAngle), 
            curve.Evaluate(1 - RotateProgress)) * Mathf.Sign(maxAngle);

        private void Update()
        {
            var active = !Blocked;
            var speed = positiveSpeed * (active ? 1 : negativeSpeedCoefficient) * Time.deltaTime;  
            Progress = Mathf.MoveTowards(Progress, active ? 0 : 1, speed);
            RotateProgress = Mathf.MoveTowards(RotateProgress % 360, active ? 0 : 1, speed * rotateSpeedCoefficient);
            transform.rotation = Quaternion.Euler(0, 0, CurrenAngle);
            //
            // var emission = ps.emission;
            // var rate = emission.rateOverTime;
            // rate.constant = active ? beerCurve.Evaluate(Progress) * power : 0;
            // emission.rateOverTime = rate;
            // _filled.fillAmount = Progress;
        }
    }
}