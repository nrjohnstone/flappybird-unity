using System;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class RandomColumnSpawner : IColumnSpawnStrategy
    {
        public int ColumnPoolSize { get; set; }
        public GameObject ColumnPrefab;
        public float SpawnRate { get; set; }
        public float ColumnMin { get; set; }
        public float ColumnMax { get; set; }
        public float SpawnXPosition { get; set; }

        public ITime Time { get; set; }
        public IRandom Random { get; set; }

        public RandomColumnSpawner(Func<IGameObject> columnFactory)
        {
            _columnFactory = columnFactory;

            SpawnXPosition = 10f;
            ColumnMax = 2f;
            ColumnMin = -2f;
            SpawnRate = 4f;
            ColumnPoolSize = 5;

            Time = new AmbientTime();
            Random = new AmbientRandom();
        }

        public void Initialize()
        {
            _columns = new IGameObject[ColumnPoolSize];
            for (int i = 0; i < ColumnPoolSize; i++)
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
            float spawnYPosition = Random.Range(ColumnMin, ColumnMax);

            _columns[_currentColumn].transform.position = new Vector2(SpawnXPosition, spawnYPosition);
            _currentColumn++;
            if (_currentColumn >= ColumnPoolSize)
                _currentColumn = 0;
        }

        public Vector2 GetColumnPosition(int i)
        {
            return _columns[i].transform.position;
        }

        private readonly Func<IGameObject> _columnFactory;

        private IGameObject[] _columns;
        private float _timeSinceLastSpawned;
        private int _currentColumn = 0;
    }

}