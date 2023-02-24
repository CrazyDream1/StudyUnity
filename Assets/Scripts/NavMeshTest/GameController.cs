using System.Collections;
using System.Collections.Generic;
using NavMeshTest.Enemies;
using NavMeshTest.LevelSystem;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace NavMeshTest
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _startPosition;
        [SerializeField] private GameObject _endPosition;
        [SerializeField] private float _time = 30f;
        
        private int _hp;
        private int _gold;
        private List<EnemyWave> _enemyWaves;
        private int _currentWaveId = 0;
        private int _numberOfEnemies = 0;

        private LevelsIterator _levelsIterator;

        public void Construct(LevelsIterator levelsIterator)
        {
            _levelsIterator = levelsIterator;
        }
        
        public void Initialize(int hp, int gold, List<EnemyWave> enemyWaves)
        {
            _hp = hp;
            _gold = gold;
            _enemyWaves = enemyWaves;
        }

        [Button("Start")]
        public void StartFirstWave()
        {
            StartCoroutine(Waves());
        }

        private IEnumerator Waves()
        {
            while (_currentWaveId < _enemyWaves.Count)
            {
                StartCoroutine(SpawnWave(_enemyWaves[_currentWaveId]));
                _currentWaveId++;
                yield return new WaitForSeconds(_time);
            }
        }

        private IEnumerator SpawnWave(EnemyWave enemyWave)
        {
            foreach (EnemyWave.EnemyWithNumber enemyWithNumber in enemyWave.Enemies)
            {
                _numberOfEnemies += enemyWithNumber.NumberOfEnemies;
                for (var j = 0; j < enemyWithNumber.NumberOfEnemies; j++)
                {
                    NavMeshHit hit;
                    NavMesh.SamplePosition(_startPosition.transform.position, out hit, 1.0f, 1);
                    var enemy = Instantiate(enemyWithNumber.Enemy, hit.position, Quaternion.identity);
                    enemy.GetComponent<NavMeshAgent>().Warp(_startPosition.transform.position);
                    enemy.GetComponent<Enemy>().SetFinalDestination(_endPosition.transform.position, this);
                    yield return new WaitForSeconds(1f);
                }
            }
        }

        public void EnemyWasKilled(int goldEarn)
        {
            _numberOfEnemies--;
            _gold += goldEarn;
            Debug.Log($"Gold earn - {goldEarn}; Gold - {_gold}");
            if (_numberOfEnemies == 0 && _currentWaveId == _enemyWaves.Count)
            {
                Debug.Log("Win");
                _levelsIterator.Complete();
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        public void EnemyReachedEndPoint( int damage)
        {
            _hp -= damage;
            Debug.Log($"Damage taken {damage}");
            if (_hp <= 0)
            {
                Debug.Log("Lost");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }
}