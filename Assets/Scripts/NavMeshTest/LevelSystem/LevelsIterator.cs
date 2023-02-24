using NavMeshTest.ScriptableObjects;
using UnityEngine;

namespace NavMeshTest.LevelSystem
{
    public class LevelsIterator : MonoBehaviour
    {
        [SerializeField] private int _loop = 0;
        private Levels _levels;
        
        public void Construct(Levels levels)
        {
            _levels = levels;
        }
        
        public Levels.LevelInfo Next()
        {
            var levelNumber = LevelSaver.LevelNumber;
            if (levelNumber >= _levels.LevelsInfo.Count)
            {
                levelNumber = _loop;
            }
            return _levels.LevelsInfo[levelNumber];
        }

        public int CurrentLevelNumber()
        {
            return LevelSaver.LevelNumber < _levels.LevelsInfo.Count
                ? LevelSaver.LevelNumber
                : (LevelSaver.LevelNumber - _loop) % (_levels.LevelsInfo.Count - _loop) + _loop;
        }

        public void Complete()
        {
            LevelSaver.LevelNumber++;
        }
        
        static class LevelSaver
        {
            private const string key = "Level Number";
        
            public static int LevelNumber
            {
                set => PlayerPrefs.SetInt(key, value);
        
                get => PlayerPrefs.GetInt(key, 0);
            }
        }
    }
}