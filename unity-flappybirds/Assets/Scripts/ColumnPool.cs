using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using UnityEngine;

namespace Assets.Scripts
{
    public class ColumnPool : MonoBehaviour
    {
        public int columnPoolSize = 5;
        public GameObject columnPrefab;
        public float spawnRate = 4f;
        public float columnMin = -2f;
        public float columnMax = 2f;

        private IGameObject[] columns;
        private Vector2 objectPoolPosition = new Vector2(-15f, -25f);
        private float timeSinceLastSpawned;
        private float spawnXPosition = 10f;
        private int currentColumn = 0;
        private ColumnPoolController _columnPoolController;
        private bool _gameOver = false;

        private readonly ITime Time = new AmbientTime();

        public void Awake()
        {
            MessageHub.Instance.Subscribe<GameOverMessage>(m => GameOver());
        }

        private void GameOver()
        {
            _gameOver = true;
        }

        void Start ()
        {
            _columnPoolController = new ColumnPoolController();
            _columnPoolController.Start();
            columns = new GameObjectWrapper[columnPoolSize];
            for (int i = 0; i < columnPoolSize; i++)
            {
                var instantiate = (GameObject) Instantiate(columnPrefab, objectPoolPosition, Quaternion.identity);
                columns[i] = new GameObjectWrapper(instantiate);
            }
        }
    
        void Update ()
        {
            _columnPoolController.Update();
            timeSinceLastSpawned += Time.deltaTime;
            if (_gameOver == false && timeSinceLastSpawned >= spawnRate)
            {
                timeSinceLastSpawned = 0;
                float spawnYPosition = Random.Range(columnMin, columnMax);
                
                columns[currentColumn].transform.position = new Vector2(spawnXPosition, spawnYPosition);
                currentColumn++;
                if (currentColumn >= columnPoolSize)
                    currentColumn = 0;
            }

        }
    }
}
