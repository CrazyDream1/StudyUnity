using System;
using System.Collections;
using System.Collections.Generic;
using NavMeshTest.Enemies;
using NavMeshTest.WorldRules;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NavMeshTest.Towers
{
    [RequireComponent(typeof(SphereCollider))]
    public abstract class AttackTower : Tower
    {
        [TabGroup("Attack")][SerializeField] protected float _radius;
        [TabGroup("Attack")][SerializeField] protected int _damage;
        [TabGroup("Attack")][SerializeField] protected AttackType _attackType;
        [TabGroup("Attack")][SerializeField] protected AttackSpeed _attackSpeed;
        
        protected WorldRules.WorldRules _worldRules;
        
        protected Enemy _target;
        protected List<Enemy> _possibleTargets = new List<Enemy>();

        public void Construct(WorldRules.WorldRules worldRules)
        {
            _worldRules = worldRules;
        }

        private void Awake()
        {
            //TODO maybe optimize it
            GetComponent<SphereCollider>().radius = _radius;
        }

        protected abstract void Attack();

        IEnumerator AttackController()
        {
            while (_target != null)
            {
                UpdateTargets();
                Attack();
                yield return new WaitForSeconds(_worldRules.GetAttackSpeed(_attackSpeed));
            }
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                if (_target == null)
                {
                    _target = enemy;
                    _possibleTargets.Add(enemy);
                    StartCoroutine(AttackController());
                }
                else
                {
                    _possibleTargets.Add(enemy);
                }
            }
        }

        protected void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out Enemy enemy))
            {
                if (enemy == _target)
                {
                    _possibleTargets.Remove(enemy);
                    if (_possibleTargets.Count > 0)
                    {
                        _target = _possibleTargets[0];
                    }
                    else
                    {
                        _target = null;
                    }
                }
                else
                {
                    _possibleTargets.Remove(enemy);
                }
            }
        }

        protected void UpdateTargets()
        {
            for (var i = 0; i < _possibleTargets.Count; i++)
            {
                if (_possibleTargets[i] == null)
                {
                    _possibleTargets.RemoveAt(i);
                }
            }
        }
    }
}