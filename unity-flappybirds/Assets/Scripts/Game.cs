using System;
using Assets.Scripts.Messaging;
using Assets.Scripts.UnityAbstractions;
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
        private GameController _gameController;

        void Awake () {
            if (instance == null)
            {
                instance = this;
                _gameController = new GameController(new TextWrapper(scoreText));
                MessageHub.Instance.Subscribe<BirdDiedMessage>((m) => { BirdDied(); });
                MessageHub.Instance.Subscribe<BirdScoredMessage>(m => _gameController.BirdScored());
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
        
        public void BirdDied () {
            gameOverText.SetActive(true);
            gameOver = true;
        }
    }
}
