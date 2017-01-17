using System;
using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using TinyMessenger;

namespace Assets.Scripts
{
    public class ColumnPoolController
    {
        private readonly ITinyMessengerHub _messenger;
        private readonly IColumnSpawnStrategy _columnSpawnStrategy;
        private bool _gameOver;

        public ColumnPoolController(ITinyMessengerHub messenger, Func<GameObjectWrapper> columnFactory)
        {
            _messenger = messenger;
            _columnSpawnStrategy = new RandomColumnSpawner(columnFactory);
        }

        public void Start()
        {
            _columnSpawnStrategy.Initialize();
            _messenger.Subscribe<GameOverMessage>(m => GameOver());
        }

        public void Update()
        {
            if (_gameOver)
                return;

            if (_columnSpawnStrategy.ShouldSpawnColumn())
            {
                _columnSpawnStrategy.Spawn();
            }
        }

        private void GameOver()
        {
            _gameOver = true;
        }

    }
}