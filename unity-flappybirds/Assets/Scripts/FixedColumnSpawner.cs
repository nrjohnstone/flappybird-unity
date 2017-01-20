using System;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class FixedColumnSpawner : BaseColumnSpawner, IColumnSpawnStrategy
    {
        public float SpawnRate { get; set; }
        public ITime Time { get; set; }
        public float SpawnXPosition { get; set; }
        public float SpawnYPosition { get; set; }

        public FixedColumnSpawner(Func<IGameObject> columnFactory)
        {
            ColumnFactory = columnFactory;
            ColumnPoolSize = 5;
            SpawnXPosition = 10f;
            SpawnYPosition = 1.1f;
            SpawnRate = 4f;

            Time = new AmbientTime();
        }

        public bool ShouldSpawnColumn()
        {
            _timeSinceLastSpawned += Time.deltaTime;
            return (_timeSinceLastSpawned >= SpawnRate);
        }

        public void Spawn()
        {
            _timeSinceLastSpawned = 0;
            Columns[_currentColumn].transform.position = new Vector2(SpawnXPosition, SpawnYPosition);
            _currentColumn++;
            if (_currentColumn >= ColumnPoolSize)
                _currentColumn = 0;
        }
        
        private float _timeSinceLastSpawned;
        private int _currentColumn = 0;
    }
}