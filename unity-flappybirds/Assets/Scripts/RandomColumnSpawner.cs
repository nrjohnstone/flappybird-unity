using System;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class RandomColumnSpawner : BaseColumnSpawner, IColumnSpawnStrategy
    {
        public GameObject ColumnPrefab;
        public float SpawnRate { get; set; }
        public float ColumnMin { get; set; }
        public float ColumnMax { get; set; }
        public float SpawnXPosition { get; set; }

        public ITime Time { get; set; }
        public IRandom Random { get; set; }

        public RandomColumnSpawner(Func<IGameObject> columnFactory)
        {
            ColumnFactory = columnFactory;

            SpawnXPosition = 10f;
            ColumnMax = 2f;
            ColumnMin = -2f;
            SpawnRate = 4f;
            ColumnPoolSize = 5;

            Time = new AmbientTime();
            Random = new AmbientRandom();
        }

        public bool ShouldSpawnColumn()
        {
            _timeSinceLastSpawned += Time.deltaTime;
            return (_timeSinceLastSpawned >= SpawnRate);
        }

        public void Spawn()
        {
            _timeSinceLastSpawned = 0;
            float spawnYPosition = Random.Range(ColumnMin, ColumnMax);

            Columns[_currentColumn].transform.position = new Vector2(SpawnXPosition, spawnYPosition);
            _currentColumn++;
            if (_currentColumn >= ColumnPoolSize)
                _currentColumn = 0;
        }

        private float _timeSinceLastSpawned;
        private int _currentColumn = 0;
    }

}