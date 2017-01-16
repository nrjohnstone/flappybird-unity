using Assets.Scripts.UnityAbstractions;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController
    {
        public int score { get; set; }
        public bool gameOver { get; set; }

        private readonly IText _scoreText;
        private readonly IGameObject _gameOverText;

        public GameController(IText scoreText, IGameObject gameOverText)
        {
            _scoreText = scoreText;
            _gameOverText = gameOverText;
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