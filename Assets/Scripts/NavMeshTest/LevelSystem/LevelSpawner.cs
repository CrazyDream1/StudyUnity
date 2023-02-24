using System;
using NavMeshTest.ScriptableObjects;
using UnityEngine;

namespace NavMeshTest.LevelSystem
{
    public class LevelSpawner : MonoBehaviour
    {
        private Levels _levels;
        private LevelsIterator _levelsIterator;

        public void Construct(Levels levels, LevelsIterator levelsIterator)
        {
            _levels = levels;
            _levelsIterator = levelsIterator;
        }
        
        private void Awake()
        {
            var levelInfo = _levelsIterator.Next();
            var level = Instantiate(levelInfo.MapPrefab);
            if (level.TryGetComponent(out GameController gameController))
            {
                gameController.Initialize(levelInfo.HP, levelInfo.StartGold, levelInfo.EnemyWaves);
            }
            else
            {
                Debug.LogError("No game controller");
            }
        }
    }
}