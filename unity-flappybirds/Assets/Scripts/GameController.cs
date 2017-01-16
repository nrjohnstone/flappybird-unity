using Assets.Scripts.UnityAbstractions;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class GameController
    {
        public int score { get; set; }
        public bool gameOver = false;

        private readonly IText _scoreText;

        public GameController(IText scoreText)
        {
            _scoreText = scoreText;
        }

        public void BirdScored()
        {
            if (gameOver)
                return;
            score++;
            _scoreText.text = "Score: " + score;
        }
    }
}