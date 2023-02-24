using UnityEngine;

namespace NavMeshTest.Towers
{
    public class SingleAttackAttackTower : AttackTower
    {
        protected override void Attack()
        {
            Debug.Log($"Single Attack with damage - {_damage}");
            _target.TryToKill(_damage);
        }
    }
}