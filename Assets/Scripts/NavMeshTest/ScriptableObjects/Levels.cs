using System;
using System.Collections.Generic;
using NavMeshTest.Enemies;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NavMeshTest.ScriptableObjects
{
    [CreateAssetMenu(fileName = "LevelsInfo", menuName = "Create LevelsInfo", order = 51)]
    public class Levels : ScriptableObject
    {
        [TableList]
        public List<LevelInfo> LevelsInfo;

        [Serializable]
        public class LevelInfo
        {
            public GameObject MapPrefab;
            public int HP;
            public int StartGold = 0;
            public List<EnemyWave> EnemyWaves; 
        }
    }
}