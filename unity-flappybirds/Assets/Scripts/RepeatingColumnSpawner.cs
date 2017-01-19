using System;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class RepeatingColumnSpawner : IColumnSpawnStrategy
    {
        public int ColumnPoolSize { get; set; }
        public float SpawnRate { get; set; }
        public ITime Time { get; set; }
        public float SpawnXPosition { get; set; }
        public float SpawnYPosition { get; set; }

        public RepeatingColumnSpawner(Func<IGameObject> columnFactory)
        {
            _columnFactory = columnFactory;
            ColumnPoolSize = 5;
            SpawnXPosition = 10f;
            SpawnYPosition = 1.1f;
            SpawnRate = 4f;

            Time = new AmbientTime();
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
            _columns[_currentColumn].transform.position = new Vector2(SpawnXPosition, SpawnYPosition);
            _currentColumn++;
            if (_currentColumn >= ColumnPoolSize)
                _currentColumn = 0;
        }

        public Vector2 GetColumnPosition(int i)
        {
            return _columns[i].transform.position;
        }

        private IGameObject[] _columns;
        private readonly Func<IGameObject> _columnFactory;
        private float _timeSinceLastSpawned;
        private int _currentColumn = 0;
    }
}