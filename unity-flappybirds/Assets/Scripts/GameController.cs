using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
using TinyMessenger;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController
    {
        public int score { get; set; }
        public bool gameOver { get; set; }

        private readonly IText _scoreText;
        private readonly IGameObject _gameOverText;

        public GameController(IText scoreText, IGameObject gameOverText, ITinyMessengerHub messenger)
        {
            _scoreText = scoreText;
            _gameOverText = gameOverText;
           
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
    }
}