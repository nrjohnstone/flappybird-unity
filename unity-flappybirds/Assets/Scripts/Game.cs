using System;
using TinyMessenger;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts
{
    public class Game : MonoBehaviour
    {
        public static Game instance;
        public GameObject gameOverText;
        public Text scoreText;
        public bool gameOver = false;
        public float scrollSpeed = -1.5f;
        private int score = 0;

        void Awake () {
            if (instance == null)
            {
                instance = this;
                MessageHub.Instance.Subscribe<BirdDiedMessage>((m) => { BirdDied(); });
                MessageHub.Instance.Subscribe<BirdScoredMessage>(m => BirdScored());
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        void Update()
        {
            if (gameOver && Input.GetMouseButtonDown(0))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        public void BirdScored()
        {
            if (gameOver)
                return;
            score++;
            scoreText.text = "Score: " + score;
        }

        public void BirdDied () {
            gameOverText.SetActive(true);
            gameOver = true;
        }
    }

    public class BirdDiedMessage : ITinyMessage
    {
        public object Sender { get; private set; }
    }
}
