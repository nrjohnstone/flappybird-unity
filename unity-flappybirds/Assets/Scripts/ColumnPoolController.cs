using TinyMessenger;

namespace Assets.Scripts
{
    public class ColumnPoolController
    {
        private readonly IColumnSpawnStrategy _columnSpawnStrategy;

        public ColumnPoolController(TinyMessengerHub instance)
        {
            _columnSpawnStrategy = new RandomColumnSpawner();
        }

        public void Start()
        {
            _columnSpawnStrategy.Start();
        }

        public void Update()
        {
            if (_columnSpawnStrategy.ShouldSpawnColumn())
            {
                _columnSpawnStrategy.Spawn();
            }
        }
    }
}