using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NavMeshTest.Enemies
{
    [CreateAssetMenu(fileName = "EnemyWave", menuName = "Create EnemyWave", order = 51)]
    public class EnemyWave : ScriptableObject
    {
        [TableList]
        public List<EnemyWithNumber> Enemies;

        [Serializable]
        public class EnemyWithNumber
        {
            public Enemy Enemy;
            public int NumberOfEnemies;
        }
    }
}