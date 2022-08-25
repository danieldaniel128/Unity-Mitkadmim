using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Data.Scriptable;

namespace amitScripts
{
    public class EnemyBehavior : MonoBehaviour
    {

        public List<Transform> targetsAquired = new List<Transform>();
        public List<Transform> targetInAttRange = new List<Transform>();

        public float currentViewAngle;
        public float currentRadius;
        public float currentAttackRange;

        [SerializeField] EnemyStatsSO currentEnemySO;
        [SerializeField] LayerMask targetMask;
        [SerializeField] LayerMask ObstacleMask;

        private void Awake()
        {
            currentAttackRange = currentEnemySO.attackRange;
            currentRadius = currentEnemySO.visionRange;
            currentViewAngle = currentEnemySO.visionAngle;
        }

        IEnumerator findTargetsWithDelay(float delay)
        {
            while (true)
            {
                yield return new WaitForSeconds(delay);
                FindVisableTargets();
                TargetsInAttackRange();
            }
        }

        void FindVisableTargets()
        {
            targetsAquired.Clear();
            Collider[] targetInRadius = Physics.OverlapSphere(transform.position, currentEnemySO.visionRange, targetMask);

            for (int i = 0; i < targetInRadius.Length; i++)
            {
                Transform target = targetInRadius[i].transform;
                Vector3 dirToTarger = (target.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, dirToTarger) < currentEnemySO.visionAngle / 2)
                {
                    float dstToTarget = Vector3.Distance(transform.position, target.position);

                    if (!Physics.Raycast(transform.position, dirToTarger, dstToTarget, ObstacleMask))
                    {
                        targetsAquired.Add(target);
                    }
                }
            }
        }

        void TargetsInAttackRange()
        {
            targetInAttRange.Clear();
            Collider[] targetsInRange = Physics.OverlapSphere(transform.position, currentEnemySO.attackRange, targetMask);

            for (int i = 0; i < targetsInRange.Length; i++)
            {
                Transform target = targetsInRange[i].transform;
                Vector3 dirToTarget = (target.position - transform.position).normalized;
                float dstToTarget = Vector3.Distance(transform.position, target.position);

                if (!Physics.Raycast(transform.position, dirToTarget, dstToTarget, ObstacleMask))
                {
                    targetInAttRange.Add(target);
                }
            }
        }

        public Vector3 DirectionFromAngle(float viewAngle, bool angleIsGlobal)
        {
            if (!angleIsGlobal)
            {
                viewAngle += transform.eulerAngles.y;
            }
            return new Vector3(Mathf.Sin(viewAngle * Mathf.Deg2Rad), 0, Mathf.Cos(viewAngle * Mathf.Deg2Rad));
        }

        private void Start()
        {
            StartCoroutine("findTargetsWithDelay", .2f);
        }
    }

}

// Code By Amit
