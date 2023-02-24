using NavMeshTest.WorldRules;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace NavMeshTest.Enemies
{
    public class Enemy : MonoBehaviour
    {
        [TabGroup("Common")][SerializeField] private GameObject _enemyModel;
        [TabGroup("Battle")][SerializeField] private int _hp;
        [TabGroup("Battle")][SerializeField] private ArmorType _armorType;
        [TabGroup("Battle")][SerializeField] private int _damage;
        [TabGroup("Battle")][SerializeField] private AttackType _attackType;
        [TabGroup("Battle")][SerializeField] private AttackSpeed _attackSpeed;
        [TabGroup("Common")][SerializeField] private float _speed;
        [TabGroup("Common")][SerializeField] private int _gold;
        [TabGroup("Common")][SerializeField] private int _damageToBase;
        [TabGroup("Common")][SerializeField] private float _deathTime = 0.1f;
        
        [SerializeField] private GameObject _dest;
        [Button("Go to Dest")]
        private void GoToDest()
        {
            _navMeshAgent.destination = _dest.transform.position;
        }
        
        private GameController _gameController;
        private NavMeshAgent _navMeshAgent;
        private Vector3 _destination;
        
        private void Awake()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _navMeshAgent.speed = _speed;
        }

        public void SetFinalDestination(Vector3 destination, GameController gameController)
        {
            _navMeshAgent.destination = destination;
            _gameController = gameController;
            _destination = destination;
        }

        public void GoToDestination(Vector3 destination)
        {
            _navMeshAgent.destination = destination;
        }

        public void GoToFinalDestination()
        {
            _navMeshAgent.destination = _destination;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EndPoint endPoint))
            {
                _gameController.EnemyReachedEndPoint(_damageToBase);
                Destroy(this);
            }
        }

        public void TryToKill(int damage)
        {
            _hp -= damage;
            if (_hp <= 0)
            {
                Dead();
            }
        }
        
        private void Dead()
        {
            _gameController.EnemyWasKilled(_gold);
            _navMeshAgent.destination = transform.position; // TODO maybe change
            Destroy(gameObject, _deathTime);
        }
    }
}