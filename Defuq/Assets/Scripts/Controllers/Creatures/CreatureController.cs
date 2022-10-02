using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Data.Creatures;
using System.Events;
using Systems.Creatures;
using Animations.Enemies;


namespace Controllers.Creatures
{
    public class CreatureController : MonoBehaviour
    {
        public LineOfSight _lineOfSight;

        [SerializeField] NavMeshAgent _agent;
        [SerializeField] EnemyStatsSO _data;
        [SerializeField] CreatureEvents _enemyEvent;
        [SerializeField] AnimationSwitch _enemySwitch;
        [SerializeField] CombatSystem _combSystem;

        private MeshCollider meshCo;
        private GameObject _target;

        private int _currentHealth;
        private int _currentDamage;

        [ContextMenu("EnemyDebug")]
        void EnemyDebug()
        {
            Debug.Log("Current Enemy Health is:" + _currentHealth);
        }

        private void Awake()
        {
            _currentHealth = _data.maxHealth;
            _currentDamage = _data.attackDamage;
            meshCo = this.GetComponent<MeshCollider>(); 

            if (_lineOfSight == null) return;
            _lineOfSight.Init(_data.visionRange, _data.visionAngle, _data.attackRange);

        }

        private void Update()
        {
            if (_currentHealth > 0)
            {
                meshCo.enabled = true;
                _enemySwitch.AnimatorKilledFalse();
            }
                if (_lineOfSight.targetsAquired.Count > 0)
            {
                if (_currentHealth > 0)
                {
                    AudioManager.instance.ZombieTargetAquired();
                }
                int latestsInList = _lineOfSight.targetsAquired.Count - 1;
                _target = _lineOfSight.targetsAquired[latestsInList].gameObject;
                // This will make the enemy chase the latest gameobject in his sight line of sight, (wont work if you are inside of his att range)
            }

            if (_target != null && _currentHealth > 0)
            {
                _agent.SetDestination(_target.transform.position);
                _agent.isStopped = false;

                float veloX = Mathf.Abs(_agent.velocity.x);
                float veloZ = Mathf.Abs(_agent.velocity.z);

                if (veloX > veloZ)
                {
                    _enemySwitch.SetSpeed(veloX);
                }
                if (veloX < veloZ)
                {
                    _enemySwitch.SetSpeed(veloZ);
                }

                while (_lineOfSight.targetInAttRange.Count > 0 && _agent.isStopped == false)
                {
                    _agent.isStopped = true;
                    if (!_enemySwitch.GetAttackState())
                    {
                        _enemyEvent.enemyAttack.Invoke();
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Weapon")&& PlayerAttackController.isAttacking)
            {
                AudioManager.instance.SwordHit();
                _enemyEvent.enemyGotHit.Invoke();
                _combSystem.TakeDamage(GameManager.instance.playerData.GetAttackDamage(), gameObject, this);
            }
        }

        public int GetMaxHp()
        {
            return this._data.maxHealth;
        }

        public int GetCurrentHealth()
        {
            return _currentHealth;
        }
       
        public int GetCurrentDamage()
        {
            return _currentDamage;
        }
        public CreatureEvents GetEnemyEvent() 
        {
            return _enemyEvent;
        }
        public AnimationSwitch GetEnemySwitch() 
        {
            return _enemySwitch;
        }
        public void SetEnemySwitch(AnimationSwitch enemySwitch)
        {
            _enemySwitch= enemySwitch;
        }
        public void SetCurrentHealth(int newHealth)
        {
            _currentHealth = newHealth;
        }
        public void SetCurrentDamage(int newDamage)
        {
            _currentDamage = newDamage;
        }
    }

}

// Code By Amit
