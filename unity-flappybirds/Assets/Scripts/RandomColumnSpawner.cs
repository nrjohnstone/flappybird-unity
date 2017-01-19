using System;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class RandomColumnSpawner : IColumnSpawnStrategy
    {
        public int columnPoolSize = 5;
        public GameObject columnPrefab;
        public float SpawnRate = 4f;
        public float columnMin = -2f;
        public float columnMax = 2f;
        public float SpawnXPosition = 10f;

        public ITime Time { get; set; }
        public IRandom Random { get; set; }

        public RandomColumnSpawner(Func<IGameObject> columnFactory)
        {
            _columnFactory = columnFactory;
            Time = new AmbientTime();
            Random = new AmbientRandom();
        }

        public void Initialize()
        {
            _columns = new IGameObject[columnPoolSize];
            for (int i = 0; i < columnPoolSize; i++)
            {
                _columns[i] = _columnFactory.Invoke();
            }
        }

        public bool ShouldSpawnColumn()
        {
            _timeSinceLastSpawned += Time.deltaTime;
            return (_timeSinceLastSpawned >= SpawnRate);
        }

        public void Spawn()
        {
            _timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(columnMin, columnMax);

            _columns[_currentColumn].transform.position = new Vector2(SpawnXPosition, spawnYPosition);
            _currentColumn++;
            if (_currentColumn >= columnPoolSize)
                _currentColumn = 0;
        }

        private readonly Func<IGameObject> _columnFactory;

        private IGameObject[] _columns;
        private float _timeSinceLastSpawned;
        
        private int _currentColumn = 0;

        public Vector2 GetColumnPosition(int i)
        {
            return _columns[i].transform.position;
        }
    }
}