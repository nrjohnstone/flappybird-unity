using System;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class RandomColumnSpawner : IColumnSpawnStrategy
    {
        public int columnPoolSize = 5;
        public GameObject columnPrefab;
        public float spawnRate = 4f;
        public float columnMin = -2f;
        public float columnMax = 2f;

        public RandomColumnSpawner(Func<GameObjectWrapper> columnFactory)
        {
            _columnFactory = columnFactory;
            _columns = new GameObjectWrapper[columnPoolSize];
        }

        public void Initialize()
        {
            for (int i = 0; i < columnPoolSize; i++)
            {
                _columns[i] = _columnFactory.Invoke();
            }
        }

        public bool ShouldSpawnColumn()
        {
            return true;
        }

        public void Spawn()
        {
            timeSinceLastSpawned += Time.deltaTime;
            if (_gameOver == false && timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                float spawnYPosition = Random.Range(columnMin, columnMax);

                _columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                currentColumn++;
                if (currentColumn >= columnPoolSize)
                    currentColumn = 0;
            }
        }

        private readonly Func<GameObjectWrapper> _columnFactory;

        private readonly IGameObject[] _columns;
        private float timeSinceLastSpawned;
        private float spawnXPosition = 10f;
        private int currentColumn = 0;
        private ColumnPoolController _columnPoolController;
        private bool _gameOver = false;

        private readonly ITime Time = new AmbientTime();
        private readonly IRandom Random = new AmbientRandom();

    }
}