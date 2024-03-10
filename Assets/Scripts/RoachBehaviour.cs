using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TDPF
{
    public class RoachBehaviour : MonoBehaviour
    {
        [SerializeField] private float normalSpeed;
        [SerializeField] private float runawaySpeed;
        [SerializeField] private float maxRadius;
        [SerializeField] private float runawayRadius;

        private Vector3 startPosition;
        private Vector3 targetPosition;
        private float timeToWaitUntil;
        private Transform playerTransform;
        private bool isRunaway;
        private Animator animator;
        private static readonly int propIsRunning = Animator.StringToHash("IsRunning");

        private IEnumerator Start()
        {
            animator = GetComponentInChildren<Animator>();
            playerTransform = FindObjectOfType<FPSController>().transform;
            startPosition = transform.position;
            
            while (isActiveAndEnabled)
            {
                UpdateTarget();

                animator.SetBool(propIsRunning, true);
                while (!IsNearTarget())
                {
                    MoveToTarget();
                    yield return null;
                    
                    if (IsNearPlayer())
                        UpdateTarget();
                }
                
                timeToWaitUntil = Time.time + GetCooldown();
                animator.SetBool(propIsRunning, false);
                while (Time.time < timeToWaitUntil && !IsNearPlayer())
                {
                    yield return null;
                }

                yield return null;
            }
        }

        private float GetSpeed() =>
            isRunaway
                ? runawaySpeed
                : normalSpeed;

        private void UpdateTarget()
        {
            if (IsNearPlayer())
            {
                targetPosition = startPosition;
                isRunaway = true;
            }
            else
            {
                var pos = Random.insideUnitCircle;
                if (Vector3.Distance(startPosition, transform.position) > maxRadius)
                    targetPosition = startPosition + new Vector3(pos.x, 0, pos.y);
                else
                    targetPosition = transform.position + new Vector3(pos.x, 0, pos.y) * Random.Range(1, 3);
                isRunaway = false;
            }
        }

        private float GetCooldown() =>
            isRunaway
                ? 5
                : Random.Range(0.75f, 1f);

        private bool IsNearTarget() =>
            Vector3.Distance(transform.position, targetPosition) < 0.1f;

        private bool IsNearPlayer() =>
            Vector3.Distance(playerTransform.position, transform.position) < runawayRadius;

        private void MoveToTarget()
        {
            transform.LookAt(targetPosition);
            transform.Translate(0, 0, GetSpeed() * Time.deltaTime, Space.Self);
        }

        private void OnDrawGizmosSelected()
        {
            var oldColor = Gizmos.color;
            var newColor = Color.green;
            newColor.a = 0.1f;
            Gizmos.color = newColor;
            Gizmos.DrawSphere(startPosition, maxRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawLine(startPosition, transform.position);
            Gizmos.color = oldColor;
        }
    }
}
