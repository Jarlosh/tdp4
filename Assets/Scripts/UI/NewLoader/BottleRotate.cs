using System;
using DG.Tweening;
using UnityEngine;

namespace UI.NewLoader
{
    public class BottleRotate: MonoBehaviour
    {
        public Vector3 rotation;
        public ParticleSystem ps;

        public void SetRate(float rate)
        {
            var emission = ps.emission;
            
            var rr = emission.rateOverTime;
            rr.constant = rate;
            emission.rateOverTime = rr;
        }

        public void Do(float duration, bool open)
        {
            transform.DORotate(rotation * (open ? 1 : -1), duration);
        }
    }
}