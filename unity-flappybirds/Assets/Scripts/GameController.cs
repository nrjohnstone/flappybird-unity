using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using TinyMessenger;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController
    {
        public int score { get; set; }
        public bool gameOver { get; set; }

        private readonly IText _scoreText;
        private readonly IGameObject _gameOverText;
        private readonly IInput _input;
        private readonly ISceneManager _sceneManager;

        public GameController(IText scoreText, IGameObject gameOverText, ITinyMessengerHub messenger,
            IInput input, ISceneManager sceneManager)
        {
            _scoreText = scoreText;
            _gameOverText = gameOverText;
            _input = input;
            _sceneManager = sceneManager;

            messenger.Subscribe<BirdDiedMessage>((m) => BirdDied());
            messenger.Subscribe<BirdScoredMessage>(m => BirdScored());
        }

        public void BirdScored()
        {
            if (gameOver)
                return;
            score++;
            _scoreText.text = "Score: " + score;
        }

        public void BirdDied()
        {
            _gameOverText.SetActive(true);
            gameOver = true;
        }

        public void Update()
        {
            if (gameOver && _input.IsLeftMouseButtonDown())
            {
                _sceneManager.LoadScene(_sceneManager.GetActiveScene().buildIndex);
            }
        }
    }
}