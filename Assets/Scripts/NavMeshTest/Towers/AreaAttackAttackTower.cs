using NavMeshTest.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NavMeshTest.Towers
{
    public class AreaAttackAttackTower : AttackTower
    {
        [TabGroup("Attack")][SerializeField] private float _damageRadius;
        
        private Collider[] _colliders = new Collider[20];
        
        protected override void Attack()
        {
            Physics.OverlapSphereNonAlloc(_target.transform.position, _damageRadius, _colliders);
            //TODO change to layer mask
            foreach (Collider collider in _colliders)
            {
                if (collider.TryGetComponent(out Enemy enemy))
                {
                    enemy.TryToKill(_damage);
                }
            }
        }
    }
}